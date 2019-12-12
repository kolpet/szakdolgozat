using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Structures;
using ViewModel.Adapters;

namespace Szakdolgozat.ViewModel.Pages
{
    public class PreferencesViewModel : ViewModelBase, IPageTurn
    {
        private PreferencesModel _model;
        
        private IContext _context;

        private List<int> Group1;

        private List<int> Group2;

        public DelegateCommand RandomizeCommand { get; private set; }

        public DelegateCommand ToParticipantsCommand { get; private set; }

        public DelegateCommand ToAlgorithmCommand { get; private set; }

        public ObservableCollection<PreferenceCell> PreferenceGrid { get; set; }

        public List<string> ParticipantList { get; set; }

        public int PreferenceGridRows { get => _context.TotalSize; }

        public int PreferenceGridColumns { get => _context.GroupSize; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public PreferencesViewModel(PreferencesModel model, IContext context)
        {
            _model = model;
            _context = context;

            RandomizeCommand = new DelegateCommand(param => OnRandomizeCommand());
            ToParticipantsCommand = new DelegateCommand(param => OnToParticipantsCommand());
            ToAlgorithmCommand = new DelegateCommand(param => OnToAlgorithmCommand());

            PreferenceGrid = new ObservableCollection<PreferenceCell>();
            ParticipantList = new List<string>();
        }

        public void RefreshPage()
        {
            if(_context.ParticipantsChanged)
            {
                _model.Initialize();
            }
            Group1 = _context.Group1Participants.Select(x => x.ID).OrderBy(x => x).ToList();
            Group2 = _context.Group2Participants.Select(x => x.ID).OrderBy(x => x).ToList();
            RefreshGrid();
        }

        public void Load()
        {
            _model.Load();
            RefreshPage();
        }

        private void RefreshGrid()
        {
            PreferenceGrid.Clear();
            ParticipantList.Clear();
            List<Preference> preferenceList = new PrioritiesAdapter(_context.GetPriorities);
            for(int i = 0; i < PreferenceGridRows; i++)
            {
                ParticipantList.Add(GetName(preferenceList[i].ID));

                for(int j = 0; j < PreferenceGridColumns; j++)
                {
                    List<int> Group = _context.GetParticipants[i].Group == MarriageGroup.Group1 ? Group2 : Group1;
                    PreferenceGrid.Add(new PreferenceCell()
                    {
                        Id = preferenceList[i].ID,
                        X = j,
                        Y = i,
                        Preferences = Group.Select(x => GetName(x)).ToList(),
                        PreferenceValues = Group,
                        SelectedIndex = Group.IndexOf(preferenceList[i].Preferences[j])
                    });
                    PreferenceGrid.Last().SelectedChanged += new EventHandler(PreferenceCell_SelectedChanged);
                }
            }
            OnPropertyChanged("PreferenceGrid");
        }

        private void PreferenceCell_SelectedChanged(object sender, EventArgs e)
        {
            PreferenceCell cell = (PreferenceCell)sender;
            _model.EditPreference(cell.Id, cell.X, cell.PreferenceValues[cell.SelectedIndex]);
        }

        private string GetName(int id)
        {
            string name = id.ToString();
            if(_context.GetParticipants[id].Name != "")
            {
                name += " (" + _context.GetParticipants[id].Name + ")";
            }
            return name;
        }

        private void OnRandomizeCommand()
        {
            _model.Randomize();
            RefreshGrid();

        }

        private void OnToParticipantsCommand()
        {
            PreviousPage?.Invoke(this, null);
        }

        private void OnToAlgorithmCommand()
        {
            try
            {
                _model.Validate();
                NextPage?.Invoke(this, null);
            }
            catch(Exception)
            {

            }
        }
    }
}

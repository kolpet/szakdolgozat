using System;
using System.Collections.ObjectModel;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Pages
{
    public class ParticipantsViewModel : ViewModelBase, IPageTurn
    {
        private ParticipantsModel _model;

        public DelegateCommand ToSetupCommand { get; private set; }

        public DelegateCommand ToPreferencesCommand { get; private set; }

        public ObservableCollection<ParticipantRow> Group1Participants { get; set; }

        public ObservableCollection<ParticipantRow> Group2Participants { get; set; }

        public Participant SelectedParticipant { get; set; }

        public string Group1Name { get; set; }

        public string Group2Name { get; set; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public ParticipantsViewModel()
        {
            _model = new ParticipantsModel();

            ToSetupCommand = new DelegateCommand(param => OnToSetupCommand());
            ToPreferencesCommand = new DelegateCommand(param => OnToPreferencesCommand());

            RefreshPage();
        }

        public void RefreshPage()
        {
            if(_model.GetContext.SetupChanged)
            {
                _model.Initialize();
            }

            Group1Participants = new ObservableCollection<ParticipantRow>();
            Group2Participants = new ObservableCollection<ParticipantRow>();
            Group1Name = _model.GetContext.Group1Name;
            Group2Name = _model.GetContext.Group2Name;

            foreach(Participant participant in _model.GetContext.Group1Participants)
            {
                ParticipantRow row = new ParticipantRow(participant.ID, participant.Name);
                row.NameChanged += new EventHandler(OnParticipantNameChanged);
                Group1Participants.Add(row);
            }
            foreach(Participant participant in _model.GetContext.Group2Participants)
            {
                ParticipantRow row = new ParticipantRow(participant.ID, participant.Name);
                row.NameChanged += new EventHandler(OnParticipantNameChanged);
                Group2Participants.Add(row);
            }

            OnPropertyChanged("Group1Name");
            OnPropertyChanged("Group2Name");
            OnPropertyChanged("Group1Participants");
            OnPropertyChanged("Group2Participants");
        }

        private void OnParticipantNameChanged(object sender, EventArgs e)
        {
            ParticipantRow row = (ParticipantRow)sender;
            _model.EditParticipant(row.ID, row.Name);
        }

        public void Load()
        {
            _model.Load();
            RefreshPage();
        }

        private void OnToSetupCommand()
        {
            PreviousPage?.Invoke(this, null);
        }

        private void OnToPreferencesCommand()
        {
            try
            {
                _model.Validate();
                NextPage?.Invoke(this, null);
            } catch { }
        }
    }
}

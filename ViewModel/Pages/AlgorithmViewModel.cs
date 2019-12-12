using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Events;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Pages
{
    public class AlgorithmViewModel : ViewModelBase, IPageTurn
    {
        private AlgorithmModel _model;

        private IContext _context;

        private IAlgorithmOptionVisitor _visitor;

        private List<IAlgorithmOptionElement> AlgorithmElements;

        public DelegateCommand DeleteAlgorithmCommand { get; private set; }

        public DelegateCommand NewGaleShapleyAlgorithmCommand { get; private set; }

        public DelegateCommand NewGeneticAlgorithmCommand { get; private set; }

        public DelegateCommand ToPreferencesCommand { get; private set; }

        public DelegateCommand ToRunCommand { get; private set; }

        public ObservableCollection<AlgorithmOptionBase> AlgorithmOptions { get; set; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public AlgorithmViewModel(AlgorithmModel model, IContext context)
        {
            _model = model;
            _context = context;

            DeleteAlgorithmCommand = new DelegateCommand(param => OnDeleteAlgorithmCommand(Convert.ToInt32(param)));
            NewGaleShapleyAlgorithmCommand = new DelegateCommand(param => OnNewGaleShapleyAlgorithmCommand());
            NewGeneticAlgorithmCommand = new DelegateCommand(param => OnNewGeneticAlgorithmCommand());
            ToPreferencesCommand = new DelegateCommand(param => OnToPreferencesCommand());
            ToRunCommand = new DelegateCommand(param => OnToRunCommand());

            AlgorithmElements = new List<IAlgorithmOptionElement>();
            AlgorithmOptions = new ObservableCollection<AlgorithmOptionBase>();
            _visitor = new AlgorithmOptionVisitor();
        }

        public void RefreshPage()
        {
            if(_context.PreferencesChanged)
            {
                _model.Initialize();
            }

            AlgorithmOptions.Clear();
            AlgorithmElements.Clear();
            for(int i = 0; i < _context.GetAlgorithms.Count; i++)
            {
                IAlgorithmData data = _context.GetAlgorithms[i];
                _model.VisitAlgorithm(i, 
                    (x) => RefreshGaleShapley(data.Name, i),
                    (x) => RefreshGenetic(data.Name, i, x.Settings));
            }
            OnPropertyChanged("AlgorithmOptions");
        }

        public void Load()
        {
            _model.Load();
            RefreshPage();
        }

        private void RefreshGaleShapley(string name, int index)
        {
            AlgorithmOptionGaleShapley alg = new AlgorithmOptionGaleShapley(name, index);
            AlgorithmOptions.Add(alg);
            AlgorithmElements.Add(alg);

            UpdateGeneticSettings(alg.Index);
        }

        private void RefreshGenetic(string name, int index, GeneticSettings settings)
        {
            AlgorithmOptionGenetic alg = new AlgorithmOptionGenetic(name, index, ConvertToViewModelSettings(settings));
            alg.Changed += new EventHandler<AlgorithmOptionChangedEventArgs>(AlgorithmOption_Changed);
            AlgorithmOptions.Add(alg);
            AlgorithmElements.Add(alg);

            UpdateGeneticSettings(alg.Index);
        }

        private void OnNewGaleShapleyAlgorithmCommand()
        {
            _model.CreateGaleShapleyAlgorithm();
            AlgorithmOptionGaleShapley alg = new AlgorithmOptionGaleShapley(
                _context.GetAlgorithms.Last().Name,
                AlgorithmOptions.Count()
            );
            AlgorithmOptions.Add(alg);
            AlgorithmElements.Add(alg);

            //UpdateGeneticSettings(alg.Index);
            OnPropertyChanged("AlgorithmOptions");
        }

        private void OnNewGeneticAlgorithmCommand()
        {
            IGeneticSettings settings = _model.CreateGeneticAlgorithm();
            AlgorithmOptionGenetic alg = new AlgorithmOptionGenetic(
                _context.GetAlgorithms.Last().Name,
                AlgorithmOptions.Count(),
                ConvertToViewModelSettings(settings)
            );
            alg.Changed += new EventHandler<AlgorithmOptionChangedEventArgs>(AlgorithmOption_Changed);
            AlgorithmOptions.Add(alg);
            AlgorithmElements.Add(alg);

            //UpdateGeneticSettings(alg.Index);
            OnPropertyChanged("AlgorithmOptions");
        }

        private void AlgorithmOption_Changed(object sender, AlgorithmOptionChangedEventArgs e)
        {
            _model.UpdateName(e.Index, AlgorithmOptions[e.Index].Name);
            UpdateGeneticSettings(e.Index);
        }

        private IGeneticSettings ConvertToViewModelSettings(IGeneticSettings settings)
        {
            IGeneticSettings newSettings = new Settings
            {
                AbsoluteSelection = settings.AbsoluteSelection * 100,
                SelectionRate = settings.SelectionRate * 100,
                MutationChance = settings.MutationChance * 100,
                StablePairWeight = settings.StablePairWeight,
                GroupHappinessWeight = settings.GroupHappinessWeight,
                EgalitarianHappinessWeight = settings.EgalitarianHappinessWeight,
                Generations = settings.Generations,
                Size = settings.Size
            };
            return newSettings;
        }

        private IGeneticSettings ConvertToModelSettings(IGeneticSettings settings)
        {
            IGeneticSettings newSettings = new Settings
            {
                AbsoluteSelection = settings.AbsoluteSelection / 100,
                SelectionRate = settings.SelectionRate / 100,
                MutationChance = settings.MutationChance / 100,
                StablePairWeight = settings.StablePairWeight,
                GroupHappinessWeight = settings.GroupHappinessWeight,
                EgalitarianHappinessWeight = settings.EgalitarianHappinessWeight,
                Generations = settings.Generations,
                Size = settings.Size
            };
            return newSettings;
        }

        private void UpdateGeneticSettings(int index)
        {
            IGeneticSettings settings = _visitor.GetGeneticOption(AlgorithmElements[index]);

            if(settings != null)
            { 
                IGeneticSettings newSettings = ConvertToViewModelSettings(
                    _model.UpdateAlgorithm(index, ConvertToModelSettings(settings)));

                if(settings != newSettings)
                {
                    settings.SelectionRate = newSettings.SelectionRate;
                    settings.AbsoluteSelection = newSettings.AbsoluteSelection;
                    settings.MutationChance = newSettings.MutationChance;
                    settings.StablePairWeight = newSettings.StablePairWeight;
                    settings.GroupHappinessWeight = newSettings.GroupHappinessWeight;
                    settings.EgalitarianHappinessWeight = newSettings.EgalitarianHappinessWeight;
                    settings.Size = newSettings.Size;
                    settings.Generations = newSettings.Generations;
                }
                OnPropertyChanged("AlgorithmOptions");
            }
        }

        private void OnDeleteAlgorithmCommand(int index)
        {
            _model.DeleteAlgorithm(index);
            AlgorithmOptions.RemoveAt(index);
            AlgorithmElements.RemoveAt(index);
            
            for(int i = index; i < AlgorithmOptions.Count(); i++)
            {
                _visitor.ReduceIndex(AlgorithmElements[i]);
            }
            OnPropertyChanged("AlgorithmOptions");
        }

        private void OnToPreferencesCommand()
        {
            PreviousPage?.Invoke(this, null);
        }

        private void OnToRunCommand()
        {
            try
            {
                NextPage?.Invoke(this, null);
            }
            catch(Exception)
            {

            }
        }
    }
}

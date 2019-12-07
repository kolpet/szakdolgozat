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

        public AlgorithmViewModel()
        {
            _model = new AlgorithmModel();

            NewGaleShapleyAlgorithmCommand = new DelegateCommand(param => OnDeleteAlgorithmCommand(Convert.ToInt32(param)));
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
            if(_model.GetContext.PreferencesChanged)
            {
                _model.Initialize();
            }

            AlgorithmOptions.Clear();
            for(int i = 0; i < _model.GetContext.Algorithms.Count; i++)
            {
                AlgorithmData data = _model.GetContext.Algorithms[i];
                AlgorithmVisitorParam visitor = new AlgorithmVisitorParam((x) => RefreshGaleShapley(data.Name, i),
                    (x) => RefreshGenetic(data.Name, i, x.Settings));
                visitor.Visit(data.Element);
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
            AlgorithmOptionGenetic alg = new AlgorithmOptionGenetic(name, index, settings);
            AlgorithmOptions.Add(alg);
            AlgorithmElements.Add(alg);

            UpdateGeneticSettings(alg.Index);
        }

        private void OnNewGaleShapleyAlgorithmCommand()
        {
            _model.CreateGaleShapleyAlgorithm();
            AlgorithmOptionGaleShapley alg = new AlgorithmOptionGaleShapley(
                _model.GetContext.Algorithms.Last().Name,
                AlgorithmOptions.Count()
            );
            AlgorithmOptions.Add(alg);
            AlgorithmElements.Add(alg);

            UpdateGeneticSettings(alg.Index);
            OnPropertyChanged("AlgorithmOptions");
        }

        private void OnNewGeneticAlgorithmCommand()
        {
            GeneticSettings settings = _model.CreateGeneticAlgorithm();
            AlgorithmOptionGenetic alg = new AlgorithmOptionGenetic(
                _model.GetContext.Algorithms.Last().Name,
                AlgorithmOptions.Count(),
                settings
            );
            alg.Changed += new EventHandler<AlgorithmOptionChangedEventArgs>(AlgorithmOption_Changed);
            AlgorithmOptions.Add(alg);
            AlgorithmElements.Add(alg);

            UpdateGeneticSettings(alg.Index);
            OnPropertyChanged("AlgorithmOptions");
        }

        private void AlgorithmOption_Changed(object sender, AlgorithmOptionChangedEventArgs e)
        {
            UpdateGeneticSettings(e.Index);
        }

        private void UpdateGeneticSettings(int index)
        {
            IGeneticSettings settings = _visitor.GetGeneticOption(AlgorithmElements[index]);

            if(settings != null)
            { 
                IGeneticSettings newSettings = _model.UpdateAlgorithm(index, settings);

                if(settings != newSettings)
                {
                    settings.SelectionRate = newSettings.SelectionRate * 100;
                    settings.AbsoluteSelection = newSettings.AbsoluteSelection * 100;
                    settings.MutationChance = newSettings.MutationChance * 100;
                    settings.StablePairWeight = newSettings.StablePairWeight;
                    settings.GroupHappinessWeight = newSettings.GroupHappinessWeight;
                    settings.EgalitarianHappinessWeight = newSettings.EgalitarianHappinessWeight;
                    settings.Size = newSettings.Size;
                    settings.Generations = newSettings.Generations;
                }
                else
                {
                    settings.SelectionRate *= 100;
                    settings.AbsoluteSelection *= 100;
                    settings.MutationChance *= 100;
                }
                OnPropertyChanged("AlgorithmOptions");
            }
        }

        private void OnDeleteAlgorithmCommand(int index)
        {
            //TODO: Fix delete
            _model.DeleteAlgorithm(index);
            AlgorithmOptions.RemoveAt(index);
            AlgorithmElements.RemoveAt(index);
            
            for(int i = index; i < AlgorithmOptions.Count(); i++)
            {
                _visitor.ReduceIndex(AlgorithmElements[i]);
            }
            OnPropertyChanged("AlgorithmOptions");
        }

        private void OnUpdateCommand(int index)
        {
            IGeneticSettings settings = _visitor.GetGeneticOption(AlgorithmElements[index]);

            if(settings != null)
            {
                IGeneticSettings newSettings = _model.UpdateAlgorithm(index, settings);

                if(settings != newSettings)
                {
                    //AlgorithmOptions[index].AlgorithmOption = settings;
                    OnPropertyChanged("AlgorithmOptions");
                }
            }
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

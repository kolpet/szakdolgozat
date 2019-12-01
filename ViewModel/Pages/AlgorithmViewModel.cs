using System;
using System.Collections.ObjectModel;
using System.Linq;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Structures;
using ViewModel.Adapters;

namespace Szakdolgozat.ViewModel.Pages
{
    public class AlgorithmViewModel : ViewModelBase, IPageTurn
    {
        private AlgorithmModel _model;

        public DelegateCommand NewGaleShapleyAlgorithmCommand { get; private set; }

        public DelegateCommand NewGeneticAlgorithmCommand { get; private set; }

        public DelegateCommand ToPreferencesCommand { get; private set; }

        public DelegateCommand ToRunCommand { get; private set; }

        public ObservableCollection<AlgorithmOptionPanel> AlgorithmOptions { get; set; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public AlgorithmViewModel()
        {
            _model = new AlgorithmModel();

            NewGaleShapleyAlgorithmCommand = new DelegateCommand(param => OnNewGaleShapleyAlgorithmCommand());
            NewGeneticAlgorithmCommand = new DelegateCommand(param => OnNewGeneticAlgorithmCommand());
            ToPreferencesCommand = new DelegateCommand(param => OnToPreferencesCommand());
            ToRunCommand = new DelegateCommand(param => OnToRunCommand());

            AlgorithmOptions = new ObservableCollection<AlgorithmOptionPanel>();
        }

        public void RefreshPage()
        {
            _model.Initialize();

            AlgorithmOptions.Clear();
            foreach(AlgorithmData data in _model.GetContext.Algorithms.ToList())
            {
                AlgorithmVisitor visitor = new AlgorithmVisitor(() => OnNewGaleShapleyAlgorithmCommand(), 
                    () => OnNewGeneticAlgorithmCommand());
                visitor.Visit(data.Element);
            }
            OnPropertyChanged("AlgorithmOptions");
        }

        private void NewPanel(IAlgorithmOptionElement alg)
        {
            AlgorithmOptions.Add(new AlgorithmOptionPanel(alg)
            {
                DeleteCommand = new DelegateCommand(param => OnDeleteCommand(Convert.ToInt32(param))),
                UpdateCommand = new DelegateCommand(param => OnUpdateCommand(Convert.ToInt32(param))),
                Index = AlgorithmOptions.Count(),
            });
        }

        private void OnNewGaleShapleyAlgorithmCommand()
        {
            _model.CreateGaleShapleyAlgorithm();
            AlgorithmOptionGaleShapley alg = new AlgorithmOptionGaleShapley();
            NewPanel(alg);
        }

        private void OnNewGeneticAlgorithmCommand()
        {
            GeneticSettings settings = _model.CreateGeneticAlgorithm();
            AlgorithmOptionGenetic alg = new GeneticSettingsAdapter(settings);
            NewPanel(alg);
        }

        private void OnDeleteCommand(int index)
        {
            _model.DeleteAlgorithm(index);
            AlgorithmOptions.RemoveAt(index);
            for(int i = index; i < AlgorithmOptions.Count(); i++)
            {
                AlgorithmOptions[i].Index--;
            }
        }

        private void OnUpdateCommand(int index)
        {
            IAlgorithmOptionVisitor visitor = new AlgorithmOptionVisitor();
            GeneticSettings settings = visitor.GetGeneticSettings(AlgorithmOptions[index]);

            if(settings != null)
            {
                GeneticSettings newSettings = _model.UpdateAlgorithm(index, settings);

                if(settings != newSettings)
                {
                    visitor.SetGeneticSettings(AlgorithmOptions[index], settings);
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
                foreach(AlgorithmOptionPanel algPanel in AlgorithmOptions)
                {
                    OnUpdateCommand(algPanel.Index);
                }
                NextPage?.Invoke(this, null);
            }
            catch(Exception)
            {

            }
        }
    }
}

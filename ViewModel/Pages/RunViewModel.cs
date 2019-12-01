using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Pages
{
    public class RunViewModel : ViewModelBase, IPageTurn
    {
        private RunModel _model;

        public DelegateCommand RunAllCommand { get; private set; }

        public DelegateCommand RunSingleCommand { get; private set; }

        public DelegateCommand ToAlgorithmCommand { get; private set; }

        public ObservableCollection<StableMarriagePanel> Results { get; private set; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public RunViewModel()
        {
            _model = new RunModel();

            RunAllCommand = new DelegateCommand(param => OnRunAllCommand()); 
            RunSingleCommand = new DelegateCommand(param => OnRunSingleCommand(Convert.ToInt32(param))); 
            ToAlgorithmCommand = new DelegateCommand(param => OnToAlgorithmCommand());

            _model.AlgorithmStarted += Model_AlgorithmStarted;
            _model.AlgorithmFinished += Model_AlgorithmFinished;

            Results = new ObservableCollection<StableMarriagePanel>();
        }

        public void RefreshPage()
        {
            if(_model.GetContext.AlgorithmsChanged)
            {
                Results.Clear();
                for(int i = 0; i < _model.GetContext.Algorithms.Count; i++)
                {
                    Results.Add(new StableMarriagePanel(_model.GetContext.Algorithms[i].Name, i)
                    {
                        State = "Futtatható",
                        Time = 0,
                        Runable = true,
                        Done = false,
                    });
                }
            }
        }

        public void Model_AlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            Results[e.Index].State = "Futás alatt";
            Results[e.Index].Runable = false;
            Results[e.Index].Done = false;
        }

        public void Model_AlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            Results[e.Index].State = "Kész";
            Results[e.Index].Runable = true;
            Results[e.Index].Done = true;
            Results[e.Index].StablePairs = e.StablePairs;
            Results[e.Index].GroupHappiness = e.GroupHappiness;
            Results[e.Index].EgalitarianHappiness = e.EgalitarianHappiness;
        }

        private void OnRunAllCommand()
        {
            Task.Run(() => _model.RunAllAlgorithms());
        }

        private void OnRunSingleCommand(int Index)
        {
            Task.Run(() => _model.RunSingleAlgorithm(Index));
        }

        private void OnToAlgorithmCommand()
        {
            _model.StopAll();
            PreviousPage?.Invoke(this, null);
        }

    }
}
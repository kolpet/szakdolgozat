using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Persistence;

namespace Szakdolgozat.Model
{
    public class RunModel : ModelBase
    {
        private List<int> _running;

        private List<Task> _tasks;

        public event EventHandler<AlgorithmEventArgs> AlgorithmStarted;

        public event EventHandler<AlgorithmEventArgs> AlgorithmFinished;

        public RunModel()
        {
            _running = new List<int>();
            _tasks = new List<Task>();
        }

        public void Initialize()
        {
            //TODO: create safe task disposal
            if(Context.AlgorithmsChanged)
            {
                //_running.Clear();
                //_tasks.Clear();
                Context.AlgorithmsChanged = false;
            }
        }

        public async Task RunAllAlgorithms()
        {
            List<Task<int>> tasks = new List<Task<int>>();

            for(int i = 0; i < Context.Algorithms.Count(); i++)
            {
                if(!_running.Contains(i))
                {
                    _running.Add(i);
                    AlgorithmStarted?.Invoke(this, new AlgorithmEventArgs(i));

                    Task<int> task = Task.Run(() => RunAlgorithm(i));
                    tasks.Add(task);
                    _tasks.Add(task);
                }
            }

            while(tasks.Any())
            {
                Task<int> finished = await Task.WhenAny(tasks);
                _running.Remove(finished.Result);
                tasks.Remove(finished);
                _tasks.Remove(finished);
            }
        }

        public async Task RunSingleAlgorithm(int Index)
        {
            if(!_running.Contains(Index))
            {
                _running.Add(Index);
                AlgorithmStarted?.Invoke(this, new AlgorithmEventArgs(Index));

                Task<int> task = Task.Run(() => RunAlgorithm(Index));
                
                _running.Remove(await task);
                _tasks.Remove(task);
            }
        }

        public void StopAll()
        {
            _running.Clear();
            foreach(Task task in _tasks)
            {
                task.Dispose();
            }
            _tasks.Clear();
        }

        private async Task<int> RunAlgorithm(int Index)
        {
            await Task.FromResult(0);
            AlgorithmBase algorithm = Context.Algorithms[Index].Algorithm;
            algorithm.Calculate();

            StablePairsEvaluation stablePairsEvaluation = new StablePairsEvaluation();
            GroupHappinessEvaluation groupHappinessEvaluation = new GroupHappinessEvaluation();
            EgalitarianHappinessEvaluation egalitarianHappinessEvaluation = new EgalitarianHappinessEvaluation();

            AlgorithmFinished?.Invoke(this, new AlgorithmEventArgs(Index, 
                algorithm.Evaluate(stablePairsEvaluation),
                algorithm.Evaluate(groupHappinessEvaluation),
                algorithm.Evaluate(egalitarianHappinessEvaluation)));
            return Index;
        }
    }
}

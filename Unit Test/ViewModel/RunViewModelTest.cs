using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Pages;

namespace Unit_Test.ViewModel
{
    [TestClass]
    public class RunViewModelTest
    {
        private RunViewModel _viewModel;

        private RunModel _model;

        private ModelContext _context;

        [TestMethod]
        public void Initialize()
        {
            NewViewModel();
            Assert.IsNotNull(_viewModel.Results);
        }

        [TestMethod]
        public void RunSingleGaleShapley()
        {
            NewViewModel();
            Assert.IsTrue(_viewModel.Results[0].Runable);
            Assert.IsFalse(_viewModel.Results[0].Done);

            _model.AlgorithmStarted += (object sender, AlgorithmEventArgs e) =>
            {
                Assert.IsFalse(_viewModel.Results[0].Runable);
                Assert.IsFalse(_viewModel.Results[0].Done);
            };
            _model.AlgorithmFinished += (object sender, AlgorithmEventArgs e) =>
            {
                Assert.IsTrue(_viewModel.Results[0].Runable);
                Assert.IsTrue(_viewModel.Results[0].Done);
            };

            Assert.IsTrue(_viewModel.RunSingleCommand.CanExecute(0));
            _viewModel.RunSingleCommand.Execute(0);
        }

        [TestMethod]
        public void RunSingleGenetic()
        {
            NewViewModel();
            Assert.IsTrue(_viewModel.Results[1].Runable);
            Assert.IsFalse(_viewModel.Results[1].Done);

            _model.AlgorithmStarted += (object sender, AlgorithmEventArgs e) =>
            {
                Assert.IsFalse(_viewModel.Results[1].Runable);
                Assert.IsFalse(_viewModel.Results[1].Done);
            };
            _model.AlgorithmFinished += (object sender, AlgorithmEventArgs e) =>
            {
                Assert.IsTrue(_viewModel.Results[1].Runable);
                Assert.IsTrue(_viewModel.Results[1].Done);
            };

            Assert.IsTrue(_viewModel.RunSingleCommand.CanExecute(1));
            _viewModel.RunSingleCommand.Execute(1);
        }

        [TestMethod]
        public async Task RunAll()
        {
            int startedCount = 0;
            int finishedCount = 0;
            NewViewModel();

            _model.AlgorithmStarted += (object sender, AlgorithmEventArgs e) =>
            {
                startedCount++;
                Assert.IsFalse(_viewModel.Results[e.Index].Runable);
                Assert.IsFalse(_viewModel.Results[e.Index].Done);
            };
            _model.AlgorithmFinished += (object sender, AlgorithmEventArgs e) =>
            {
                finishedCount++;
                Assert.IsTrue(_viewModel.Results[e.Index].Runable);
                Assert.IsTrue(_viewModel.Results[e.Index].Done);
            };

            Assert.IsTrue(_viewModel.RunAllCommand.CanExecute(null));
            _viewModel.RunAllCommand.Execute(null);

            int timeout = 5000;
            Task task = Task.Run(async () => { while(startedCount < 2 && finishedCount < 2) await Task.Delay(25); });
            if(await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {

            }
            else
            {
                Assert.Fail("timeout");
            }

        }

        [TestMethod]
        public void RefreshPage()
        {
            NewViewModel();
            GaleShapleyAlgorithm algorithm = new GaleShapleyAlgorithm(_context.StableMarriage);
            _context.Algorithms.Add(new AlgorithmData { Name = "test", Algorithm = algorithm, Element = algorithm });

            _viewModel.RefreshPage();
            Assert.AreEqual(3, _viewModel.Results.Count());
            Assert.IsNotNull(_viewModel.Results[2]);
            Assert.IsTrue(_viewModel.Results[2].Runable);
            Assert.IsFalse(_viewModel.Results[2].Done);
        }

        private void NewViewModel()
        {
            _context = new ModelContext();
            _model = new RunModel(_context);
            _viewModel = new RunViewModel(_model, _context);
            
            SetupModel setupModel = new SetupModel(_context);
            setupModel.Initialize();
            ParticipantsModel participantsModel = new ParticipantsModel(_context);
            participantsModel.Initialize();
            PreferencesModel preferencesModel = new PreferencesModel(_context);
            preferencesModel.Initialize();
            AlgorithmModel algorithmModel = new AlgorithmModel(_context);
            algorithmModel.Initialize();

            algorithmModel.CreateGaleShapleyAlgorithm();
            algorithmModel.CreateGeneticAlgorithm();

            _viewModel.RefreshPage();
        }
    }
}

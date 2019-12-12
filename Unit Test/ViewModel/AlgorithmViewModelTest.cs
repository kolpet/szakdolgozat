using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Pages;
using Szakdolgozat.ViewModel.Structures;

namespace Unit_Test.ViewModel
{
    [TestClass]
    public class AlgorithmViewModelTest
    {
        private AlgorithmViewModel _viewModel;

        private AlgorithmModel _model;

        private ModelContext _context;

        [TestMethod]
        public void Initialize()
        {
            NewViewModel();
            Assert.IsNotNull(_viewModel.AlgorithmOptions);
        }

        [TestMethod]
        public void NewGaleShapleyAlgorithm()
        {
            bool changed = false;
            NewViewModel();
            Assert.IsTrue(_viewModel.NewGaleShapleyAlgorithmCommand.CanExecute(null));
            _viewModel.PropertyChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual(1, _viewModel.AlgorithmOptions.Count());
                Assert.IsInstanceOfType(_viewModel.AlgorithmOptions[0], typeof(AlgorithmOptionGaleShapley));
                Assert.AreEqual(1, _context.Algorithms.Count());
                Assert.IsInstanceOfType(_context.Algorithms[0].Algorithm, typeof(GaleShapleyAlgorithm));
            };

            _viewModel.NewGaleShapleyAlgorithmCommand.Execute(null);
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void NewGeneticAlgorithm()
        {
            bool changed = false;
            NewViewModel();
            Assert.IsTrue(_viewModel.NewGeneticAlgorithmCommand.CanExecute(null));
            _viewModel.PropertyChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual(1, _viewModel.AlgorithmOptions.Count());
                Assert.IsInstanceOfType(_viewModel.AlgorithmOptions[0], typeof(AlgorithmOptionGenetic));
                Assert.AreEqual(1, _context.Algorithms.Count());
                Assert.IsInstanceOfType(_context.Algorithms[0].Algorithm, typeof(GeneticAlgorithm));
            };

            _viewModel.NewGeneticAlgorithmCommand.Execute(null);
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void EditGaleShapleyAlgorithm()
        {
            bool changed = false;
            NewViewModel();
            _viewModel.NewGeneticAlgorithmCommand.Execute(null);

            _viewModel.AlgorithmOptions[0].Changed += (s, e) =>
            {
                changed = true;
                Assert.AreEqual("test", _viewModel.AlgorithmOptions[0].Name);
                Assert.AreEqual("test", _context.Algorithms[0].Name);
            };

            _viewModel.AlgorithmOptions[0].Name = "test";
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void EditGeneticAlgorithm()
        {
            int changedCount = 0;
            NewViewModel();
            _viewModel.NewGeneticAlgorithmCommand.Execute(null);

            _viewModel.AlgorithmOptions[0].Changed += (s, e) => changedCount++;

            AlgorithmOptionVisitorParam visitor = new AlgorithmOptionVisitorParam(
                (x) => Assert.Fail(),
                (x) =>
                {
                    x.AbsoluteSelection = 0;
                    x.SelectionRate = 0;
                    x.MutationChance = 0;
                });
            visitor.Visit(_viewModel.AlgorithmElements[0]);

            _viewModel.AlgorithmOptions[0].Name = "test";
            Assert.AreEqual(4, changedCount);
        }

        [TestMethod]
        public void DeleteAlgorithm()
        {
            bool changed = false;
            NewViewModel();
            Assert.IsTrue(_viewModel.DeleteAlgorithmCommand.CanExecute(0));
            _viewModel.NewGaleShapleyAlgorithmCommand.Execute(null);
            _viewModel.NewGaleShapleyAlgorithmCommand.Execute(null);

            _viewModel.PropertyChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual(1, _viewModel.AlgorithmOptions.Count());
                Assert.AreEqual(0, _viewModel.AlgorithmOptions[0].Index);
            };

            _viewModel.DeleteAlgorithmCommand.Execute(0);
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void RefreshPage()
        {
            NewViewModel();
            _model.CreateGaleShapleyAlgorithm();
            _model.CreateGeneticAlgorithm();

            _viewModel.RefreshPage();
            Assert.AreEqual(2, _viewModel.AlgorithmOptions.Count());
            Assert.IsInstanceOfType(_viewModel.AlgorithmOptions[0], typeof(AlgorithmOptionGaleShapley));
            Assert.IsInstanceOfType(_viewModel.AlgorithmOptions[1], typeof(AlgorithmOptionGenetic));
        }

        private void NewViewModel()
        {
            _context = new ModelContext();
            _model = new AlgorithmModel(_context);
            _viewModel = new AlgorithmViewModel(_model, _context);

            SetupModel setupModel = new SetupModel(_context);
            setupModel.Initialize();
            ParticipantsModel participantsModel = new ParticipantsModel(_context);
            participantsModel.Initialize();
            PreferencesModel preferencesModel = new PreferencesModel(_context);
            preferencesModel.Initialize();

            _viewModel.RefreshPage();
        }
    }
}

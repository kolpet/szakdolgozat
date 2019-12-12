using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Pages;
using Szakdolgozat.ViewModel.Structures;

namespace Unit_Test.ViewModel
{
    [TestClass]
    public class PreferencesViewModelTest
    {
        private PreferencesViewModel _viewModel;

        private PreferencesModel _model;

        private ModelContext _context;

        [TestMethod]
        public void Initialize()
        {
            NewViewModel();
            Assert.IsNotNull(_viewModel.PreferenceGridColumns);
            Assert.IsNotNull(_viewModel.PreferenceGridRows);
            Assert.IsNotNull(_viewModel.PreferenceGrid);
            Assert.IsNotNull(_viewModel.ParticipantList);
        }

        [TestMethod]
        public void PreferenceChanged()
        {
            bool changed = false;
            NewViewModel();
            _viewModel.PreferenceGrid[0].SelectedChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual(2, _viewModel.PreferenceGrid[0].SelectedIndex);
                Assert.AreEqual(_viewModel.PreferenceGrid[0].PreferenceValues[2], _context.Priorities[0][0]);
            };
            _viewModel.PreferenceGrid[0].SelectedIndex = 2;
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void RefreshPage()
        {
            int changedCount = 0;
            NewViewModel();
            _viewModel.PreferenceGrid[0].SelectedChanged += (s, e) => changedCount++;
            _context.Priorities[0].Reverse();

            _viewModel.RefreshPage();
            for(int i = 0; i < 5; i++)
            {
                Assert.AreEqual(4 - i, _viewModel.PreferenceGrid[i].SelectedIndex);
            }
        }

        [TestMethod]
        public void Randomize()
        {
            NewViewModel();
            List<PreferenceCell> cells = new List<PreferenceCell>();
            foreach(PreferenceCell cell in _viewModel.PreferenceGrid)
            {
                cells.Add(cell);
            }
            Assert.IsTrue(_viewModel.RandomizeCommand.CanExecute(null));

            _viewModel.RandomizeCommand.Execute(null);
            if(_context.Priorities[9][4] == 9) _context.Priorities[9][4] = 8;

            bool equal = true;
            for(int i = 0; i < _viewModel.PreferenceGrid.Count() && equal; i++)
            {
                if(_viewModel.PreferenceGrid[i].SelectedIndex != cells[i].SelectedIndex)
                {
                    equal = false;
                }
            }
            Assert.IsFalse(equal);
        }

        private void NewViewModel()
        {
            _context = new ModelContext();
            _model = new PreferencesModel(_context);
            _viewModel = new PreferencesViewModel(_model, _context);

            SetupModel setupModel = new SetupModel(_context);
            setupModel.Initialize();
            ParticipantsModel participantsModel = new ParticipantsModel(_context);
            participantsModel.Initialize();

            _viewModel.RefreshPage();
        }
    }
}

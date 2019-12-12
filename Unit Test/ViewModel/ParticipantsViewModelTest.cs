using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model;
using Szakdolgozat.ViewModel.Pages;

namespace Unit_Test.ViewModel
{
    [TestClass]
    public class ParticipantsViewModelTest
    {
        private ParticipantsViewModel _viewModel;

        private ParticipantsModel _model;

        private ModelContext _context;

        [TestMethod]
        public void Initialize()
        {
            NewViewModel();
            Assert.IsNotNull(_viewModel.Group1Name);
            Assert.IsNotNull(_viewModel.Group2Name);
            Assert.IsNotNull(_viewModel.Group1Participants);
            Assert.IsNotNull(_viewModel.Group2Participants);
        }

        [TestMethod]
        public void Group1ParticipantChanged()
        {
            bool changed = false;
            NewViewModel();
            _viewModel.Group1Participants[0].NameChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual("test", _viewModel.Group1Participants[0].Name);
                Assert.AreEqual("test", _context.Participants[_viewModel.Group1Participants[0].ID].Name);
            };
            _viewModel.Group1Participants[0].Name = "test";
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void Group2ParticipantChanged()
        {
            bool changed = false;
            NewViewModel();
            _viewModel.Group2Participants[0].NameChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual("test", _viewModel.Group2Participants[0].Name);
                Assert.AreEqual("test", _context.Participants[_viewModel.Group2Participants[0].ID].Name);
            };
            _viewModel.Group2Participants[0].Name = "test";
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void RefreshPage()
        {
            NewViewModel();
            _context.Participants[_viewModel.Group1Participants[0].ID].Name = "test";
            _context.Participants[_viewModel.Group2Participants[0].ID].Name = "test";

            _viewModel.RefreshPage();
            Assert.AreEqual("test", _viewModel.Group1Participants[0].Name);
            Assert.AreEqual("test", _viewModel.Group2Participants[0].Name);
        }

        private void NewViewModel()
        {
            _context = new ModelContext();
            _model = new ParticipantsModel(_context);
            _viewModel = new ParticipantsViewModel(_model, _context);

            SetupModel setupModel = new SetupModel(_context);
            setupModel.Initialize();

            _viewModel.RefreshPage();
        }
    }
}

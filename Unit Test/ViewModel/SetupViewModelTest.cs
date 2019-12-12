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
    public class SetupViewModelTest
    {
        private SetupViewModel _viewModel;

        private SetupModel _model;

        private ModelContext _context;

        [TestMethod]
        public void Initialize()
        {
            NewViewModel();
            Assert.IsNotNull(_viewModel.Group1Name);
            Assert.IsNotNull(_viewModel.Group2Name);
            Assert.IsNotNull(_viewModel.ParticipantNumber);
        }

        [TestMethod]
        public void Group1NameChanged()
        {
            bool changed = false;
            NewViewModel();
            _viewModel.PropertyChanged += (s, e) =>
            {
                 changed = true;
                Assert.AreEqual("Group1Name", e.PropertyName);
                Assert.AreEqual("test", _viewModel.Group1Name);
                Assert.AreEqual("test", _context.Group1Name);
            };
            _viewModel.Group1Name = "test";
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void Group2NameChanged()
        {
            bool changed = false;
            NewViewModel();
            _viewModel.PropertyChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual("Group2Name", e.PropertyName);
                Assert.AreEqual("test", _viewModel.Group2Name);
                Assert.AreEqual("test", _context.Group2Name);
            };
            _viewModel.Group2Name = "test";
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void SizeChanged()
        {
            bool changed = false;
            NewViewModel();
            _viewModel.PropertyChanged += (s, e) =>
            {
                changed = true;
                Assert.AreEqual("ParticipantNumber", e.PropertyName);
                Assert.AreEqual(20, _viewModel.ParticipantNumber);
                Assert.AreEqual(20, _context.TotalSize);
            };
            _viewModel.ParticipantNumber = 20;
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void RefreshPage()
        {
            NewViewModel();
            _context.Group1Name = "test";
            _context.Group2Name = "test2";
            _context.TotalSize = 20;

            _viewModel.RefreshPage();
            Assert.AreEqual("test", _viewModel.Group1Name);
            Assert.AreEqual("test2", _viewModel.Group2Name);
            Assert.AreEqual(20, _viewModel.ParticipantNumber);
        }

        private void NewViewModel()
        {
            _context = new ModelContext();
            _model = new SetupModel(_context);
            _viewModel = new SetupViewModel(_model, _context);
            _viewModel.RefreshPage();
        }
    }
}

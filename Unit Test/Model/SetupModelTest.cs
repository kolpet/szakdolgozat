using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Persistence;
using Szakdolgozat.Persistence.Structures;

namespace Unit_Test.Model
{
    [TestClass]
    public class SetupModelTest
    {
        private SetupModel _model;

        private ModelContext _context;

        [TestMethod]
        public void ChangeValues()
        {
            string group1Name = "test1";
            string group2Name = "test2";
            int groupSize = 42;

            NewModel();

            _model.ChangeGroup1Name(group1Name);
            _model.ChangeGroup2Name(group2Name);
            _model.ChangeParticipantNumber(groupSize);

            Assert.AreEqual(group1Name, _context.Group1Name);
            Assert.AreEqual(group2Name, _context.Group2Name);
            Assert.AreEqual(groupSize, _context.TotalSize);
            Assert.IsTrue(_model.IsValid);
        }

        [TestMethod]
        public void EmptyName()
        {
            string oldName;

            NewModel();

            oldName = _context.Group1Name;
            try
            {
                _model.ChangeGroup1Name("");
                Assert.Fail("Nem lehet üres csoportnév");
            }
            catch(Exception)
            {
                Assert.AreEqual(oldName, _context.Group1Name);
                Assert.IsFalse(_model.IsValid);
            }

            oldName = _context.Group2Name;
            try
            {
                _model.ChangeGroup2Name("");
                Assert.Fail("Nem lehet üres csoportnév");
            }
            catch(Exception)
            {
                Assert.AreEqual(oldName, _context.Group2Name);
                Assert.IsFalse(_model.IsValid);
            }
        }

        [TestMethod]
        public void EqualNames()
        {
            string name = "test";
            string oldName;

            NewModel();

            _model.ChangeGroup1Name(name);
            oldName = _context.Group2Name;
            try
            {
                _model.ChangeGroup2Name(name);
                Assert.Fail("Nem lehetnek megegyező nevűek a csoportok");
            }
            catch(Exception)
            {
                Assert.AreEqual(oldName, _context.Group2Name);
                Assert.IsFalse(_model.IsValid);
            }

            name = "test2";
            NewModel();

            _model.ChangeGroup2Name(name);
            oldName = _context.Group1Name;
            try
            {
                _model.ChangeGroup1Name(name);
                Assert.Fail("Nem lehetnek megegyező nevűek a csoportok");
            }
            catch(Exception)
            {
                Assert.AreEqual(oldName, _context.Group1Name);
                Assert.IsFalse(_model.IsValid);
            }
        }

        [TestMethod]
        public void WrongSize()
        {
            int size;

            NewModel();
            size = _context.TotalSize;

            try
            {
                _model.ChangeParticipantNumber(-1);
                Assert.Fail("Nem lehet üres a csoport");
            }
            catch(Exception)
            {
                Assert.AreEqual(size, _context.TotalSize);
                Assert.IsFalse(_model.IsValid);
            }

            try
            {
                _model.ChangeParticipantNumber(3);
                Assert.Fail("Csak páros számú résztvevő lehet");
            }
            catch(Exception)
            {
                Assert.AreEqual(size, _context.TotalSize);
                Assert.IsFalse(_model.IsValid);
            }
        }

        [TestMethod]
        public void Load()
        {
            string newName1 = "test";
            string newName2 = "test2";
            int newSize = 20;

            MockPersistence mockPersistence = new MockPersistence
            {
                MockData = new SaveData
                {
                    Group1Name = newName1,
                    Group2Name = newName2,
                    Participants = Enumerable.Repeat<UnitSave>(null, newSize).ToList()
                }
            };

            NewModel();
            _context.Persistence = mockPersistence;
            _context.Persistence.Load("");

            _model.Load();
            Assert.AreEqual(newName1, _context.Group1Name);
            Assert.AreEqual(newName2, _context.Group2Name);
            Assert.AreEqual(newSize, _context.TotalSize);
        }

        private void NewModel()
        {
            _context = new ModelContext();
            _model = new SetupModel(_context);
            _model.Initialize();
            _context.SetupChanged = false;
        }
    }
}

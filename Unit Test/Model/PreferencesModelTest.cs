using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;
using Szakdolgozat.Persistence.Structures;

namespace Unit_Test.Model
{
    [TestClass]
    public class PreferencesModelTest
    {
        private PreferencesModel _model;

        private ModelContext _context;

        /// <summary>
        /// Az inicializálás ellenőrzése
        /// </summary>
        [TestMethod]
        public void Initialize()
        {
            NewModel();
            IsValid();
        }

        /// <summary>
        /// Egy preferencia frissítésének ellenőrzése
        /// </summary>
        [TestMethod]
        public void EditPreference()
        {
            NewModel();

            _model.EditPreference(0, 0, 3);
            Assert.AreEqual(3, _context.Priorities[0][0]);
            Assert.IsTrue(_context.PreferencesChanged);
        }

        /// <summary>
        /// Hibás preferenciák ellenőrzése
        /// </summary>
        [TestMethod]
        public void BadPreferences()
        {
            NewModel();

            _model.EditPreference(0, 0, 3);
            try
            {
                _model.Validate();
                Assert.Fail();
            }
            catch(Exception)
            {

            }
            Assert.IsTrue(_context.PreferencesChanged);
        }

        [TestMethod]
        public void Randomize()
        {
            NewModel();

            _model.Randomize();
            IsValid();
            Assert.IsTrue(_context.ParticipantsChanged);
        }

        /// <summary>
        /// Betöltés ellenőrzése
        /// </summary>
        [TestMethod]
        public void Load()
        {
            NewModel();

            List<PreferenceSave> preferences = new List<PreferenceSave>();
            foreach(Participant participant in _context.Group1Participants)
            {
                preferences.Add(new PreferenceSave
                {
                    Id = participant.ID,
                    Preferences = _context.Group2Participants.Select(x => x.ID).Reverse().ToList()
                });
            }
            foreach(Participant participant in _context.Group2Participants)
            {
                preferences.Add(new PreferenceSave
                {
                    Id = participant.ID,
                    Preferences = _context.Group1Participants.Select(x => x.ID).Reverse().ToList()
                });
            }
            MockPersistence mockPersistence = new MockPersistence
            {
                MockData = new SaveData
                {
                    Preferences = preferences
                }
            };

            _context.Persistence = mockPersistence;
            _context.Persistence.Load("");

            _model.Load();
            for(int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, _context.Participants[i].ID);
                for(int j = 0; j < 5; j++)
                {
                    if(i < 5)
                    {
                        Assert.AreEqual(9 - j, _context.Priorities[i][j]);
                    }
                    else
                    {
                        Assert.AreEqual(4 - j, _context.Priorities[i][j]);
                    }
                }
            }
            Assert.IsTrue(_context.ParticipantsChanged);
        }

        /// <summary>
        /// Create new model
        /// </summary>
        private void NewModel()
        {
            List<Participant> participants = new List<Participant>();
            for(int i = 0; i < 5; i++)
            {
                participants.Add(new Participant(i, "test" + i, MarriageGroup.Group1));
            }
            for(int i = 5; i < 10; i++)
            {
                participants.Add(new Participant(i, "test" + i, MarriageGroup.Group2));
            }

            _context = new ModelContext
            {
                TotalSize = 10,
                Participants = participants,
                SetupChanged = true,
                ParticipantsChanged = true
            };
            _model = new PreferencesModel(_context);
            _model.Initialize();
            _context.PreferencesChanged = false;
        }

        private void IsValid()
        {
            List<int> participants = new List<int>();

            foreach(KeyValuePair<int, UnitSet> priority in _context.Priorities)
            {
                participants.Add(priority.Key);
                List<int> priorityList = new List<int>();
                foreach(int id in priority.Value)
                {
                    Assert.IsFalse(priorityList.Contains(id));
                    priorityList.Add(id);
                }
                Assert.AreEqual(_context.GroupSize, priorityList.Count());
            }

            Assert.AreEqual(_context.TotalSize, participants.Count());
            Assert.IsTrue(participants.Distinct().Count() == participants.Count());
        }
    }
}

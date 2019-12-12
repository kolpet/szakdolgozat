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
    public class ParticipantsModelTest
    {
        private ParticipantsModel _model;

        private ModelContext _context;

        [TestMethod]
        public void Initialize()
        {
            int group1Count = 0;
            int group2Count = 0;
            NewModel();

            foreach(Participant participant in _context.Participants)
            {
                if(participant.Group == MarriageGroup.Group1)
                {
                    group1Count++;
                }
                else if(participant.Group == MarriageGroup.Group2)
                {
                    group2Count++;
                }
            }

            Assert.AreEqual(_context.TotalSize, group1Count + group2Count);
            Assert.IsTrue(group1Count == group2Count);
        }

        [TestMethod]
        public void EditParticipant()
        {
            string name = "test";

            NewModel();

            _model.EditParticipant(0, name);
            Assert.AreEqual(name, _context.Participants[0].Name);
            Assert.IsTrue(_context.ParticipantsChanged);
        }

        [TestMethod]
        public void Load()
        {
            NewModel();

            List<UnitSave> participants = new List<UnitSave>();
            for(int i = 0; i < 5; i++)
            {
                participants.Add(new UnitSave { Id = i, Name = "test" + i, Group = MarriageGroup.Group1 });
            }
            for(int i = 5; i < 10; i++)
            {
                participants.Add(new UnitSave { Id = i, Name = "test" + i, Group = MarriageGroup.Group2 });
            }

            MockPersistence mockPersistence = new MockPersistence
            {
                MockData = new SaveData
                {
                    Participants = participants
                }
            };

            _context.Persistence = mockPersistence;
            _context.Persistence.Load("");

            _model.Load();
            for(int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, _context.Participants[i].ID);
                Assert.AreEqual("test" + i, _context.Participants[i].Name);
                Assert.AreEqual(i < 5 ? MarriageGroup.Group1 : MarriageGroup.Group2, _context.Participants[i].Group);
            }
            Assert.IsTrue(_context.ParticipantsChanged);
        }

        /// <summary>
        /// Create new model
        /// </summary>
        private void NewModel()
        {
            _context = new ModelContext
            {
                TotalSize = 10,
                SetupChanged = true
            };
            _model = new ParticipantsModel(_context);
            _model.Initialize();
            _context.ParticipantsChanged = false;
        }
    }
}

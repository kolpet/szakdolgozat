using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Persistence;
using Szakdolgozat.Persistence.Structures;

namespace Unit_Test.Model
{
    [TestClass]
    public class AlgorithmModelTest
    {
        private AlgorithmModel _model;

        private ModelContext _context;

        private AlgorithmVisitor _visitor;

        private AlgorithmVisitorParam _visitorParam;

        private GeneticSettings _settings;

        [TestInitialize]
        public void TestInitialize()
        {
            _settings = new GeneticSettings
            {
                SelectionRate = 0.1,
                AbsoluteSelection = 0.2,
                MutationChance = 0.3,
                StablePairWeight = 1,
                GroupHappinessWeight = 2,
                EgalitarianHappinessWeight = 3,
                Size = 200,
                Generations = 200
            };
        }

        [TestMethod]
        public void Initialize()
        {
            NewModel();

            Assert.AreEqual(0, _context.Algorithms.Count());
            Assert.IsNotNull(_context.StableMarriage);
            Assert.IsTrue(_context.AlgorithmsChanged);
        }

        [TestMethod]
        public void CreateGaleShapley()
        {
            NewModel();

            _model.CreateGaleShapleyAlgorithm();
            Assert.AreEqual(1, _context.Algorithms.Count());

            _visitor = new AlgorithmVisitor(null, () => Assert.Fail());
            _visitor.Visit(_context.Algorithms[0].Element);
            Assert.IsTrue(_context.AlgorithmsChanged);
        }

        [TestMethod]
        public void CreateGenetic()
        {
            NewModel();

            GeneticSettings settings = _model.CreateGeneticAlgorithm();
            Assert.AreEqual(1, _context.Algorithms.Count());

            _visitorParam = new AlgorithmVisitorParam((x) => Assert.Fail(), 
                (x) => Assert.AreEqual(settings, x.Settings));
            _visitorParam.Visit(_context.Algorithms[0].Element);
            Assert.IsTrue(_context.AlgorithmsChanged);
        }

        [TestMethod]
        public void UpdateName()
        {
            NewModel();

            _model.CreateGaleShapleyAlgorithm();
            _model.UpdateName(0, "test");
            Assert.AreEqual("test", _context.Algorithms[0].Name);
            Assert.IsTrue(_context.AlgorithmsChanged);
        }

        [TestMethod]
        public void UpdateAlgorithm()
        {
            NewModel();

            _model.CreateGaleShapleyAlgorithm();
            try
            {
                _model.UpdateAlgorithm(0, _settings);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }

            _model.CreateGeneticAlgorithm();
            try
            {
                _model.UpdateAlgorithm(1, _settings);
                _visitorParam = new AlgorithmVisitorParam(null,
                    (x) =>
                    {
                        Assert.AreEqual(x.Settings.AbsoluteSelection, _settings.AbsoluteSelection);
                        Assert.AreEqual(x.Settings.SelectionRate, _settings.SelectionRate);
                        Assert.AreEqual(x.Settings.MutationChance, _settings.MutationChance);
                        Assert.AreEqual(x.Settings.StablePairWeight, _settings.StablePairWeight);
                        Assert.AreEqual(x.Settings.GroupHappinessWeight, _settings.GroupHappinessWeight);
                        Assert.AreEqual(x.Settings.EgalitarianHappinessWeight, _settings.EgalitarianHappinessWeight);
                        Assert.AreEqual(x.Settings.Generations, _settings.Generations);
                        Assert.AreEqual(x.Settings.Size, _settings.Size);
                    }); 
                _visitorParam.Visit(_context.Algorithms[1].Element);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DeleteAlgorithm()
        {
            NewModel();

            _model.CreateGaleShapleyAlgorithm();
            _model.CreateGeneticAlgorithm();

            try
            {
                _model.DeleteAlgorithm(0);
                Assert.AreEqual(1, _context.Algorithms.Count());
                _model.DeleteAlgorithm(0);
                Assert.AreEqual(0, _context.Algorithms.Count());
                _model.DeleteAlgorithm(0);
                Assert.AreEqual(0, _context.Algorithms.Count());
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Load()
        {
            List<AlgorithmSaveBase> algorithms = new List<AlgorithmSaveBase>
            {
                new GaleShapleyAlgorithmSave
                {
                    AlgorithmName = "galeshapley"
                },
                new GeneticAlgorithmSave
                {
                    AlgorithmName = "genetic",
                    AbsoluteSelection = _settings.AbsoluteSelection,
                    SelectionRate = _settings.SelectionRate,
                    MutationChance = _settings.MutationChance,
                    StablePairWeight = _settings.StablePairWeight,
                    GroupHappinessWeight = _settings.GroupHappinessWeight,
                    EgalitarianHappinessWeight = _settings.EgalitarianHappinessWeight,
                    Generations = _settings.Generations,
                    Size = _settings.Size
                }
            };
            MockPersistence mockPersistence = new MockPersistence
            {
                MockData = new SaveData
                {
                    Algorithms = algorithms
                }
            };

            NewModel();

            _context.Persistence = mockPersistence;
            _context.Persistence.Load("");

            _model.Load();
            Assert.AreEqual(2, _context.Algorithms.Count());

            Assert.AreEqual("galeshapley", _context.Algorithms[0].Name);
            _visitor = new AlgorithmVisitor(null,
                () => Assert.Fail());
            _visitor.Visit(_context.Algorithms[0].Element);

            Assert.AreEqual("genetic", _context.Algorithms[1].Name);
            _visitorParam = new AlgorithmVisitorParam((x) => Assert.Fail(),
                (x) =>
                {
                    Assert.AreEqual(x.Settings.AbsoluteSelection,  _settings.AbsoluteSelection);
                    Assert.AreEqual(x.Settings.SelectionRate, _settings.SelectionRate);
                    Assert.AreEqual(x.Settings.MutationChance, _settings.MutationChance);
                    Assert.AreEqual(x.Settings.StablePairWeight, _settings.StablePairWeight);
                    Assert.AreEqual(x.Settings.GroupHappinessWeight, _settings.GroupHappinessWeight);
                    Assert.AreEqual(x.Settings.EgalitarianHappinessWeight, _settings.EgalitarianHappinessWeight);
                    Assert.AreEqual(x.Settings.Generations, _settings.Generations);
                    Assert.AreEqual(x.Settings.Size, _settings.Size);
                });
            _visitorParam.Visit(_context.Algorithms[1].Element);
        }

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
                ParticipantsChanged = true,
                PreferencesChanged = true,
            };

            _context.Priorities = new Priorities(new Dictionary<int, UnitSet>());
            foreach(Participant participant in _context.Group1Participants)
            {
                _context.Priorities[participant.ID] = new UnitSet(_context.Group2Participants.Select(x => x.ID).ToList());
            }
            foreach(Participant participant in _context.Group2Participants)
            {
                _context.Priorities[participant.ID] = new UnitSet(_context.Group1Participants.Select(x => x.ID).ToList());
            }

            _model = new AlgorithmModel(_context);
            _model.Initialize();
            _context.AlgorithmsChanged = false;
        }
    }
}

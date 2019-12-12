using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Persistence;

namespace Unit_Test.Model
{
    [TestClass]
    public class AppModelTest
    {
        private AppModel _model;

        private ModelContext _context;

        private MockPersistence _persistence;

        [TestMethod]
        public void Initialize()
        {
            NewModel();
            Assert.AreEqual(_context, _model.GetContext);
        }

        [TestMethod]
        public void NewSetupModel()
        {
            NewModel();
            SetupModel setupModel = _model.NewSetupModel();
            setupModel.Initialize();
            _context.SetupChanged = false;

            setupModel.ChangeGroup1Name("test1");
            setupModel.ChangeGroup2Name("test2");
            setupModel.ChangeParticipantNumber(20);

            Assert.AreEqual("test1", _context.Group1Name);
            Assert.AreEqual("test2", _context.Group2Name);
            Assert.AreEqual(20, _context.TotalSize);
            Assert.IsTrue(setupModel.IsValid);
        }

        [TestMethod]
        public void NewParticipantsModel()
        {
            NewModel();
            SetupModel setupModel = _model.NewSetupModel();
            setupModel.Initialize();
            ParticipantsModel participantsModel = _model.NewParticipantsModel();
            participantsModel.Initialize();
            _context.ParticipantsChanged = false;

            participantsModel.EditParticipant(0, "test");
            Assert.AreEqual("test", _context.Participants[0].Name);
            Assert.IsTrue(_context.ParticipantsChanged);
        }

        [TestMethod]
        public void NewPreferencesModel()
        {
            NewModel();
            SetupModel setupModel = _model.NewSetupModel();
            setupModel.Initialize();
            ParticipantsModel participantsModel = _model.NewParticipantsModel();
            participantsModel.Initialize();
            PreferencesModel preferencesModel = _model.NewPreferencesModel();
            preferencesModel.Initialize();
            _context.PreferencesChanged = false;

            preferencesModel.EditPreference(0, 0, 3);
            Assert.AreEqual(3, _context.Priorities[0][0]);
            Assert.IsTrue(_context.PreferencesChanged);
        }

        [TestMethod]
        public void NewAlgorithmModel()
        {
            NewModel();
            SetupModel setupModel = _model.NewSetupModel();
            setupModel.Initialize();
            ParticipantsModel participantsModel = _model.NewParticipantsModel();
            participantsModel.Initialize();
            PreferencesModel preferencesModel = _model.NewPreferencesModel();
            preferencesModel.Initialize();
            AlgorithmModel algorithmModel = _model.NewAlgorithmModel();
            algorithmModel.Initialize();
            _context.AlgorithmsChanged = false;

            algorithmModel.CreateGaleShapleyAlgorithm();
            Assert.AreEqual(1, _context.Algorithms.Count());

            AlgorithmVisitor _visitor = new AlgorithmVisitor(null, () => Assert.Fail());
            _visitor.Visit(_context.Algorithms[0].Element);
            Assert.IsTrue(_context.AlgorithmsChanged);
        }

        [TestMethod]
        public void NewRunModel()
        {
            StablePairsEvaluation stablePairsEvaluation = new StablePairsEvaluation();
            GroupHappinessEvaluation groupHappinessEvaluation = new GroupHappinessEvaluation();
            EgalitarianHappinessEvaluation egalitarianHappinessEvaluation = new EgalitarianHappinessEvaluation();

            NewModel();
            SetupModel setupModel = _model.NewSetupModel();
            setupModel.Initialize();
            ParticipantsModel participantsModel = _model.NewParticipantsModel();
            participantsModel.Initialize();
            PreferencesModel preferencesModel = _model.NewPreferencesModel();
            preferencesModel.Initialize();
            AlgorithmModel algorithmModel = _model.NewAlgorithmModel();
            algorithmModel.Initialize();
            RunModel runModel = _model.NewRunModel();
            runModel.Initialize();

            int receivedEvents = 0;
            Task task;
            NewModel();

            runModel.AlgorithmStarted += (object sender, AlgorithmEventArgs e) => receivedEvents++;
            runModel.AlgorithmFinished += (object sender, AlgorithmEventArgs e) =>
            {
                receivedEvents++;
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(stablePairsEvaluation), e.StablePairs);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(groupHappinessEvaluation), e.GroupHappiness);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(egalitarianHappinessEvaluation), e.EgalitarianHappiness);
            };
            task = Task.Run(async () => {
                await runModel.RunSingleAlgorithm(0);
                Assert.AreEqual(2, receivedEvents);
            });
        }

        [TestMethod]
        public void SaveData()
        {
            NewModel();
            SetupModel setupModel = new SetupModel(_context);
            setupModel.Initialize();
            ParticipantsModel participantsModel = new ParticipantsModel(_context);
            participantsModel.Initialize();
            PreferencesModel preferencesModel = new PreferencesModel(_context);
            preferencesModel.Initialize();
            AlgorithmModel algorithmModel = new AlgorithmModel(_context);
            algorithmModel.Initialize();

            _model.SaveAsData("");
            Assert.IsNotNull(_persistence.MockData);
            Assert.IsNotNull(_persistence.MockData.Group1Name);
            Assert.IsNotNull(_persistence.MockData.Group2Name);
            Assert.IsNotNull(_persistence.MockData.Participants);
            Assert.IsNotNull(_persistence.MockData.Preferences);
            Assert.IsNotNull(_persistence.MockData.Algorithms);
        }

        [TestMethod]
        public void LoadData()
        {
            SaveData data;

            NewModel();
            SetupModel setupModel = new SetupModel(_context);
            setupModel.Initialize();
            ParticipantsModel participantsModel = new ParticipantsModel(_context);
            participantsModel.Initialize();
            PreferencesModel preferencesModel = new PreferencesModel(_context);
            preferencesModel.Initialize();
            AlgorithmModel algorithmModel = new AlgorithmModel(_context);
            algorithmModel.Initialize();

            _model.SaveAsData("");
            data = _persistence.MockData;

            NewModel();
            _persistence.MockData = data;
            _model.LoadData("");
            Assert.AreEqual(data, _persistence.Data);
        }

        private void NewModel()
        {
            _persistence = new MockPersistence();
            _context = new ModelContext();
            _model = new AppModel(_persistence, _context);
        }
    }
}

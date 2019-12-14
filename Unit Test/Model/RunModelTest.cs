using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szakdolgozat.Common;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Model.Structures;

namespace Unit_Test.Model
{
    [TestClass]
    public class RunModelTest
    {
        private RunModel _model;

        private ModelContext _context;

        private StablePairsEvaluation stablePairsEvaluation;

        private GroupHappinessEvaluation groupHappinessEvaluation;

        private EgalitarianHappinessEvaluation egalitarianHappinessEvaluation;

        [TestInitialize]
        public void TestInitialize()
        {
            stablePairsEvaluation = new StablePairsEvaluation();
            groupHappinessEvaluation = new GroupHappinessEvaluation();
            egalitarianHappinessEvaluation = new EgalitarianHappinessEvaluation();
        }

        /*[TestMethod]
        public void Initialize()
        {
            NewModel();
        }*/

        [TestMethod]
        public void RunGaleShapley()
        {
            int receivedEvents = 0;
            Task task;
            NewModel();

            _model.AlgorithmStarted += (object sender, AlgorithmEventArgs e) => receivedEvents++;
            _model.AlgorithmFinished += (object sender, AlgorithmEventArgs e) =>
            {
                receivedEvents++;
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(stablePairsEvaluation), e.StablePairs);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(groupHappinessEvaluation), e.GroupHappiness);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(egalitarianHappinessEvaluation), e.EgalitarianHappiness);
            };
            task = Task.Run(async () => {
                await _model.RunSingleAlgorithm(0);
                Assert.AreEqual(2, receivedEvents);
            });
        }

        [TestMethod]
        public void RunGenetic()
        {
            int receivedEvents = 0;
            Task task;
            NewModel();

            _model.AlgorithmStarted += (object sender, AlgorithmEventArgs e) => receivedEvents++;
            _model.AlgorithmFinished += (object sender, AlgorithmEventArgs e) =>
            {
                receivedEvents++;
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(stablePairsEvaluation), e.StablePairs);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(groupHappinessEvaluation), e.GroupHappiness);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(egalitarianHappinessEvaluation), e.EgalitarianHappiness);
            };

            task = Task.Run(async () => {
                await _model.RunSingleAlgorithm(1);
                Assert.AreEqual(2, receivedEvents);
            });
        }

        [TestMethod]
        public void RunAll()
        {
            int receivedEvents = 0;
            Task task;
            NewModel();

            _model.AlgorithmStarted += (object sender, AlgorithmEventArgs e) => receivedEvents++;
            _model.AlgorithmFinished += (object sender, AlgorithmEventArgs e) =>
            {
                receivedEvents++;
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(stablePairsEvaluation), e.StablePairs);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(groupHappinessEvaluation), e.GroupHappiness);
                Assert.AreEqual(_context.Algorithms[e.Index].Algorithm.Evaluate(egalitarianHappinessEvaluation), e.EgalitarianHappiness);
            };

            task = Task.Run(async () => {
                await _model.RunAllAlgorithms();
                Assert.AreEqual(4, receivedEvents);
            });
        }

        private void NewModel()
        {
            _context = new ModelContext();
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
            _model = new RunModel(_context);
            _model.Initialize();
            _context.AlgorithmsChanged = false;
        }

        private async Task RunAlgorithm(Task task, int count)
        {
            

            await task;
        }
    }
}

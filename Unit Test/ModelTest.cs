using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Model.Structures;

namespace Unit_Test
{
    [TestClass]
    public class ModelTest
    {
        private Mock<GaleShapleyAlgorithm> mockGaleShapleyAlgorithm;
        private Mock<GeneticAlgorithm> mockGeneticAlgorithm;

        [TestMethod]
        public void Initialize()
        {
        }

        [TestMethod]
        public void Evaluation_IEvaluation()
        {
            Mock<IEvaluation<int>> mockIEvaluation = new Mock<IEvaluation<int>>();
            Mock<StableMarriage> mockStableMarriage = new Mock<StableMarriage>();
            Mock<Solution> mockSolution = new Mock<Solution>();

            mockIEvaluation.Setup(m => m.Evaluate(mockStableMarriage.Object, mockSolution.Object)).Returns(It.IsAny<int>());
        }

        [TestMethod]
        public void Evaluation_StablePairs()
        {
        }
    }
}

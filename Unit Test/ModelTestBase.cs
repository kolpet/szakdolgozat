using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Szakdolgozat.Model;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Model.Structures;

namespace Unit_Test
{
    public class ModelTestBase
    {
        private AppModel _model;

        public void Initialize()
        {
        }

        [TestMethod]
        public void Model_SetupModel()
        {
            Mock<SetupModel> model = new Mock<SetupModel>();

            //model.Setup(m => m.)
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

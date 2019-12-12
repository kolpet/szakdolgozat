using Microsoft.VisualStudio.TestTools.UnitTesting;
using Szakdolgozat.Model;

namespace Unit_Test.Model
{
    [TestClass]
    public class SetupModelTest
    {
        [TestMethod]
        public void Initialize()
        {

        }

        [TestMethod]
        public void ChangeValues()
        {
            string group1Name = "test1";
            string group2Name = "test2";
            int groupSize = 42;

            SetupModel model = CreateModel();

            model.ChangeGroup1Name(group1Name);
            model.ChangeGroup2Name(group2Name);
            model.ChangeParticipantNumber(groupSize);

            Assert.Equals(model.)
        }

        private SetupModel CreateModel()
        {
            SetupModel model = new SetupModel();
            model.Initialize();
            return model;
        }
    }
}

namespace Geranium.Toposorts.Tests
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void DependenciesByTypesTest()
        {
            var m1 = new Module1();
            var m2 = new Module2();
            var m3 = new Module3();

            var modules = new BaseModule[] { m1,m2,m3 };
            var sorted = modules.Sort().ToArray();

            Assert.AreEqual(m1, sorted[0]);
            Assert.AreEqual(m3, sorted[1]);
            Assert.AreEqual(m2, sorted[2]);
        }

        [TestMethod]
        public void DependenciesByWeightTest()
        {
            var m1 = new ModuleWeight1();
            var m2 = new ModuleWeight2();
            var m3 = new ModuleWeight3();

            var modules = new BaseModule[] { m1, m2, m3 };
            var sorted = modules.Sort().ToArray();

            Assert.AreEqual(m1, sorted[0]);
            Assert.AreEqual(m3, sorted[1]);
            Assert.AreEqual(m2, sorted[2]);
        }

        [TestMethod]
        public void DependenciesByAllTest()
        {
            var m1 = new ModuleWeight1();
            var m2 = new ModuleWeight2();
            var m3 = new Module2();
            var m4 = new Module3();

            var modules = new BaseModule[] { m1, m2, m3, m4 };
            var sorted = modules.Sort().ToArray();

            Assert.AreEqual(m1, sorted[0]);
            Assert.AreEqual(m2, sorted[1]);
            Assert.AreEqual(m4, sorted[2]);
            Assert.AreEqual(m3, sorted[3]);
        }
    }
}
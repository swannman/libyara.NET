using libyaraNET;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public static class AssemblyInitialize
    {
        private static YaraContext _ctx;

        [AssemblyInitialize]
        public static void MyTestInitialize(TestContext _)
        {
            _ctx = new YaraContext();
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            _ctx.Dispose();
        }
    }
}

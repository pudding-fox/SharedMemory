using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace SharedMemory.Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsTrue(SharedMemory.Load());
        }

        [TearDown]
        public void TearDown()
        {
            SharedMemory.Unload();
        }

        [Test]
        public void Test001()
        {
            var name = Guid.NewGuid().ToString();
            var size = 1024;
            var handle = default(IntPtr);
            var result = SharedMemory.Create(name, size, out handle);
            Assert.AreEqual(0x80004001L, result);
        }
    }
}

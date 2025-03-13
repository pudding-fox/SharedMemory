using NUnit.Framework;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
            {
                var result = SharedMemory.Create(name, size, out handle);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var result = SharedMemory.Release(handle);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
        }

        [Test]
        public void Test002()
        {
            var name = Guid.NewGuid().ToString();
            var size = 1024;
            var master = default(IntPtr);
            var handle = default(IntPtr);
            var map = default(IntPtr);
            {
                var result = SharedMemory.Create(name, size, out master);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var result = SharedMemory.Open(name, 0, size, out handle, out map);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var result = SharedMemory.Close(handle, map);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var result = SharedMemory.Release(master);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
        }

        [Test]
        public void Test003()
        {
            var name = Guid.NewGuid().ToString();
            var size = 1024;
            var master = default(IntPtr);
            var handle = default(IntPtr);
            var map = default(IntPtr);
            var subject = "Hello World!";
            {
                var result = SharedMemory.Create(name, size, out master);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var result = SharedMemory.Open(name, 0, size, out handle, out map);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var buffer = Encoding.UTF8.GetBytes(subject);
                var result = SharedMemory.Write(handle, map, ref buffer, 0, buffer.Length);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var buffer = new byte[36];
                var result = SharedMemory.Read(handle, map, ref buffer, 0, 36);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var result = SharedMemory.Close(handle, map);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
            {
                var result = SharedMemory.Release(master);
                Assert.AreEqual(SharedMemory.HRESULT.OK, result);
            }
        }
    }
}

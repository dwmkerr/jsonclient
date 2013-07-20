using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace JsonClient.Tests
{
    [TestFixture]
    public class JsonClientErrorHandlingTests
    {
        private Process serverProcess;

        [Test]
        public void CanFailGracefullyWithNoConnection()
        {
            //  Kill the server.
            RunAfterAnyTests();

            //  Try and get a post.
            var result = JsonClient.Get("http://localhost:3212/posts");
            var posts = result.Dynamic;

            //  There should be no json, no dynamic, no response and one error.
            Assert.IsNull(result.Json);
            Assert.IsNull(result.Dynamic);
            Assert.IsNull(result.Response);
            Assert.IsNotNull(result.Error);
        }

        [Test]
        public void CanFailGracefullyWithBadRequestVerbConnection()
        {
            //  Try and get a post.
            var result = JsonClient.Request("GOT", "http://localhost:3212/posts");
            var posts = result.Dynamic;

            //  There should be no json, no dynamic, no response and one error.
            Assert.IsNull(result.Json);
            Assert.IsNull(result.Dynamic);
            Assert.IsNull(result.Response);
            Assert.IsNotNull(result.Error);
        }

        [SetUp]
        public void RunBeforeAnyTests()
        {
            var serverPath = Path.Combine(TestHelper.GetTestsPath(), "..\\..\\..\\TestServer\\server.js");
            serverProcess = Process.Start("node", serverPath);
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            try
            {
                serverProcess.Kill();
            }
            catch (Exception)
            {
                
            }
        }
    }
}

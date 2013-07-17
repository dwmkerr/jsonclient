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
    public class JsonClientAsynchronousTests
    {
        private Process serverProcess;
        [SetUp]
        public void RunBeforeAnyTests()
        {
            var serverPath = Path.Combine(TestHelper.GetTestsPath(), "..\\..\\..\\TestServer\\server.js");
            serverProcess = Process.Start("node", serverPath);
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            serverProcess.Kill();
        }

        [Test]
        public async void CanGetPosts()
        {
            var result = await JsonClient.GetAsync("http://localhost:3212/posts");
            var posts = result.Dynamic;

            //  We should have three posts.
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error getting the posts.");
            Assert.AreEqual(posts[0].title, "Post One");
            Assert.AreEqual(posts[1].title, "Post Two");
            Assert.AreEqual(posts[2].title, "Post Three");
        }

        [Test]
        public async void CanGetPost()
        {
            var result = await JsonClient.GetAsync("http://localhost:3212/posts/0");
            var posts = result.Dynamic;
            Assert.AreEqual(posts.title, "Post One");
        }

        [Test]
        public async void CannotGetInvalidPost()
        {
            var result = await JsonClient.GetAsync("http://localhost:3212/posts/99");
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async void CanAddPost()
        {
            //  Add the entry.
            var result = await JsonClient.PostAsync("http://localhost:3212/posts",
                                         new
                                             {
                                                 title = "New Blog Post",
                                                 created = DateTime.Now,
                                                 content = "Example entry."
                                             });

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.Created, "Error when adding the post.");
        }

        [Test]
        public async void CanAddPostJson()
        {
            //  Add the entry.
            var result = await JsonClient.PostJsonAsync("http://localhost:3212/posts",
                                         string.Format(
                                            @"{{""title"": ""New Blog Post"", 
                                            ""created"": ""{0}"", 
                                            ""content"": ""Example entry.""}}", DateTime.Now));

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.Created, "Error when adding the post.");
        }

        [Test]
        public async void CanPutPost()
        {
            //  Update post one
            var result = await JsonClient.PutAsync("http://localhost:3212/posts/0",
                                         new
                                         {
                                             title = "Post One Updated",
                                             created = DateTime.Now,
                                             content = "Example entry."
                                         });

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error when putting the post.");

            //  Get post one.
            result = await JsonClient.GetAsync("http://localhost:3212/posts/0");
            var post = result.Dynamic;
            Assert.AreEqual(post.title, "Post One Updated");
        }

        [Test]
        public async void CanPutPostJson()
        {
            //  Update post one
            var result = await JsonClient.PutJsonAsync("http://localhost:3212/posts/0",
                                        @"{""title"":""Post One Updated"", ""content"":""new content""}");

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error when putting the post.");

            //  Get post one.
            result = await JsonClient.GetAsync("http://localhost:3212/posts/0");
            var post = result.Dynamic;
            Assert.AreEqual(post.title, "Post One Updated");
        }

        [Test]
        public async void CanDeletePost()
        {
            //  Delete post one
            var result = await JsonClient.DeleteAsync("http://localhost:3212/posts/0");

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error when deleting the post.");

            //  Make sure it's gone.
            result = await JsonClient.GetAsync("http://localhost:3212/posts/0");
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.NotFound, "The post wasn't deleted");
        }

        [Test]
        public async void CannotDeleteInvalidPost()
        {
            var result = await JsonClient.DeleteAsync("http://localhost:3212/posts/99");
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}

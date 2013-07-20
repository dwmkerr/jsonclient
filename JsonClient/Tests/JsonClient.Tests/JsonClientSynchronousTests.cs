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
    public class JsonClientSynchronousTests
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
        public void CanGetPosts()
        {
            var result = JsonClient.Get("http://localhost:3212/posts");
            var posts = result.Dynamic;

            //  We should have three posts.
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error getting the posts.");
            Assert.AreEqual(posts[0].title, "Post One");
            Assert.AreEqual(posts[1].title, "Post Two");
            Assert.AreEqual(posts[2].title, "Post Three");
        }

        [Test]
        public void CanGetPostsWithRequest()
        {
            var result = JsonClient.Request("GET", "http://localhost:3212/posts");
            var posts = result.Dynamic;

            //  We should have three posts.
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error getting the posts.");
            Assert.AreEqual(posts[0].title, "Post One");
            Assert.AreEqual(posts[1].title, "Post Two");
            Assert.AreEqual(posts[2].title, "Post Three");
        }

        [Test]
        public void CanGetPost()
        {
            var result = JsonClient.Get("http://localhost:3212/posts/0");
            var posts = result.Dynamic;
            Assert.AreEqual(posts.title, "Post One");
        }

        [Test]
        public void CannotGetInvalidPost()
        {
            var result = JsonClient.Get("http://localhost:3212/posts/99");
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public void CanAddPost()
        {
            //  Add the entry.
            var result = JsonClient.Post("http://localhost:3212/posts",
                                         new
                                             {
                                                 title = "New Blog Post",
                                                 created = DateTime.Now,
                                                 content = "Example entry."
                                             });

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.Created, "Error when adding the post.");
        }

        [Test]
        public void CanAddPostJson()
        {
            //  Add the entry.
            var result = JsonClient.Post("http://localhost:3212/posts",
                                         string.Format(
                                            @"{{""title"": ""New Blog Post"", 
                                            ""created"": ""{0}"", 
                                            ""content"": ""Example entry.""}}", DateTime.Now));

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.Created, "Error when adding the post.");
        }

        [Test]
        public void CanPutPost()
        {
            //  Update post one
            var result = JsonClient.Put("http://localhost:3212/posts/0",
                                         new
                                         {
                                             title = "Post One Updated",
                                             created = DateTime.Now,
                                             content = "Example entry."
                                         });

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error when putting the post.");

            //  Get post one.
            result = JsonClient.Get("http://localhost:3212/posts/0");
            var post = result.Dynamic;
            Assert.AreEqual(post.title, "Post One Updated");
        }

        [Test]
        public void CanPutPostJson()
        {
            //  Update post one
            var result = JsonClient.Put("http://localhost:3212/posts/0",
                                        @"{""title"":""Post One Updated"", ""content"":""new content""}");

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error when putting the post.");

            //  Get post one.
            result = JsonClient.Get("http://localhost:3212/posts/0");
            var post = result.Dynamic;
            Assert.AreEqual(post.title, "Post One Updated");
        }

        [Test]
        public void CanDeletePost()
        {
            //  Delete post one
            var result = JsonClient.Delete("http://localhost:3212/posts/0");

            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.OK, "Error when deleting the post.");

            //  Make sure it's gone.
            result = JsonClient.Get("http://localhost:3212/posts/0");
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.NotFound, "The post wasn't deleted");
        }

        [Test]
        public void CannotDeleteInvalidPost()
        {
            var result = JsonClient.Delete("http://localhost:3212/posts/99");
            Assert.AreEqual(result.Response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}

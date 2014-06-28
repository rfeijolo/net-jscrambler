using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JScrambler.Client.Tests
{
    public class JScramblerTests
    {
        JScrambler jscrambler;

        [TestFixtureSetUp]
        public void Init()
        {
            jscrambler = new JScrambler();
        }

        [Test]
        public void CanUpload()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
                DomainLock = "example.com",
                ExpirationDate = DateTime.Now.AddDays(20)
            };

            var uploadResponse = jscrambler.UploadCode(uploadRequest);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_FunctionOutlining()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.FunctionOutlining = FunctionOutlining.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_FunctionReorder()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.FunctionReorder = FunctionReorder.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_DotNotationElimination()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.DotNotationElimination = DotNotationElimination.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_DeadCode()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.DeadCode = DeadCode.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_LiteralHooking()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.LiteralHooking = LiteralHooking.Custom;
            op.LiteralHookingPredicates = new LiteralHookingPredicates(1, 2, .8);

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        // string splitting

        // domain lock

        // browser os lock

        // self defending

        // expiration date


        [Test]
        public void CanUpload_RenameLocal()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.RenameLocal = RenameLocal.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_RenameAll()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.RenameAll = RenameAll.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_Whitespace()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.WhitespaceRemoval = WhitespaceRemoval.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_DuplicateLiterals()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.DuplicateLiterals = DuplicateLiterals.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_ConstantFolding()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.ConstantFolding = ConstantFolding.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_DeadCodeElimination()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.DeadCodeElimination = DeadCodeElimination.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_DictionaryCompression()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.DictionaryCompression = DictionaryCompression.Enabled;

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_NamePrefix()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.NamePrefix = "prefix_";

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_IgnoreFiles()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.IgnoreFiles.Add("test.html");

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_AssertsElimination()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.AssertsElimination.AddRange(new List<string>() { "test" });

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_AssertsElimination_Multiple()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.AssertsElimination.AddRange(new List<string>() { "test", "assertTrue", "equal" });

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_DebuggingCodeElimination()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.DebuggingCodeElimination.Add("DEBUG");

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        [Test]
        public void CanUpload_DebuggingCodeElimination_Multiple()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js", "Scripts\\app\\app.js" },
            };

            OptionalParameters op = new OptionalParameters();
            op.DebuggingCodeElimination.Add("DEBUG");
            op.DebuggingCodeElimination.Add("DEBUG_FUNC_A");

            var parameters = op.GetParameters();

            var uploadResponse = jscrambler.UploadCode(uploadRequest, parameters);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);
        }

        // ...

        [Test]
        public void CanGetInfo_All()
        {
            var getInfoResponse = jscrambler.GetInfo();

            Assert.NotNull(getInfoResponse);
        }

        [Test]
        public void CanGetInfo_Project()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js" },
                DomainLock = "example.com",
                ExpirationDate = DateTime.Now.AddDays(20)
            };

            var uploadResponse = jscrambler.UploadCode(uploadRequest);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);

            var getInfoResponse = jscrambler.GetInfo(uploadResponse.Id);

            Assert.NotNull(getInfoResponse);
            Assert.AreEqual(uploadResponse.Id, getInfoResponse.Id);
        }

        [Test]
        public void CanDownloadProject()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js" },
                DomainLock = "example.com",
                ExpirationDate = DateTime.Now.AddDays(20)
            };

            OptionalParameters op = new OptionalParameters();
            op.DeadCode = DeadCode.Enabled;
            op.DictionaryCompression = DictionaryCompression.Enabled;
            op.RenameAll = RenameAll.Enabled;
            op.FunctionOutlining = FunctionOutlining.Enabled;
            op.DotNotationElimination = DotNotationElimination.Enabled;
            op.DictionaryCompression = DictionaryCompression.Enabled;
            op.FunctionReorder = FunctionReorder.Enabled;

            var uploadResponse = jscrambler.UploadCode(uploadRequest, op.GetParameters());

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);

            var retries = 5;
            bool downloaded = false;

            while (retries > 0)
            {
                Thread.Sleep(5 * 1000);
                downloaded = jscrambler.DownloadProject(uploadResponse.Id);
                retries--;

                if (downloaded)
                {
                    break;
                }
            }

            Assert.True(downloaded);
        }

        [Test]
        public void CanDownloadSourceCode()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js" },
                DomainLock = "example.com",
                ExpirationDate = DateTime.Now.AddDays(20)
            };

            var uploadResponse = jscrambler.UploadCode(uploadRequest);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);

            var retries = 5;
            string source = null;

            while (retries > 0)
            {
                Thread.Sleep(5 * 1000);
                source = jscrambler.DownloadSourceCode(uploadResponse.Id, uploadResponse.Sources[0].Id, uploadResponse.Sources[0].Extension);
                retries--;

                if (!string.IsNullOrEmpty(source))
                {
                    break;
                }
            }

            Assert.IsNotNullOrEmpty(source);
        }

        [Test]
        public void CanDeleteProject()
        {
            var uploadRequest = new UploadCodeRequest()
            {
                Files = new List<string>() { "test.html", "test.js" },
                DomainLock = "example.com",
                ExpirationDate = DateTime.Now.AddDays(20)
            };

            var uploadResponse = jscrambler.UploadCode(uploadRequest);

            Assert.NotNull(uploadResponse);
            Assert.IsNotNullOrEmpty(uploadResponse.Id);

            // whait for the post upload proccess to complete
            Thread.Sleep(10 * 1000); 

            var deleteResponse = jscrambler.DeleteProject(uploadResponse.Id);

            Assert.NotNull(deleteResponse);
            Assert.AreEqual(uploadResponse.Id, deleteResponse.Id);
            Assert.IsTrue(deleteResponse.Deleted);
        }

        [Test]
        public void CannotDeleteProject_NonExistent()
        {
            var deleteResponse = jscrambler.DeleteProject("fde99d708906b06ccf2cbf3079aaa3b903dcb30d");

            Assert.NotNull(deleteResponse);
            Assert.AreEqual(deleteResponse.Error, "404");
            Assert.IsFalse(deleteResponse.Deleted);
        }

        [Test]
        public void CanTestParams()
        {
            OptionalParameters op = new OptionalParameters();

            // Optimization
            op.RenameLocal = RenameLocal.Enabled;

            // Other
            op.NamePrefix = "my_";
            op.IgnoreFiles.AddRange(new List<string>() { "file1.js", "file2.js" });
            op.ExceptionsList.AddRange(new List<string>() { "test", "equal" });
            op.AssertsElimination.AddRange(new List<string>() { "test", "equal" });
            op.DebuggingCodeElimination.AddRange(new List<string>() { "DEBUG_FUNC_A", "DEBUG_FUNC_B" });

            var parameters = op.GetParameters();

            Assert.NotNull(op);
        }
    }
}
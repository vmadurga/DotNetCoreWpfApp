using System;
using System.IO;
using DotNetCoreWpfApp.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCoreWpfApp.Core.Tests.MSTest
{
    [TestClass]
    public class FileServiceTests
    {
        private string _folderPath;
        private string _fileName;

        public FileServiceTests()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UnitTests");
            _fileName = "Tests.json";
        }

        [TestMethod]
        public void TestReadDataFromFile()
        {
            var fileService = new FileService();
            var fileData = "Lorem ipsum dolor sit amet";
            fileService.Save(_folderPath, _fileName, fileData);
            var cacheData = fileService.Read<string>(_folderPath, _fileName);
            Assert.AreEqual(fileData, cacheData);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var filePath = Path.Combine(_folderPath, _fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}

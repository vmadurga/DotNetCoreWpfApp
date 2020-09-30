using System;
using System.IO;
using DotNetCoreWpfApp.Core.Services;
using NUnit.Framework;

namespace DotNetCoreWpfApp.Core.Tests.NUnit
{
    public class FileServiceTests
    {
        private string _folderPath;
        private string _fileName;

        public FileServiceTests()
        {

        }

        [SetUp]
        public void Setup()
        {
            _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UnitTests");
            _fileName = "Tests.json";
        }

        [Test]
        public void TestReadDataFromFile()
        {
            var fileService = new FileService();
            var fileData = "Lorem ipsum dolor sit amet";
            fileService.Save(_folderPath, _fileName, fileData);
            var cacheData = fileService.Read<string>(_folderPath, _fileName);
            Assert.AreEqual(fileData, cacheData);
        }

        [TearDown]
        public void TearDown()
        {
            var filePath = Path.Combine(_folderPath, _fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}

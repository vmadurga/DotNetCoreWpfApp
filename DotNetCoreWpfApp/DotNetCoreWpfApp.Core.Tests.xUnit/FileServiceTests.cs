using System;
using System.IO;
using DotNetCoreWpfApp.Core.Services;
using Xunit;

namespace DotNetCoreWpfApp.Core.Tests.xUnit
{
    public class FileServiceTests : IDisposable
    {
        private readonly string _folderPath;
        private readonly string _fileName;

        public FileServiceTests()
        {
            _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UnitTests");
            _fileName = "Tests.json";
        }

        [Fact]
        public void TestReadDataFromFile()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folderPath = Path.Combine(localAppData, "UnitTests");
            var fileName = "Tests.json";
            var fileService = new FileService();
            var fileData = "Lorem ipsum dolor sit amet";
            fileService.Save(folderPath, fileName, fileData);
            var cacheData = fileService.Read<string>(folderPath, fileName);
            Assert.Equal(fileData, cacheData);
        }

        public void Dispose()
        {
            var filePath = Path.Combine(_folderPath, _fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}

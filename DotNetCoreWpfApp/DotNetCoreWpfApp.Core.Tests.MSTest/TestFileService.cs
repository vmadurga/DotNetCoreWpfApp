﻿using System;
using System.IO;
using System.Reflection;
using DotNetCoreWpfApp.Core.Contracts.Services;
using DotNetCoreWpfApp.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCoreWpfApp.Core.Tests.MSTest
{
    [TestClass]
    public class TestFileService
    {
        private readonly IHost _host;

        public TestFileService()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c => c.SetBasePath(appLocation))
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<IFileService, FileService>();
        }

        [TestMethod]
        public void TestReadDataFromFile()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folderPath = Path.Combine(localAppData, "UnitTests");
            var fileName = "Tests.json";
            if (_host.Services.GetService(typeof(IFileService)) is IFileService fileService)
            {
                fileService.Save(folderPath, fileName, "Lorem ipsum dolor sit amet");
                var cacheData = fileService.Read<string>(folderPath, fileName);
                Assert.AreEqual("Lorem ipsum dolor sit amet", cacheData);
            }
            else
            {
                Assert.Fail($"Can't resolve {nameof(IFileService)}");
            }
        }
    }
}

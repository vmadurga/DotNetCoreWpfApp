using System;
using DotNetCoreWpfApp.Contracts.Services;
using DotNetCoreWpfApp.Models;
using DotNetCoreWpfApp.ViewModels;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace DotNetCoreWpfApp.Tests.NUnit
{
    public class TestSettings
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSettingsViewModel_SetCurrentTheme()
        {
            var mockThemeSelectorService = new Mock<IThemeSelectorService>();
            mockThemeSelectorService.Setup(mock => mock.GetCurrentTheme()).Returns(AppTheme.Light);
            var mockAppConfig = new Mock<IOptions<AppConfig>>();
            var mockSystemService = new Mock<ISystemService>();
            var mockApplicationInfoService = new Mock<IApplicationInfoService>();

            var settingsVm = new SettingsViewModel(mockAppConfig.Object, mockThemeSelectorService.Object, mockSystemService.Object, mockApplicationInfoService.Object);
            settingsVm.OnNavigatedTo(null);

            Assert.AreEqual(AppTheme.Light, settingsVm.Theme);
        }

        [Test]
        public void TestSettingsViewModel_SetCurrentVersion()
        {
            var mockThemeSelectorService = new Mock<IThemeSelectorService>();
            var mockAppConfig = new Mock<IOptions<AppConfig>>();
            var mockSystemService = new Mock<ISystemService>();
            var mockApplicationInfoService = new Mock<IApplicationInfoService>();

            var testVersion = new Version(1, 2, 3, 4);

            mockApplicationInfoService.Setup(mock => mock.GetVersion()).Returns(testVersion);

            var settingsVm = new SettingsViewModel(mockAppConfig.Object, mockThemeSelectorService.Object, mockSystemService.Object, mockApplicationInfoService.Object);
            settingsVm.OnNavigatedTo(null);

            Assert.AreEqual($"DotNetCoreWpfApp - {testVersion}", settingsVm.VersionDescription);
        }
    }
}

using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotNetCoreWpfApp.Core.Contracts.Services;
using DotNetCoreWpfApp.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace DotNetCoreWpfApp.Core.Tests.NUnit
{
    public class TestSampleDataService
    {
        private IHost _host;

        [SetUp]
        public void Setup()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c => c.SetBasePath(appLocation))
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddTransient<ISampleDataService, SampleDataService>();
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Test]
        public async Task EnsureSampleDataServiceReturnsContentGridDataAsync()
        {
            if (_host.Services.GetService(typeof(ISampleDataService)) is ISampleDataService sampleDataService)
            {
                var data = await sampleDataService.GetContentGridDataAsync();
                Assert.IsTrue(data.Any());
            }
            else
            {
                Assert.Fail($"Can't resolve {nameof(ISampleDataService)}");
            }
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Test]
        public async Task EnsureSampleDataServiceReturnsGridDataAsync()
        {
            if (_host.Services.GetService(typeof(ISampleDataService)) is ISampleDataService sampleDataService)
            {
                var data = await sampleDataService.GetGridDataAsync();
                Assert.IsTrue(data.Any());
            }
            else
            {
                Assert.Fail($"Can't resolve {nameof(ISampleDataService)}");
            }
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Test]
        public async Task EnsureSampleDataServiceReturnsMasterDetailDataAsync()
        {
            if (_host.Services.GetService(typeof(ISampleDataService)) is ISampleDataService sampleDataService)
            {
                var data = await sampleDataService.GetMasterDetailDataAsync();
                Assert.IsTrue(data.Any());
            }
            else
            {
                Assert.Fail($"Can't resolve {nameof(ISampleDataService)}");
            }
        }
    }
}

using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotNetCoreWpfApp.Core.Contracts.Services;
using DotNetCoreWpfApp.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace DotNetCoreWpfApp.Core.Tests.xUnit
{
    public class TestSampleDataService
    {
        private readonly IHost _host;

        public TestSampleDataService()
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
        [Fact]
        public async Task EnsureSampleDataServiceReturnsContentGridDataAsync()
        {
            if (_host.Services.GetService(typeof(ISampleDataService)) is ISampleDataService sampleDataService)
            {
                var data = await sampleDataService.GetContentGridDataAsync();
                Assert.True(data.Any());
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(ISampleDataService)}");
            }
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Fact]
        public async Task EnsureSampleDataServiceReturnsGridDataAsync()
        {
            if (_host.Services.GetService(typeof(ISampleDataService)) is ISampleDataService sampleDataService)
            {
                var data = await sampleDataService.GetGridDataAsync();
                Assert.True(data.Any());
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(ISampleDataService)}");
            }
        }

        // TODO WTS: Remove or update this once your app is using real data and not the SampleDataService.
        // This test serves only as a demonstration of testing functionality in the Core library.
        [Fact]
        public async Task EnsureSampleDataServiceReturnsMasterDetailDataAsync()
        {
            if (_host.Services.GetService(typeof(ISampleDataService)) is ISampleDataService sampleDataService)
            {
                var data = await sampleDataService.GetMasterDetailDataAsync();
                Assert.True(data.Any());
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(ISampleDataService)}");
            }
        }
    }
}

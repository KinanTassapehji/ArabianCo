using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ArabianCo.Configuration;

namespace ArabianCo.Web.Host.Startup
{
    [DependsOn(
       typeof(ArabianCoWebCoreModule))]
    public class ArabianCoWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ArabianCoWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ArabianCoWebHostModule).GetAssembly());
        }
    }
}

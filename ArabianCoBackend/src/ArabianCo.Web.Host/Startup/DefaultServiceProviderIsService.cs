using Microsoft.Extensions.DependencyInjection;
using System;

namespace ArabianCo.Web.Host.Startup
{
    internal class DefaultServiceProviderIsService : IServiceProviderIsService
    {
        private readonly IServiceCollection _services;

        public DefaultServiceProviderIsService(IServiceCollection services)
        {
            _services = services;
        }

        public bool IsService(Type serviceType)
        {
            foreach (var descriptor in _services)
            {
                if (descriptor.ServiceType == serviceType)
                    return true;
            }

            return false;
        }
    }
}
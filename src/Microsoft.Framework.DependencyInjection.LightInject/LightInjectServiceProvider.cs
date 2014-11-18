using System;
using LightInject;

namespace Microsoft.Framework.DependencyInjection.LightInject
{
    internal class LightInjectServiceProvider : IServiceProvider
    {
        private readonly IServiceFactory _factory;


        public LightInjectServiceProvider(IServiceFactory factory)
        {
            _factory = factory;
        }

        public object GetService(Type serviceType)
        {
            return _factory.TryGetInstance(serviceType);
        }
    }
}
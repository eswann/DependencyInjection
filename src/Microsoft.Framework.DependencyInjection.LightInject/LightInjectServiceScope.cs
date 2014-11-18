using System;
using LightInject;

namespace Microsoft.Framework.DependencyInjection.LightInject
{
    internal class LightInjectServiceScope : IServiceScope
    {
        private readonly IServiceFactory _factory;
        private readonly Scope _scope;
        public LightInjectServiceScope(IServiceFactory factory)
        {
            _factory = factory;
            _scope = _factory.BeginScope();
        }

        public IServiceProvider ServiceProvider => new LightInjectServiceProvider(_factory);

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
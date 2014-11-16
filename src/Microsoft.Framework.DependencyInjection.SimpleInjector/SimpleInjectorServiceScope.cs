using System;
using SimpleInjector;

namespace Microsoft.Framework.DependencyInjection.SimpleInjector
{
    internal class SimpleInjectorServiceScope : IServiceScope
    {
        private readonly Container _container;
        private readonly Scope _scope;
        public SimpleInjectorServiceScope(Container container, Scope scope)
        {
            _container = container;
            _scope = scope;
        }

        public IServiceProvider ServiceProvider => _container;

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
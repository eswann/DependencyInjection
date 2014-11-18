using System;
using System.Collections.Generic;
using LightInject;
using System.Reflection;

namespace Microsoft.Framework.DependencyInjection.LightInject
{
    public static class LightInjectRegistration
    {
        public static void Populate(this ServiceContainer container, IEnumerable<IServiceDescriptor> descriptors, IServiceProvider fallbackProvider = null)
        {
            if (fallbackProvider != null)
            {
                container.RegisterFallback((serviceType, serviceName) => true, request => fallbackProvider.GetService(request.ServiceType));
            }

            container.RegisterInstance<IServiceProvider>(new LightInjectServiceProvider(container));

            container.Register<IServiceScope>(factory => new LightInjectServiceScope(factory));
            container.Register<IServiceScopeFactory>(factory => new LightInjectServiceScopeFactory(factory));

            Register(container, descriptors);
        }

        private static void Register(ServiceContainer container, IEnumerable<IServiceDescriptor> descriptors)
        {
            foreach (var descriptor in descriptors)
            {
                ILifetime lifetime = GetLifestyle(descriptor.Lifecycle);

                var localDescriptor = descriptor;

                if (localDescriptor.ImplementationType != null)
                {
                    var serviceTypeInfo = descriptor.ServiceType.GetTypeInfo();

                    container.Register(localDescriptor.ServiceType, localDescriptor.ImplementationType, lifetime);
                }
                else if (localDescriptor.ImplementationFactory != null)
                {
                    container.Register(factory => localDescriptor.ImplementationFactory(factory.GetInstance<IServiceProvider>()), lifetime);
                }
                else
                {
                    container.RegisterInstance(localDescriptor.ServiceType, localDescriptor.ImplementationInstance);
                }
            }
        }

        private static ILifetime GetLifestyle(LifecycleKind lifecycleKind)
        {
            switch (lifecycleKind)
            {
                case LifecycleKind.Singleton:
                    return new PerContainerLifetime();
                case LifecycleKind.Scoped:
                    return new PerScopeLifetime();
            }

            return null;
        }


    }
}
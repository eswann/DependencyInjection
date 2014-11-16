using System;
using System.Collections.Generic;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Extensions;
using System.Reflection;

namespace Microsoft.Framework.DependencyInjection.SimpleInjector
{
    public static class SimpleInjectorRegistration
    {
        public static void Populate(this Container container, IEnumerable<IServiceDescriptor> descriptors, IServiceProvider fallbackProvider = null)
        {
            if (fallbackProvider != null)
            {
                container.ResolveUnregisteredType += ((sender, args) =>
                {
                    args.Register(() => fallbackProvider.GetService(args.UnregisteredServiceType));
                });
            }

            container.Register<IServiceScope, SimpleInjectorServiceScope>();
            container.Register<IServiceScopeFactory, SimpleInjectorServiceScopeFactory>();

            Register(container, descriptors);
        }

        private static void Register(Container container, IEnumerable<IServiceDescriptor> descriptors)
        {
            foreach (var descriptor in descriptors)
            {
                Lifestyle lifestyle = GetLifestyle(descriptor.Lifecycle);

                var localDescriptor = descriptor;

                if (localDescriptor.ImplementationType != null)
                {
                    var serviceTypeInfo = descriptor.ServiceType.GetTypeInfo();

                    if (serviceTypeInfo.ContainsGenericParameters)
                    {
                        container.RegisterOpenGeneric(localDescriptor.ServiceType, localDescriptor.ImplementationType, lifestyle);
                    }
                    else
                    {
                        container.Register(localDescriptor.ServiceType, localDescriptor.ImplementationType, lifestyle);
                    }
                }
                else if (localDescriptor.ImplementationFactory != null)
                {
                    
                    container.Register(localDescriptor.ServiceType, () => localDescriptor.ImplementationFactory(container), lifestyle);
                }
                else
                {
                    container.Register(() => localDescriptor.ImplementationInstance, lifestyle);
                }
            }
        }

        private static Lifestyle GetLifestyle(LifecycleKind lifecycleKind)
        {
            switch (lifecycleKind)
            {
                case LifecycleKind.Singleton:
                    return Lifestyle.Singleton;
                case LifecycleKind.Scoped:
                    return new ExecutionContextScopeLifestyle();
            }

            return Lifestyle.Transient;
        }


    }
}
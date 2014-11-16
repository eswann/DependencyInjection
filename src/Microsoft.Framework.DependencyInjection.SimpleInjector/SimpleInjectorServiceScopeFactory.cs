// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Microsoft.Framework.DependencyInjection.SimpleInjector
{
    internal class SimpleInjectorServiceScopeFactory : IServiceScopeFactory
    {
        private readonly Container container;
        public SimpleInjectorServiceScopeFactory(Container container)
        {
            this.container = container;
        }

        public IServiceScope CreateScope()
        {
            return new SimpleInjectorServiceScope(container, container.BeginExecutionContextScope());
        }
    }
}

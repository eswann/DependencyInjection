// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using LightInject;

namespace Microsoft.Framework.DependencyInjection.LightInject
{
    internal class LightInjectServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IServiceFactory _factory;

        public LightInjectServiceScopeFactory(IServiceFactory factory)
        {
            _factory = factory;
        }

        public IServiceScope CreateScope()
        {
            return new LightInjectServiceScope(_factory);
        }
    }
}

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Framework.DependencyInjection.SimpleInjector;
using Microsoft.Framework.DependencyInjection.Tests.Fakes;
using SimpleInjector;

namespace Microsoft.Framework.DependencyInjection.Tests
{
    public class SimpleInjectorContainerTests : ScopingContainerTestBase
    {
        protected override IServiceProvider CreateContainer()
        {
            return CreateContainer(new FakeFallbackServiceProvider());
        }

        protected override IServiceProvider CreateContainer(IServiceProvider fallbackProvider)
        {
            var container = new Container();

            container.Populate(
                TestServices.DefaultServices(),
                fallbackProvider);

            return container.GetInstance<IServiceProvider>();
        }
    }
}
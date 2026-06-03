// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests
{
    public abstract class CerbosTest
    {
        private const int HttpPort = 3592;
        private const int GrpcPort = 3593;
        private const string Image = "ghcr.io/cerbos/cerbos";
        private const string Tag = "dev";
        private const string PathToPolicies = "./../../../res/policies";
        private const string PathToConfig = "./../../../res/config";
        protected readonly Grpc.Core.Metadata _metadata = new() { { "wibble", "wobble" } };

        private IContainer? _container;

        protected ICerbosClient? _client;
        protected ICerbosAdminClient? _clientAdmin;
        protected ICerbosClient? _clientPlayground;

        private const string PlaygroundHost = "https://demo-pdp.cerbos.cloud";
        private const string PlaygroundInstanceId = "XhkOi82fFKk3YW60e2c806Yvm0trKEje"; // See: https://play.cerbos.dev/p/XhkOi82fFKk3YW60e2c806Yvm0trKEje

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _container = new ContainerBuilder($"{Image}:{Tag}")
                .WithPortBinding(HttpPort)
                .WithPortBinding(GrpcPort)
                .WithBindMount(Path.GetFullPath(PathToPolicies), "/policies")
                .WithBindMount(Path.GetFullPath(PathToConfig), "/config")
                .WithCommand("server", "--config=/config/config.yaml")
                .Build();

            Task.Run(async () => await _container.StartAsync()).Wait();
            Thread.Sleep(3000);

            var client = CerbosClientBuilder.ForTarget("http://127.0.0.1:3593").WithMetadata(_metadata).WithPlaintext();

            _client = client.Build();
            _clientAdmin = client.BuildAdminClient("cerbos", "cerbosAdmin");
            _clientPlayground = CerbosClientBuilder.ForTarget(PlaygroundHost).WithMetadata(_metadata).WithPlaygroundInstance(PlaygroundInstanceId).Build();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Task.Run(async () => await _container.StopAsync()).Wait();
            _container.DisposeAsync();
        }
    }
}

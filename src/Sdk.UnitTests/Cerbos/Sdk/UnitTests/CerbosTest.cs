// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;
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
        private const string ImageCtl = "ghcr.io/cerbos/cerbosctl";
        private const string Tag = "dev";
        private const string PathToConfig = "./../../../res/config";
        private const string PathToPolicies = "./../../../res/policies";

        protected readonly Grpc.Core.Metadata _metadata = new() { { "wibble", "wobble" } };

        private IContainer _container = null!;

        protected ICerbosClient _client = null!;
        protected ICerbosAdminClient _clientAdmin = null!;
        protected ICerbosClient _clientPlayground = null!;

        private const string PlaygroundHost = "https://demo-pdp.cerbos.cloud";
        private const string PlaygroundInstanceId = "XhkOi82fFKk3YW60e2c806Yvm0trKEje"; // See: https://play.cerbos.dev/p/XhkOi82fFKk3YW60e2c806Yvm0trKEje

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _container = Cerbos();
            Cerbosctl(_container.IpAddress, "put", "policies", "--plaintext", "-R", "/policies");
            Cerbosctl(_container.IpAddress, "put", "schemas", "--plaintext", "-R", "/policies");

            var client = CerbosClientBuilder.ForTarget("http://127.0.0.1:3593").WithMetadata(_metadata).WithPlaintext();
            var playgroundClient = CerbosClientBuilder.ForTarget(PlaygroundHost).WithMetadata(_metadata).WithPlaygroundInstance(PlaygroundInstanceId);

            _client = client.Build();
            _clientAdmin = client.BuildAdminClient("cerbos", "cerbosAdmin");
            _clientPlayground = playgroundClient.Build();
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            Task.Run(async () => await _container.StopAsync()).Wait();
            await _container.DisposeAsync();
        }

        private IContainer Cerbos()
        {
            var container = new ContainerBuilder($"{Image}:{Tag}")
                .WithPortBinding(HttpPort)
                .WithPortBinding(GrpcPort)
                .WithBindMount(Path.GetFullPath(PathToConfig), "/config")
                .WithCommand("server", "--config=/config/config.yaml")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilContainerIsHealthy())
                .Build();

            Task.Run(async () => await container.StartAsync()).Wait();
            return container;
        }

        private void Cerbosctl(string host, params string[] args)
        {
            var combinedArgs = new[] { $"--server={host}:3593", "--username=cerbos", "--password=cerbosAdmin" }.Concat(args).ToArray();
            var container = new ContainerBuilder($"{ImageCtl}:{Tag}")
                .WithBindMount(Path.GetFullPath(PathToPolicies), "/policies")
                .WithCommand(combinedArgs)
                .Build();
            Task.Run(async () => await container.StartAsync()).Wait();
            Thread.Sleep(3000);
        }
    }
}

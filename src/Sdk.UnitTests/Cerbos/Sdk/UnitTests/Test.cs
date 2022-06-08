using Cerbos.Sdk.Builders;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests
{
    public class Tests
    {
        private const int HttpPort = 3592;
        private const int GrpcPort = 3593;
        private const string Image = "ghcr.io/cerbos/cerbos";
        private const string Tag = "latest";
        private TestcontainersContainer Container;
        
        private CerbosBlockingClient _client;
        private readonly string _jwt =
            "eyJhbGciOiJFUzM4NCIsImtpZCI6IjE5TGZaYXRFZGc4M1lOYzVyMjNndU1KcXJuND0iLCJ0eXAiOiJKV1QifQ.eyJhdWQiOlsiY2VyYm9zLWp3dC10ZXN0cyJdLCJjdXN0b21BcnJheSI6WyJBIiwiQiIsIkMiXSwiY3VzdG9tSW50Ijo0MiwiY3VzdG9tTWFwIjp7IkEiOiJBQSIsIkIiOiJCQiIsIkMiOiJDQyJ9LCJjdXN0b21TdHJpbmciOiJmb29iYXIiLCJleHAiOjE5NTAyNzc5MjYsImlzcyI6ImNlcmJvcy10ZXN0LXN1aXRlIn0._nCHIsuFI3wczeuUv_xjSwaVnIQUdYA9sGf_jVsrsDWloLs3iPWDaA1bXpuIUJVsi8-G6qqdrPI0cOBxEocg1NCm8fyD9T_3hsZV0fYWon_Je6Kl93a3JIW3S6kbvjsL";
        
        [SetUp]
        public void Setup()
        {
            Container = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage($"{Image}:{Tag}")
                .WithPortBinding(HttpPort)
                .WithPortBinding(GrpcPort)
                .WithMount("./res/policies", "/policies")
                .WithMount("./res/config", "/config")
                .WithCommand("server", "--config=/config/config.yaml")
                .Build();

            Task.Run(async () => await Container.StartAsync()).Wait();
            Thread.Sleep(3000);
            _client = new CerbosClientBuilder("127.0.0.1:3593").WithPlaintext().BuildBlockingClient();
        }

        [TearDown]
        public void TearDown()
        {
            Task.Run(async () => await Container.StopAsync()).Wait();
        }

        [Test]
        public void CheckWithoutJWT()
        {
            var have =
                _client.Check(
                    Principal.NewInstance("john", new []{"employee"})
                    .WithPolicyVersion("20210210")
                    .WithAttribute("department", AttributeValue.StringValue("marketing"))
                    .WithAttribute("geography", AttributeValue.StringValue("GB")),
                    Resource.NewInstance("leave_request", "xx125")
                        .WithPolicyVersion("20210210")
                        .WithAttribute("department", AttributeValue.StringValue("marketing"))
                        .WithAttribute("geography", AttributeValue.StringValue("GB"))
                        .WithAttribute("owner", AttributeValue.StringValue("john")),
                    new []{"view:public", "approve"}
                );
            Assert.AreEqual(true, have.IsAllowed("view:public"));
            Assert.AreEqual(false, have.IsAllowed("approve"));
        }
    }
}

// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Api.V1.Engine;
using Cerbos.Sdk.Builders;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using NUnit.Framework;
using AuxData = Cerbos.Sdk.Builders.AuxData;
using Principal = Cerbos.Sdk.Builders.Principal;
using Resource = Cerbos.Sdk.Builders.Resource;

namespace Cerbos.Sdk.UnitTests
{
    public class CerbosBlockingClientTest
    {
        private const int HttpPort = 3592;
        private const int GrpcPort = 3593;
        private const string Image = "ghcr.io/cerbos/cerbos";
        private const string Tag = "latest";
        private const string PathToPolicies = "./../../../res/policies";
        private const string PathToConfig = "./../../../res/config";
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
                .WithBindMount(Path.GetFullPath(PathToPolicies), "/policies")
                .WithBindMount(Path.GetFullPath(PathToConfig), "/config")
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
        public void CheckWithoutJwt()
        {
            var have =
                _client
                    .CheckResources(
                        Principal.NewInstance("john","employee")
                        .WithPolicyVersion("20210210")
                        .WithAttribute("team", AttributeValue.StringValue("design"))
                        .WithAttribute("department", AttributeValue.StringValue("marketing"))
                        .WithAttribute("geography", AttributeValue.StringValue("GB"))
                        .WithAttribute("reader", AttributeValue.BoolValue(false)),
                        
                        Resource.NewInstance("leave_request", "XX125")
                            .WithPolicyVersion("20210210")
                            .WithAttribute("id", AttributeValue.StringValue("XX125"))
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("GB"))
                            .WithAttribute("team", AttributeValue.StringValue("design"))
                            .WithAttribute("owner", AttributeValue.StringValue("john")),
                        
            "view:public", "approve"
                    );
            
            Assert.That(have.IsAllowed("view:public"), Is.True);
            Assert.That(have.IsAllowed("approve"), Is.False);
        }
        
        [Test]
        public void CheckWithJwt()
        {
            var have =
                _client
                    .With(AuxData.WithJwt(_jwt))
                    .CheckResources(
                        Principal.NewInstance("john", "employee")
                            .WithPolicyVersion("20210210")
                            .WithAttribute("team", AttributeValue.StringValue("design"))
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("GB"))
                            .WithAttribute("reader", AttributeValue.BoolValue(false)),
                        
                        Resource.NewInstance("leave_request", "XX125")
                            .WithPolicyVersion("20210210")
                            .WithAttribute("id", AttributeValue.StringValue("XX125"))
                            .WithAttribute("team", AttributeValue.StringValue("design"))
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("GB"))
                            .WithAttribute("owner", AttributeValue.StringValue("john")),
                        
                        "defer"
                    );
            Assert.That(have.IsAllowed("defer"), Is.True);
        }
        
        [Test]
        public void CheckMultiple()
        {
            var have =
                _client
                    .With(AuxData.WithJwt(_jwt))
                    .CheckResources(
                        Principal.NewInstance("john", "employee")
                            .WithPolicyVersion("20210210")
                            .WithAttribute("team", AttributeValue.StringValue("design"))
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("GB"))
                            .WithAttribute("reader", AttributeValue.BoolValue(false)),
                        
                        ResourceAction.NewInstance("leave_request", "XX125")
                            .WithPolicyVersion("20210210")
                            .WithAttribute("id", AttributeValue.StringValue("XX125"))
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("GB"))
                            .WithAttribute("team", AttributeValue.StringValue("design"))
                            .WithAttribute("owner", AttributeValue.StringValue("john"))
                            .WithActions("view:public", "approve", "defer"),
                        
                        ResourceAction.NewInstance("leave_request", "XX225")
                            .WithPolicyVersion("20210210")
                            .WithAttribute("id", AttributeValue.StringValue("XX225"))
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("GB"))
                            .WithAttribute("team", AttributeValue.StringValue("design"))
                            .WithAttribute("owner", AttributeValue.StringValue("martha"))
                            .WithActions("view:public", "approve"),
                        
                        ResourceAction.NewInstance("leave_request", "XX325")
                            .WithPolicyVersion("20210210")
                            .WithAttribute("id", AttributeValue.StringValue("XX325"))
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("US"))
                            .WithAttribute("team", AttributeValue.StringValue("design"))
                            .WithAttribute("owner", AttributeValue.StringValue("peggy"))
                            .WithActions("view:public", "approve")
                    );

            var resourcexx125 = have.Find("XX125");
            Assert.That(resourcexx125.IsAllowed("view:public"), Is.True);
            Assert.That(resourcexx125.IsAllowed("defer"), Is.True);
            Assert.That(resourcexx125.IsAllowed("approve"), Is.False);
            
            var resourcexx225 = have.Find("XX225");
            Assert.That(resourcexx225.IsAllowed("view:public"), Is.True);
            Assert.That(resourcexx225.IsAllowed("approve"), Is.False);

            var resourcexx325 = have.Find("XX325");
            Assert.That(resourcexx325.IsAllowed("view:public"), Is.True);
            Assert.That(resourcexx325.IsAllowed("approve"), Is.False);
        }

        [Test]
        public void PlanResources()
        {
            PlanResourcesResult have = _client.PlanResources
            (
                Principal.NewInstance("maggie", "manager")
                    .WithPolicyVersion("20210210")
                    .WithAttribute("department", AttributeValue.StringValue("marketing"))
                    .WithAttribute("geography", AttributeValue.StringValue("GB"))
                    .WithAttribute("managed_geographies", AttributeValue.StringValue("GB"))
                    .WithAttribute("team", AttributeValue.StringValue("design"))
                    .WithAttribute("reader", AttributeValue.BoolValue(false)),
                
                Resource.NewInstance("leave_request")
                    .WithPolicyVersion("20210210"), 
                
                "approve"
            );
            
            Assert.That(have.GetAction(), Is.EqualTo("approve"));
            Assert.That(have.GetPolicyVersion(), Is.EqualTo("20210210"));
            Assert.That(have.GetResourceKind(), Is.EqualTo("leave_request"));
            Assert.That(have.HasValidationErrors(), Is.False);
            Assert.That(have.IsAlwaysAllowed(), Is.False);
            Assert.That(have.IsAlwaysDenied(), Is.False);
            Assert.That(have.IsConditional(), Is.True);

            PlanResourcesFilter.Types.Expression.Types.Operand cond = have.GetCondition();
            PlanResourcesFilter.Types.Expression expr = cond.Expression;
            
            Assert.NotNull(expr);
            Assert.That(expr.Operator, Is.EqualTo("and"));

            PlanResourcesFilter.Types.Expression argExpr = expr.Operands[0].Expression;
            Assert.NotNull(argExpr);
            Assert.That(argExpr.Operator, Is.EqualTo("eq"));
            
            PlanResourcesFilter.Types.Expression argExpr1 = expr.Operands[1].Expression;
            Assert.NotNull(argExpr1);
            Assert.That(argExpr1.Operator, Is.EqualTo("eq"));
        }
        
        [Test]
        public void PlanResourcesValidation()
        {
            PlanResourcesResult have = _client.PlanResources
            (
                Principal.NewInstance("maggie", "manager")
                    .WithPolicyVersion("20210210")
                    .WithAttribute("department", AttributeValue.StringValue("accounting"))
                    .WithAttribute("geography", AttributeValue.StringValue("GB"))
                    .WithAttribute("managed_geographies", AttributeValue.StringValue("GB"))
                    .WithAttribute("team", AttributeValue.StringValue("design"))
                    .WithAttribute("reader", AttributeValue.BoolValue(false)),
                
                Resource.NewInstance("leave_request")
                    .WithPolicyVersion("20210210")
                    .WithAttribute("department", AttributeValue.StringValue("accounting")), 
                
                "approve"
            );
            
            Assert.That(have.GetAction(), Is.EqualTo("approve"));
            Assert.That(have.GetPolicyVersion(), Is.EqualTo("20210210"));
            Assert.That(have.GetResourceKind(), Is.EqualTo("leave_request"));
            
            Assert.That(have.HasValidationErrors(), Is.True);
            Assert.That(have.GetValidationErrors().Count(), Is.EqualTo(2));
            
            Assert.That(have.IsAlwaysDenied(), Is.True);
            Assert.That(have.IsAlwaysAllowed(), Is.False);
            Assert.That(have.IsConditional(), Is.False);
        }
    }
}

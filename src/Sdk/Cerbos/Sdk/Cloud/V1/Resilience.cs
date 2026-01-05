// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Grpc.Core;
using Polly;
using Polly.CircuitBreaker;

namespace Cerbos.Sdk.Cloud.V1
{
    internal class Resilience
    {
        public static ResiliencePipeline Pipeline = NewInstance();

        private Resilience() { }

        private static ResiliencePipeline NewInstance()
        {
            var builder = new ResiliencePipelineBuilder().
            AddCircuitBreaker(
                new CircuitBreakerStrategyOptions
                {
                    FailureRatio = 0.6,
                    SamplingDuration = TimeSpan.FromSeconds(15),
                    MinimumThroughput = 5,
                    BreakDuration = TimeSpan.FromSeconds(30),
                    ShouldHandle = new PredicateBuilder()
                        .Handle(
                            (RpcException e) =>
                            {
                                switch (e.StatusCode)
                                {
                                    case StatusCode.Aborted:
                                    case StatusCode.Cancelled:
                                    case StatusCode.DeadlineExceeded:
                                    case StatusCode.FailedPrecondition:
                                        return false;
                                    default:
                                        return true;
                                }
                            }
                        )
                }
            );

            return builder.Build();
        }
    }
}

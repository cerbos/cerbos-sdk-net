// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Linq;
using Grpc.Core;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace Cerbos.Sdk.Cloud.V1
{
    internal class Resilience
    {
        private CircuitBreakerStrategyOptions CircuitBreakerOptions { get; set; }
        private RetryStrategyOptions RetryOptions { get; set; }

        private Resilience() { }

        public static Resilience NewInstance()
        {
            return new Resilience();
        }

        public Resilience WithRetry(params StatusCode[] statusCodesToHandle)
        {
            if (RetryOptions == null)
            { 
                RetryOptions = new RetryStrategyOptions
                {
                    MaxRetryAttempts = 5,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    ShouldHandle = new PredicateBuilder()
                        .Handle(
                            (RpcException e) =>
                            {
                                if (statusCodesToHandle.Contains(e.StatusCode))
                                {
                                    return true;
                                }

                                return false;
                            }
                        )
                };
            }

            return this;
        }

        public Resilience WithCircuitBreaker()
        {
            if (CircuitBreakerOptions == null)
            {
                CircuitBreakerOptions = new CircuitBreakerStrategyOptions
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
                };
            }

            return this;
        }

        public ResiliencePipeline Build()
        {
            var pipeline = new ResiliencePipelineBuilder();
            if (CircuitBreakerOptions != null)
            {
                pipeline.AddCircuitBreaker(CircuitBreakerOptions);
            }

            if (RetryOptions != null)
            {
                pipeline.AddRetry(RetryOptions);
            }

            return pipeline.Build();
        }
    }
}

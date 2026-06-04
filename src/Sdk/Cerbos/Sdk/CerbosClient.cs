// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Sdk.Builder;
using Cerbos.Sdk.Response;
using Grpc.Core;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CerbosClient provides a client implementation that communicates with the PDP.
    /// </summary>
    public sealed class CerbosClient : ICerbosClient
    {
        private Api.V1.Svc.CerbosService.CerbosServiceClient CerbosServiceClient { get; }
        private Grpc.Health.V1.Health.HealthClient HealthClient { get; }
        private readonly Metadata _metadata;

        public CerbosClient(
            Api.V1.Svc.CerbosService.CerbosServiceClient cerbosServiceClient,
            Grpc.Health.V1.Health.HealthClient healthClient,
            Metadata metadata = null
        )
        {
            CerbosServiceClient = cerbosServiceClient;
            HealthClient = healthClient;
            _metadata = metadata;
        }

        /// <summary>
        /// Send a request consisting of the service name to see if the service is up and running.
        /// </summary>
        public HealthCheckResponse CheckHealth(HealthCheckRequest request, Metadata headers = null)
        {
            try
            {
                var req = request.ToHealthCheckRequest();
                return Utility.Rpc.Call(req, () =>
                    new HealthCheckResponse(
                        HealthClient.Check(req, Utility.Metadata.Merge(_metadata, headers))
                    )
                );
            }
            catch (RpcException e)
            {
                if (request.Service == HealthCheckRequest.Types.Service.Admin && e.StatusCode == StatusCode.NotFound)
                {
                    return new HealthCheckResponse(HealthCheckResponse.Types.ServiceStatus.Disabled);
                }

                throw;
            }
        }

        /// <summary>
        /// Send an async request consisting of the service name to see if the service is up and running.
        /// </summary>
        public Task<HealthCheckResponse> CheckHealthAsync(HealthCheckRequest request, Metadata headers = null)
        {
            try
            {
                var req = request.ToHealthCheckRequest();
                return Utility.Rpc.CallAsync(req, async () =>
                    new HealthCheckResponse(
                        await HealthClient
                            .CheckAsync(req, Utility.Metadata.Merge(_metadata, headers))
                            .ResponseAsync
                    )
                );
            }
            catch (RpcException e)
            {
                if (request.Service == HealthCheckRequest.Types.Service.Admin && e.StatusCode == StatusCode.NotFound)
                {
                    return Task.FromResult(new HealthCheckResponse(HealthCheckResponse.Types.ServiceStatus.Disabled));
                }

                throw;
            }
        }

        /// <summary>
        /// Send a request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        public CheckResourcesResponse CheckResources(CheckResourcesRequest request, Metadata headers = null)
        {
            var req = request.ToCheckResourcesRequest();
            return Utility.Rpc.Call(req, () =>
                new CheckResourcesResponse(
                    CerbosServiceClient.CheckResources(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        /// <summary>
        /// Send an async request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        public Task<CheckResourcesResponse> CheckResourcesAsync(CheckResourcesRequest request, Metadata headers = null)
        {
            var req = request.ToCheckResourcesRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new CheckResourcesResponse(
                    await CerbosServiceClient
                        .CheckResourcesAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        /// <summary>
        /// Obtain a query plan for performing the given action on the given resource kind.
        /// </summary>
        public PlanResourcesResponse PlanResources(PlanResourcesRequest request, Metadata headers = null)
        {
            var req = request.ToPlanResourcesRequest();
            return Utility.Rpc.Call(req, () =>
                new PlanResourcesResponse(
                    CerbosServiceClient.PlanResources(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        /// <summary>
        /// Obtain a query plan for performing the given action on the given resource kind.
        /// </summary>
        public Task<PlanResourcesResponse> PlanResourcesAsync(PlanResourcesRequest request, Metadata headers = null)
        {
            var req = request.ToPlanResourcesRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new PlanResourcesResponse(
                    await CerbosServiceClient
                        .PlanResourcesAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }
    }
}
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
                return new HealthCheckResponse(HealthClient.Check(request.ToHealthCheckRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (RpcException e)
            {
                if (request.Service == HealthCheckRequest.Types.Service.Admin && e.StatusCode == StatusCode.NotFound)
                {
                    return new HealthCheckResponse(HealthCheckResponse.Types.ServiceStatus.Disabled);
                }

                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to check health: ${e}");
            }
        }

        /// <summary>
        /// Send an async request consisting of the service name to see if the service is up and running.
        /// </summary>
        public Task<HealthCheckResponse> CheckHealthAsync(HealthCheckRequest request, Metadata headers = null)
        {
            try
            {
                return HealthClient
                    .CheckAsync(request.ToHealthCheckRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new HealthCheckResponse(r.Result)
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
            catch (Exception e)
            {
                throw new Exception($"Failed to check health: ${e}");
            }
        }

        /// <summary>
        /// Send a request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        public CheckResourcesResponse CheckResources(CheckResourcesRequest request, Metadata headers = null)
        {
            try
            {
                return new CheckResourcesResponse(CerbosServiceClient.CheckResources(request.ToCheckResourcesRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to check resources: ${e}");
            }
        }

        /// <summary>
        /// Send an async request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        public Task<CheckResourcesResponse> CheckResourcesAsync(CheckResourcesRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosServiceClient
                    .CheckResourcesAsync(request.ToCheckResourcesRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new CheckResourcesResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to check resources: ${e}");
            }
        }

        /// <summary>
        /// Obtain a query plan for performing the given action on the given resource kind.
        /// </summary>
        public PlanResourcesResponse PlanResources(PlanResourcesRequest request, Metadata headers = null)
        {
            try
            {
                return new PlanResourcesResponse(CerbosServiceClient.PlanResources(request.ToPlanResourcesRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to plan resources: ${e}");
            }
        }

        /// <summary>
        /// Obtain a query plan for performing the given action on the given resource kind.
        /// </summary>
        public Task<PlanResourcesResponse> PlanResourcesAsync(PlanResourcesRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosServiceClient
                    .PlanResourcesAsync(request.ToPlanResourcesRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new PlanResourcesResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to plan resources: ${e}");
            }
        }
    }
}
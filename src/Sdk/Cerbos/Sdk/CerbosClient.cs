// Copyright 2021-2024 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Sdk.Response;
using Grpc.Core;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CerbosClient provides a client implementation that communicates with the PDP.
    /// </summary>
    public sealed class CerbosClient: ICerbosClient
    {
        private Api.V1.Svc.CerbosService.CerbosServiceClient CerbosServiceClient { get; }
        private readonly Metadata _metadata;
        
        public CerbosClient(Api.V1.Svc.CerbosService.CerbosServiceClient cerbosServiceClient, Metadata metadata = null)
        {
            CerbosServiceClient = cerbosServiceClient;
            _metadata = metadata;
        }

        /// <summary>
        /// Send a request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        public CheckResourcesResponse CheckResources(Builder.CheckResourcesRequest request, Metadata headers = null)
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
        public Task<CheckResourcesResponse> CheckResourcesAsync(Builder.CheckResourcesRequest request, Metadata headers = null)
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
        public PlanResourcesResponse PlanResources(Builder.PlanResourcesRequest request, Metadata headers = null)
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
        public Task<PlanResourcesResponse> PlanResourcesAsync(Builder.PlanResourcesRequest request, Metadata headers = null)
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
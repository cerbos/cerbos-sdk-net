// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Sdk.Response;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CerbosClient provides a client implementation that communicates with the PDP.
    /// </summary>
    public sealed class CerbosClient
    {
        private Api.V1.Svc.CerbosService.CerbosServiceClient CerbosServiceClient { get; }

        public CerbosClient(Api.V1.Svc.CerbosService.CerbosServiceClient cerbosServiceClient)
        {
            CerbosServiceClient = cerbosServiceClient;
        }

        /// <summary>
        /// Send a request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        public CheckResourcesResponse CheckResources(Builder.CheckResourcesRequest request)
        {
            try
            {
                return new CheckResourcesResponse(CerbosServiceClient.CheckResources(request.ToCheckResourcesRequest()));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to check resources: ${e}");
            }
        }
        
        /// <summary>
        /// Send an async request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        public Task<CheckResourcesResponse> CheckResourcesAsync(Builder.CheckResourcesRequest request)
        {
            try
            {
                return CerbosServiceClient
                    .CheckResourcesAsync(request.ToCheckResourcesRequest())
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
        public PlanResourcesResponse PlanResources(Builder.PlanResourcesRequest request)
        {
            try
            {
                return new PlanResourcesResponse(CerbosServiceClient.PlanResources(request.ToPlanResourcesRequest()));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to plan resources: ${e}");
            }
        }
        
        /// <summary>
        /// Obtain a query plan for performing the given action on the given resource kind.
        /// </summary>
        public Task<PlanResourcesResponse> PlanResourcesAsync(Builder.PlanResourcesRequest request)
        {
            try
            {
                return CerbosServiceClient
                    .PlanResourcesAsync(request.ToPlanResourcesRequest())
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
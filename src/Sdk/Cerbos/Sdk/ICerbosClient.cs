// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;
using Cerbos.Sdk.Builder;
using Cerbos.Sdk.Response;
using Grpc.Core;

namespace Cerbos.Sdk
{
    public interface ICerbosClient
    {
        HealthCheckResponse CheckHealth(HealthCheckRequest request, Metadata headers = null);
        Task<HealthCheckResponse> CheckHealthAsync(HealthCheckRequest request, Metadata headers = null);
        CheckResourcesResponse CheckResources(CheckResourcesRequest request, Metadata headers = null);
        Task<CheckResourcesResponse> CheckResourcesAsync(CheckResourcesRequest request, Metadata headers = null);
        PlanResourcesResponse PlanResources(PlanResourcesRequest request, Metadata headers = null);
        Task<PlanResourcesResponse> PlanResourcesAsync(PlanResourcesRequest request, Metadata headers = null);
    }
}
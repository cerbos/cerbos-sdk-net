// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;
using Cerbos.Sdk.Response;
using Grpc.Core;

namespace Cerbos.Sdk
{
    public interface ICerbosClient
    {
        CheckResourcesResponse CheckResources(Builder.CheckResourcesRequest request, Metadata headers = null);
        Task<CheckResourcesResponse> CheckResourcesAsync(Builder.CheckResourcesRequest request, Metadata headers = null);
        PlanResourcesResponse PlanResources(Builder.PlanResourcesRequest request, Metadata headers = null);
        Task<PlanResourcesResponse> PlanResourcesAsync(Builder.PlanResourcesRequest request, Metadata headers = null);
    }
}
// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Cerbos.Api.V1.Response;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CheckResourcesResult provides an interface to see whether some actions are allowed on multiple resources.
    /// </summary>
    /// <remarks>
    /// <see cref="Cerbos.Sdk.CheckResourcesResult"/> covers the results where multiple resources with one or more actions exists.
    /// Please see <see cref="Cerbos.Sdk.CheckResult"/> for single resource and one or more actions.
    /// </remarks>
    public class CheckResourcesResult
    {
        private readonly CheckResourcesResponse _response;

        public CheckResourcesResult(CheckResourcesResponse resp) {
            _response = resp;
        }
        
        public List<CheckResult> Results()
        {
            List<CheckResult> results = new List<CheckResult>();
            foreach (var result in _response.Results)
            {
                results.Add(new CheckResult(result.Actions));
            }

            return results;
        }

        /// <summary>
        /// Find looks for the <see cref="Cerbos.Sdk.CheckResult"/> with the given <paramref name="resourceId"/>.
        /// </summary>
        /// <remarks>
        /// The <paramref name="resourceId"/> must be specified with the request send using the <see cref="Cerbos.Sdk.CerbosBlockingClient"/>. 
        /// </remarks>
        public CheckResult Find(string resourceId)
        {
            foreach (var result in _response.Results)
            {
                if (result.Resource.Id.Equals(resourceId))
                {
                    return new CheckResult(result.Actions);
                }
            }

            return null;
        }

        public CheckResourcesResponse GetRaw()
        {
            return _response;
        }
    }
}
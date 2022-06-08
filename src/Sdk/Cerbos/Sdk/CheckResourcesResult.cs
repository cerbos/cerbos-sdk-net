// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Cerbos.Api.V1.Response;

namespace Cerbos.Sdk
{
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
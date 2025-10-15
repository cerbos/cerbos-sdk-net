// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class HealthCheckResponse
    {
        private Grpc.Health.V1.HealthCheckResponse R { get; }

        public Types.ServiceStatus Status
        {
            get
            {
                switch (R.Status)
                {
                    case Grpc.Health.V1.HealthCheckResponse.Types.ServingStatus.NotServing:
                        return Types.ServiceStatus.NotServing;
                    case Grpc.Health.V1.HealthCheckResponse.Types.ServingStatus.Serving:
                        return Types.ServiceStatus.Serving;
                    default:
                        return Types.ServiceStatus.Unknown;
                }
            }
        }

        public Grpc.Health.V1.HealthCheckResponse Raw => R;

        public HealthCheckResponse(Grpc.Health.V1.HealthCheckResponse response)
        {
            R = response;
        }

        public static class Types
        {
            public enum ServiceStatus
            {
                /// <summary>
                /// The service status is unknown.
                /// </summary>
                Unknown = 0,
                /// <summary>
                /// The service is shutting down.
                /// </summary>
                NotServing = 1,
                Serving = 2
            }
        }
    }

}
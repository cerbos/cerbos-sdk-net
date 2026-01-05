// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class HealthCheckResponse
    {
        private Types.ServiceStatus status;

        public Types.ServiceStatus Status
        {
            get
            {
                return status;
            }
        }

        public HealthCheckResponse(Grpc.Health.V1.HealthCheckResponse response)
        {
            switch (response.Status)
            {
                case Grpc.Health.V1.HealthCheckResponse.Types.ServingStatus.Serving:
                    status = Types.ServiceStatus.Serving;
                    break;
                case Grpc.Health.V1.HealthCheckResponse.Types.ServingStatus.NotServing:
                    status = Types.ServiceStatus.NotServing;
                    break;
                default:
                    status = Types.ServiceStatus.NotServing;
                    break;
            }
        }

        public HealthCheckResponse(Types.ServiceStatus status)
        {
            this.status = status;
        }

        public static class Types
        {
            public enum ServiceStatus
            {
                Serving = 0,
                /// <summary>
                /// The service is shutting down.
                /// </summary>
                NotServing = 1,
                /// <summary>
                /// The service is disabled in the server configuration.
                /// </summary>
                Disabled = 2
            }
        }
    }
}

// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Builder
{
    public sealed class HealthCheckRequest
    {
        private Types.Service Service { get; set; }

        private HealthCheckRequest() { }

        public static HealthCheckRequest NewInstance()
        {
            return new HealthCheckRequest();
        }

        public HealthCheckRequest WithService(Types.Service service)
        {
            Service = service;
            return this;
        }

        public Grpc.Health.V1.HealthCheckRequest ToHealthCheckRequest()
        {
            string service;
            switch (Service)
            {
                case Types.Service.Admin:
                    service = "cerbos.svc.v1.CerbosAdminService";
                    break;
                case Types.Service.Cerbos:
                    service = "cerbos.svc.v1.CerbosService";
                    break;
                default:
                    throw new System.Exception("Service must be specified");
            }

            var request = new Grpc.Health.V1.HealthCheckRequest
            {
                Service = service
            };

            return request;
        }
    }

    public static class Types
    {
        public enum Service
        {
            Unspecified = 0,
            Cerbos = 1, // cerbos.svc.v1.CerbosService
            Admin = 2 // cerbos.svc.v1.CerbosAdminService
        }
    }
}
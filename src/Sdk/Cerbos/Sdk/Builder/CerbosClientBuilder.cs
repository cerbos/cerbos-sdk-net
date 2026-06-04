// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Security.Authentication;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;

namespace Cerbos.Sdk.Builder
{
    public sealed class CerbosClientBuilder
    {
        private const string PlaygroundInstanceHeader = "playground-instance";

        private string Target { get; }
        private string PlaygroundInstanceId { get; set; }
        private bool Plaintext { get; set; }
        private StreamReader CaCertificate { get; set; }
        private StreamReader TlsCertificate { get; set; }
        private StreamReader TlsKey { get; set; }
        private GrpcChannelOptions GrpcChannelOptions { get; set; }
        private Metadata Metadata { get; set; }

        private CerbosClientBuilder(string target)
        {
            Target = target;
        }

        public static CerbosClientBuilder ForTarget(string target)
        {
            return new CerbosClientBuilder(target);
        }

        public CerbosClientBuilder WithMetadata(Metadata headers)
        {
            Metadata = headers;
            return this;
        }

        public CerbosClientBuilder WithPlaintext()
        {
            Plaintext = true;
            return this;
        }

        public CerbosClientBuilder WithCaCertificate(StreamReader caCertificate)
        {
            CaCertificate = caCertificate;
            return this;
        }

        public CerbosClientBuilder WithTlsCertificate(StreamReader tlsCertificate)
        {
            TlsCertificate = tlsCertificate;
            return this;
        }

        public CerbosClientBuilder WithTlsKey(StreamReader tlsKey)
        {
            TlsKey = tlsKey;
            return this;
        }

        public CerbosClientBuilder WithPlaygroundInstance(string playgroundInstanceId)
        {
            PlaygroundInstanceId = playgroundInstanceId;
            return this;
        }

        public CerbosClientBuilder WithGrpcChannelOptions(GrpcChannelOptions grpcChannelOptions)
        {
            GrpcChannelOptions = grpcChannelOptions;
            return this;
        }

        private CallInvoker Channel(SslCredentials sslCredentials)
        {
            var grpcChannelOptions = GrpcChannelOptions ?? new GrpcChannelOptions();
            if (sslCredentials != null)
            {
                grpcChannelOptions.Credentials = sslCredentials;
            }
            else if (!Plaintext)
            {
                grpcChannelOptions.Credentials = ChannelCredentials.SecureSsl;
            }

            return GrpcChannel
                .ForAddress(Target, grpcChannelOptions)
                .Intercept();
        }

        public ICerbosClient Build()
        {
            if (string.IsNullOrEmpty(Target))
            {
                throw new Exception("Target must not be empty");
            }

            if ((CaCertificate != null || TlsCertificate != null || TlsKey != null) && Plaintext)
            {
                throw new Exception("TLS certificates and plaintext must not be specified at the same time");
            }

            if (!string.IsNullOrEmpty(PlaygroundInstanceId) && Plaintext)
            {
                throw new Exception(
                    "Plaintext connections are not supported by the Cerbos Playground"
                );
            }

            Metadata combined = Metadata;
            if (!string.IsNullOrEmpty(PlaygroundInstanceId))
            {
                combined = Utility.Metadata.Merge(
                    Metadata,
                    new Metadata {
                        { PlaygroundInstanceHeader, PlaygroundInstanceId.Trim() }
                    }
                );
            }

            SslCredentials sslCredentials = null;
            if (CaCertificate != null)
            {
                if (TlsCertificate != null && TlsKey != null)
                {
                    sslCredentials = new SslCredentials(CaCertificate.ReadToEnd(), new KeyCertificatePair(TlsCertificate.ReadToEnd(), TlsKey.ReadToEnd()));
                }
                else
                {
                    sslCredentials = new SslCredentials(CaCertificate.ReadToEnd());
                }
            }

            var grpcChannel = Channel(sslCredentials);
            return new CerbosClient(new Api.V1.Svc.CerbosService.CerbosServiceClient(grpcChannel), new Grpc.Health.V1.Health.HealthClient(grpcChannel), combined);
        }

        public ICerbosAdminClient BuildAdminClient(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new InvalidCredentialException("Username and password must be specified");
            }

            if (string.IsNullOrEmpty(Target))
            {
                throw new Exception("Target must not be empty");
            }

            if ((CaCertificate != null || TlsCertificate != null || TlsKey != null) && Plaintext)
            {
                throw new Exception("TLS certificates and plaintext must not be specified at the same time");
            }

            SslCredentials sslCredentials = null;
            if (CaCertificate != null)
            {
                if (TlsCertificate != null && TlsKey != null)
                {
                    sslCredentials = new SslCredentials(CaCertificate.ReadToEnd(), new KeyCertificatePair(TlsCertificate.ReadToEnd(), TlsKey.ReadToEnd()));
                }
                else
                {
                    sslCredentials = new SslCredentials(CaCertificate.ReadToEnd());
                }
            }

            string adminAPICreds = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));
            string authorizationHeader = $"Basic {adminAPICreds}";
            Metadata combined = Utility.Metadata.Merge(
                    Metadata,
                    new Metadata {
                        { "Authorization", authorizationHeader }
                    }
                );

            return new CerbosAdminClient(new Api.V1.Svc.CerbosAdminService.CerbosAdminServiceClient(Channel(sslCredentials)), combined);
        }
    }
}
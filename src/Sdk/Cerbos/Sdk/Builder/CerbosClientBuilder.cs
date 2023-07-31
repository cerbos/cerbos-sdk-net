// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
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

        private CerbosClientBuilder(string target) {
            Target = target;
        }

        public static CerbosClientBuilder ForTarget(string target)
        {
            return new CerbosClientBuilder(target);
        }

        public CerbosClientBuilder WithPlaintext() {
            Plaintext = true;
            return this;
        }

        public CerbosClientBuilder WithCaCertificate(StreamReader caCertificate) {
            CaCertificate = caCertificate;
            return this;
        }

        public CerbosClientBuilder WithTlsCertificate(StreamReader tlsCertificate) {
            TlsCertificate = tlsCertificate;
            return this;
        }

        public CerbosClientBuilder WithTlsKey(StreamReader tlsKey) {
            TlsKey = tlsKey;
            return this;
        }

        public CerbosClientBuilder WithPlaygroundInstance(string playgroundInstanceId)
        {
            PlaygroundInstanceId = playgroundInstanceId;
            return this;
        }

        public CerbosClient BuildClient()
        {
            if (string.IsNullOrEmpty(Target))
            {
                throw new Exception(string.Format($"Invalid target [{Target}]"));
            }

            CallCredentials callCredentials = null;
            SslCredentials sslCredentials = null;

            if (!string.IsNullOrEmpty(PlaygroundInstanceId))
            {
                callCredentials = CallCredentials.FromInterceptor((context, metadata) =>
                {
                    metadata.Add(PlaygroundInstanceHeader, PlaygroundInstanceId.Trim());
                    return Task.CompletedTask;
                });
            }
            
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

            if (Plaintext && callCredentials != null)
            {
                throw new Exception(
                    "It is not possible to connect to a playground instance using credentials if connection is plaintext due to the nature of gRPC"
                    );
            }
            
            GrpcChannel channel;
            if (Plaintext)
            {
                channel = GrpcChannel.ForAddress(Target);
            }
            else
            {
                GrpcChannelOptions grpcChannelOptions = new GrpcChannelOptions();
                if (callCredentials != null && sslCredentials != null)
                {
                    grpcChannelOptions.Credentials = ChannelCredentials.Create(sslCredentials, callCredentials);
                }
                else if (sslCredentials != null)
                {
                    grpcChannelOptions.Credentials = sslCredentials;
                }
                else if (callCredentials != null)
                {
                    grpcChannelOptions.Credentials = ChannelCredentials.Create(ChannelCredentials.SecureSsl, callCredentials);
                }
                
                channel = GrpcChannel.ForAddress(Target, grpcChannelOptions);
            }

            return new CerbosClient(new Api.V1.Svc.CerbosService.CerbosServiceClient(channel));
        }
    }
}
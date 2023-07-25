// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using Cerbos.Api.V1.Svc;
using Grpc.Net.Client;

namespace Cerbos.Sdk.Builders
{
    public class CerbosClientBuilder
    {
        private const string PlaygroundInstanceHeader = "playground-instance";
        
        private readonly string _target;
        private string _playgroundInstanceId;
        private bool _plaintext;
        private StreamReader _caCertificate;
        private StreamReader _tlsCertificate;
        private StreamReader _tlsKey;

        public CerbosClientBuilder(string target) {
            _target = target;
        }
        
        public CerbosClientBuilder WithPlaintext() {
            _plaintext = true;
            return this;
        }

        public CerbosClientBuilder WithCaCertificate(StreamReader caCertificate) {
            _caCertificate = caCertificate;
            return this;
        }

        public CerbosClientBuilder WithTlsCertificate(StreamReader tlsCertificate) {
            _tlsCertificate = tlsCertificate;
            return this;
        }

        public CerbosClientBuilder WithTlsKey(StreamReader tlsKey) {
            _tlsKey = tlsKey;
            return this;
        }

        public CerbosClientBuilder WithPlaygroundInstance(string playgroundInstanceId)
        {
            _playgroundInstanceId = playgroundInstanceId;
            return this;
        }

        public CerbosClient BuildClient()
        {
            if (_target == "")
            {
                throw new Exception(string.Format($"Invalid target [{_target}]"));
            }

            CallCredentials callCredentials = null;
            SslCredentials sslCredentials = null;

            if (!string.IsNullOrEmpty(_playgroundInstanceId))
            {
                callCredentials = CallCredentials.FromInterceptor((context, metadata) =>
                {
                    metadata.Add(PlaygroundInstanceHeader, _playgroundInstanceId.Trim());
                    return Task.CompletedTask;
                });
            }
            
            if (_caCertificate != null)
            {
                if (_tlsCertificate != null && _tlsKey != null)
                {
                    sslCredentials = new SslCredentials(_caCertificate.ReadToEnd(), new KeyCertificatePair(_tlsCertificate.ReadToEnd(), _tlsKey.ReadToEnd()));
                }
                else
                {
                    sslCredentials = new SslCredentials(_caCertificate.ReadToEnd());   
                }
            }

            if (_plaintext && callCredentials != null)
            {
                throw new Exception(
                    "It is not possible to connect to a playground instance using credentials if connection is plaintext due to the nature of gRPC");
            }
            
            GrpcChannel channel;
            if (_plaintext)
            {
                channel = GrpcChannel.ForAddress(_target);
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
                
                channel = GrpcChannel.ForAddress(_target, grpcChannelOptions);
            }

            return new CerbosClient(new CerbosService.CerbosServiceClient(channel));
        }
    }
}
// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using Grpc.Core;
using Cerbos.Api.V1.Svc;

namespace Cerbos.Sdk.Builders
{
    public class CerbosClientBuilder
    {
        private string _target;
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
        
        public CerbosBlockingClient BuildBlockingClient()
        {
            if (_target == "")
            {
                throw new Exception(string.Format($"Invalid target [{_target}]"));
            }

            Channel channel;
            if (_plaintext)
            {
                channel = new Channel(_target, ChannelCredentials.Insecure);
            }
            else
            {
                var secureChanel = new SslCredentials(_caCertificate.ReadToEnd(), new KeyCertificatePair(_tlsCertificate.ReadToEnd(), _tlsKey.ReadToEnd()));
                channel = new Channel(_target, secureChanel);
            }

            var csc = new CerbosService.CerbosServiceClient(channel);
            var casc = new CerbosAdminService.CerbosAdminServiceClient(channel);
            var cpsc = new CerbosPlaygroundService.CerbosPlaygroundServiceClient(channel);

            return new CerbosBlockingClient(csc, casc, cpsc);
        }
    }
}
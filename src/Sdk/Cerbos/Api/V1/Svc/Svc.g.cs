// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: cerbos/svc/v1/svc.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Cerbos.Api.V1.Svc {

  /// <summary>Holder for reflection information generated from cerbos/svc/v1/svc.proto</summary>
  public static partial class SvcReflection {

    #region Descriptor
    /// <summary>File descriptor for cerbos/svc/v1/svc.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SvcReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChdjZXJib3Mvc3ZjL3YxL3N2Yy5wcm90bxINY2VyYm9zLnN2Yy52MRofY2Vy",
            "Ym9zL3JlcXVlc3QvdjEvcmVxdWVzdC5wcm90bxohY2VyYm9zL3Jlc3BvbnNl",
            "L3YxL3Jlc3BvbnNlLnByb3RvGhxnb29nbGUvYXBpL2Fubm90YXRpb25zLnBy",
            "b3RvGi5wcm90b2MtZ2VuLW9wZW5hcGl2Mi9vcHRpb25zL2Fubm90YXRpb25z",
            "LnByb3RvMvIKCg1DZXJib3NTZXJ2aWNlEqcCChBDaGVja1Jlc291cmNlU2V0",
            "EiouY2VyYm9zLnJlcXVlc3QudjEuQ2hlY2tSZXNvdXJjZVNldFJlcXVlc3Qa",
            "LC5jZXJib3MucmVzcG9uc2UudjEuQ2hlY2tSZXNvdXJjZVNldFJlc3BvbnNl",
            "IrgBgtPkkwIPOgEqIgovYXBpL2NoZWNrkkGfARIFQ2hlY2sakwFbRGVwcmVj",
            "YXRlZDogVXNlIENoZWNrUmVzb3VyY2VzIEFQSSBpbnN0ZWFkXSBDaGVjayB3",
            "aGV0aGVyIGEgcHJpbmNpcGFsIGhhcyBwZXJtaXNzaW9ucyB0byBwZXJmb3Jt",
            "IHRoZSBnaXZlbiBhY3Rpb25zIG9uIGEgc2V0IG9mIHJlc291cmNlIGluc3Rh",
            "bmNlcy5YARK2AgoSQ2hlY2tSZXNvdXJjZUJhdGNoEiwuY2VyYm9zLnJlcXVl",
            "c3QudjEuQ2hlY2tSZXNvdXJjZUJhdGNoUmVxdWVzdBouLmNlcmJvcy5yZXNw",
            "b25zZS52MS5DaGVja1Jlc291cmNlQmF0Y2hSZXNwb25zZSLBAYLT5JMCHjoB",
            "KiIZL2FwaS9jaGVja19yZXNvdXJjZV9iYXRjaJJBmQESFENoZWNrIHJlc291",
            "cmNlIGJhdGNoGn9bRGVwcmVjYXRlZDogVXNlIENoZWNrUmVzb3VyY2VzIEFQ",
            "SSBpbnN0ZWFkXSBDaGVjayBhIHByaW5jaXBhbCdzIHBlcm1pc3Npb25zIHRv",
            "IGEgYmF0Y2ggb2YgaGV0ZXJvZ2VuZW91cyByZXNvdXJjZXMgYW5kIGFjdGlv",
            "bnMuWAES8AEKDkNoZWNrUmVzb3VyY2VzEiguY2VyYm9zLnJlcXVlc3QudjEu",
            "Q2hlY2tSZXNvdXJjZXNSZXF1ZXN0GiouY2VyYm9zLnJlc3BvbnNlLnYxLkNo",
            "ZWNrUmVzb3VyY2VzUmVzcG9uc2UihwGC0+STAhk6ASoiFC9hcGkvY2hlY2sv",
            "cmVzb3VyY2VzkkFlEg9DaGVjayByZXNvdXJjZXMaUkNoZWNrIGEgcHJpbmNp",
            "cGFsJ3MgcGVybWlzc2lvbnMgdG8gYSBiYXRjaCBvZiBoZXRlcm9nZW5lb3Vz",
            "IHJlc291cmNlcyBhbmQgYWN0aW9ucy4SxQEKClNlcnZlckluZm8SJC5jZXJi",
            "b3MucmVxdWVzdC52MS5TZXJ2ZXJJbmZvUmVxdWVzdBomLmNlcmJvcy5yZXNw",
            "b25zZS52MS5TZXJ2ZXJJbmZvUmVzcG9uc2UiaYLT5JMCEhIQL2FwaS9zZXJ2",
            "ZXJfaW5mb5JBThIWR2V0IHNlcnZlciBpbmZvcm1hdGlvbho0R2V0IGluZm9y",
            "bWF0aW9uIGFib3V0IHRoZSBzZXJ2ZXIgZS5nLiBzZXJ2ZXIgdmVyc2lvbhKf",
            "AgoNUGxhblJlc291cmNlcxInLmNlcmJvcy5yZXF1ZXN0LnYxLlBsYW5SZXNv",
            "dXJjZXNSZXF1ZXN0GikuY2VyYm9zLnJlc3BvbnNlLnYxLlBsYW5SZXNvdXJj",
            "ZXNSZXNwb25zZSK5AYLT5JMCNDoBKloaOgEqIhUvYXBpL3gvcGxhbi9yZXNv",
            "dXJjZXMiEy9hcGkvcGxhbi9yZXNvdXJjZXOSQXwSDlBsYW4gcmVzb3VyY2Vz",
            "GmpQcm9kdWNlIGEgcXVlcnkgcGxhbiB3aXRoIGNvbmRpdGlvbnMgdGhhdCBt",
            "dXN0IGJlIHNhdGlzZmllZCBmb3IgYWNjZXNzaW5nIGEgc2V0IG9mIGluc3Rh",
            "bmNlcyBvZiBhIHJlc291cmNlGiGSQR4SHENlcmJvcyBQb2xpY3kgRGVjaXNp",
            "b24gUG9pbnQysAwKEkNlcmJvc0FkbWluU2VydmljZRLJAQoRQWRkT3JVcGRh",
            "dGVQb2xpY3kSKy5jZXJib3MucmVxdWVzdC52MS5BZGRPclVwZGF0ZVBvbGlj",
            "eVJlcXVlc3QaLS5jZXJib3MucmVzcG9uc2UudjEuQWRkT3JVcGRhdGVQb2xp",
            "Y3lSZXNwb25zZSJYgtPkkwImOgEqWhI6ASoaDS9hZG1pbi9wb2xpY3kiDS9h",
            "ZG1pbi9wb2xpY3mSQSkSFkFkZCBvciB1cGRhdGUgcG9saWNpZXNiDwoNCglC",
            "YXNpY0F1dGgSABKcAQoMTGlzdFBvbGljaWVzEiYuY2VyYm9zLnJlcXVlc3Qu",
            "djEuTGlzdFBvbGljaWVzUmVxdWVzdBooLmNlcmJvcy5yZXNwb25zZS52MS5M",
            "aXN0UG9saWNpZXNSZXNwb25zZSI6gtPkkwIREg8vYWRtaW4vcG9saWNpZXOS",
            "QSASDUxpc3QgcG9saWNpZXNiDwoNCglCYXNpY0F1dGgSABKOAQoJR2V0UG9s",
            "aWN5EiMuY2VyYm9zLnJlcXVlc3QudjEuR2V0UG9saWN5UmVxdWVzdBolLmNl",
            "cmJvcy5yZXNwb25zZS52MS5HZXRQb2xpY3lSZXNwb25zZSI1gtPkkwIPEg0v",
            "YWRtaW4vcG9saWN5kkEdEgpHZXQgcG9saWN5Yg8KDQoJQmFzaWNBdXRoEgAS",
            "yAEKE0xpc3RBdWRpdExvZ0VudHJpZXMSLS5jZXJib3MucmVxdWVzdC52MS5M",
            "aXN0QXVkaXRMb2dFbnRyaWVzUmVxdWVzdBovLmNlcmJvcy5yZXNwb25zZS52",
            "MS5MaXN0QXVkaXRMb2dFbnRyaWVzUmVzcG9uc2UiT4LT5JMCHRIbL2FkbWlu",
            "L2F1ZGl0bG9nL2xpc3Qve2tpbmR9kkEpEhZMaXN0IGF1ZGl0IGxvZyBlbnRy",
            "aWVzYg8KDQoJQmFzaWNBdXRoEgAwARLHAQoRQWRkT3JVcGRhdGVTY2hlbWES",
            "Ky5jZXJib3MucmVxdWVzdC52MS5BZGRPclVwZGF0ZVNjaGVtYVJlcXVlc3Qa",
            "LS5jZXJib3MucmVzcG9uc2UudjEuQWRkT3JVcGRhdGVTY2hlbWFSZXNwb25z",
            "ZSJWgtPkkwImOgEqWhI6ASoaDS9hZG1pbi9zY2hlbWEiDS9hZG1pbi9zY2hl",
            "bWGSQScSFEFkZCBvciB1cGRhdGUgc2NoZW1hYg8KDQoJQmFzaWNBdXRoEgAS",
            "lwEKC0xpc3RTY2hlbWFzEiUuY2VyYm9zLnJlcXVlc3QudjEuTGlzdFNjaGVt",
            "YXNSZXF1ZXN0GicuY2VyYm9zLnJlc3BvbnNlLnYxLkxpc3RTY2hlbWFzUmVz",
            "cG9uc2UiOILT5JMCEBIOL2FkbWluL3NjaGVtYXOSQR8SDExpc3Qgc2NoZW1h",
            "c2IPCg0KCUJhc2ljQXV0aBIAEo4BCglHZXRTY2hlbWESIy5jZXJib3MucmVx",
            "dWVzdC52MS5HZXRTY2hlbWFSZXF1ZXN0GiUuY2VyYm9zLnJlc3BvbnNlLnYx",
            "LkdldFNjaGVtYVJlc3BvbnNlIjWC0+STAg8SDS9hZG1pbi9zY2hlbWGSQR0S",
            "CkdldCBzY2hlbWFiDwoNCglCYXNpY0F1dGgSABKaAQoMRGVsZXRlU2NoZW1h",
            "EiYuY2VyYm9zLnJlcXVlc3QudjEuRGVsZXRlU2NoZW1hUmVxdWVzdBooLmNl",
            "cmJvcy5yZXNwb25zZS52MS5EZWxldGVTY2hlbWFSZXNwb25zZSI4gtPkkwIP",
            "Kg0vYWRtaW4vc2NoZW1hkkEgEg1EZWxldGUgc2NoZW1hYg8KDQoJQmFzaWNB",
            "dXRoEgASnAEKC1JlbG9hZFN0b3JlEiUuY2VyYm9zLnJlcXVlc3QudjEuUmVs",
            "b2FkU3RvcmVSZXF1ZXN0GicuY2VyYm9zLnJlc3BvbnNlLnYxLlJlbG9hZFN0",
            "b3JlUmVzcG9uc2UiPYLT5JMCFRITL2FkbWluL3N0b3JlL3JlbG9hZJJBHxIM",
            "UmVsb2FkIHN0b3JlYg8KDQoJQmFzaWNBdXRoEgAaIpJBHxIdQ2VyYm9zIGFk",
            "bWluaXN0cmF0aW9uIHNlcnZpY2Uy5QQKF0NlcmJvc1BsYXlncm91bmRTZXJ2",
            "aWNlEpcBChJQbGF5Z3JvdW5kVmFsaWRhdGUSLC5jZXJib3MucmVxdWVzdC52",
            "MS5QbGF5Z3JvdW5kVmFsaWRhdGVSZXF1ZXN0Gi4uY2VyYm9zLnJlc3BvbnNl",
            "LnYxLlBsYXlncm91bmRWYWxpZGF0ZVJlc3BvbnNlIiOC0+STAh06ASoiGC9h",
            "cGkvcGxheWdyb3VuZC92YWxpZGF0ZRKHAQoOUGxheWdyb3VuZFRlc3QSKC5j",
            "ZXJib3MucmVxdWVzdC52MS5QbGF5Z3JvdW5kVGVzdFJlcXVlc3QaKi5jZXJi",
            "b3MucmVzcG9uc2UudjEuUGxheWdyb3VuZFRlc3RSZXNwb25zZSIfgtPkkwIZ",
            "OgEqIhQvYXBpL3BsYXlncm91bmQvdGVzdBKXAQoSUGxheWdyb3VuZEV2YWx1",
            "YXRlEiwuY2VyYm9zLnJlcXVlc3QudjEuUGxheWdyb3VuZEV2YWx1YXRlUmVx",
            "dWVzdBouLmNlcmJvcy5yZXNwb25zZS52MS5QbGF5Z3JvdW5kRXZhbHVhdGVS",
            "ZXNwb25zZSIjgtPkkwIdOgEqIhgvYXBpL3BsYXlncm91bmQvZXZhbHVhdGUS",
            "iwEKD1BsYXlncm91bmRQcm94eRIpLmNlcmJvcy5yZXF1ZXN0LnYxLlBsYXln",
            "cm91bmRQcm94eVJlcXVlc3QaKy5jZXJib3MucmVzcG9uc2UudjEuUGxheWdy",
            "b3VuZFByb3h5UmVzcG9uc2UiIILT5JMCGjoBKiIVL2FwaS9wbGF5Z3JvdW5k",
            "L3Byb3h5QuEBChVkZXYuY2VyYm9zLmFwaS52MS5zdmNaNmdpdGh1Yi5jb20v",
            "Y2VyYm9zL2NlcmJvcy9hcGkvZ2VucGIvY2VyYm9zL3N2Yy92MTtzdmN2MaoC",
            "EUNlcmJvcy5BcGkuVjEuU3ZjkkF7Ej8KBkNlcmJvcyItCgZDZXJib3MSEmh0",
            "dHBzOi8vY2VyYm9zLmRldhoPaW5mb0BjZXJib3MuZGV2MgZsYXRlc3QqAQIy",
            "EGFwcGxpY2F0aW9uL2pzb246EGFwcGxpY2F0aW9uL2pzb25aEQoPCglCYXNp",
            "Y0F1dGgSAggBYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Cerbos.Api.V1.Request.RequestReflection.Descriptor, global::Cerbos.Api.V1.Response.ResponseReflection.Descriptor, global::Google.Api.AnnotationsReflection.Descriptor, global::Grpc.Gateway.ProtocGenOpenapiv2.Options.AnnotationsReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, null));
    }
    #endregion

  }
}

#endregion Designer generated code
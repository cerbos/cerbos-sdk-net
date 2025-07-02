[![NuGeT](https://img.shields.io/nuget/v/Cerbos.Sdk?style=plastic)](https://www.nuget.org/packages/Cerbos.Sdk)
[![NuGeT Downloads](https://img.shields.io/nuget/dt/Cerbos.Sdk?style=plastic)](https://www.nuget.org/packages/Cerbos.Sdk)

# Cerbos .NET SDK

.NET client library for the [Cerbos](https://github.com/cerbos/cerbos) open source access control solution. This library
includes gRPC clients for accessing the Cerbos PDP and [Cerbos Hub](https://hub.cerbos.cloud/).

Find out more about Cerbos at https://cerbos.dev, Cerbos Hub at https://www.cerbos.dev/product-cerbos-hub and read the documentation at https://docs.cerbos.dev.

# Installation

- Add `Cerbos.Sdk` NuGet package as dependency to the project. See [here](https://www.nuget.org/packages/Cerbos.Sdk) for the published packages.

# Examples

## Cerbos

### Creating a client without TLS

```csharp
var client = CerbosClientBuilder.ForTarget("http://localhost:3593").WithPlaintext().Build();
```

### CheckResources API

```csharp
var request = CheckResourcesRequest.NewInstance()
    .WithRequestId(RequestId.Generate())
    .WithIncludeMeta(true)
    .WithPrincipal(
        Principal.NewInstance("john", "employee")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
    )
    .WithResourceEntries(
        ResourceEntry.NewInstance("leave_request", "XX125")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
            .WithAttribute("owner", AttributeValue.StringValue("john"))
            .WithActions("approve", "view:public")
    );

var result = client.CheckResources(request).Find("XX125");
if(result.IsAllowed("approve")){ // returns true if `approve` action is allowed
    // ...
}
```

```csharp
var request = CheckResourcesRequest.NewInstance()
    .WithRequestId(RequestId.Generate())
    .WithIncludeMeta(true)
    .WithPrincipal
    (
        Principal.NewInstance("john", "employee")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
    )
    .WithResourceEntries
    (
        ResourceEntry.NewInstance("leave_request", "XX125")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
            .WithAttribute("owner", AttributeValue.StringValue("john"))
            .WithActions("view:public", "approve", "defer"),
        
        ResourceEntry.NewInstance("leave_request", "XX225")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
            .WithAttribute("owner", AttributeValue.StringValue("martha"))
            .WithActions("view:public", "approve"),
        
        ResourceEntry.NewInstance("leave_request", "XX325")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("US"))
            .WithAttribute("owner", AttributeValue.StringValue("peggy"))
            .WithActions("view:public", "approve")
    );

CheckResourcesResponse result = client.CheckResources(request);
var resultXX125 = result.Find("XX125");
var resultXX225 = result.Find("XX225");
var resultXX325 = result.Find("XX325");

if(resultXX125.IsAllowed("defer")){ // returns true if `defer` action is allowed
    // ...
}

if(resultXX225.IsAllowed("approve")){ // returns true if `approve` action is allowed
    // ...
}

if(resultXX325.IsAllowed("view:public")){ // returns true if `view:public` action is allowed
    // ...
}
```

### Plan Resources API

```csharp
var request = PlanResourcesRequest.NewInstance()
    .WithRequestId(RequestId.Generate())
    .WithIncludeMeta(true)
    .WithPrincipal
    (
        Principal.NewInstance("maggie","manager")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
            .WithAttribute("team", AttributeValue.StringValue("design"))
    )
    .WithResource
    (
        Resource.NewInstance("leave_request")
            .WithPolicyVersion("20210210")
    )
    .WithAction("approve");

PlanResourcesResponse result = client.PlanResources(request);
if(result.IsAlwaysAllowed()) {
    // ...
}
else if (result.IsAlwaysDenied()) {
    // ...
}
else {
    // ...
}
```

> [!NOTE]  
> Cerbos PDP v0.44.0 and onwards support specifying multiple actions with the following syntax: 
> ```csharp
> .WithActions("approve", "create")
> ```

## Cerbos Hub

### Creating a Cerbos Hub client

```csharp
using Cerbos.Sdk.Cloud.V1;

var hubClient = HubClientBuilder
    .FromEnv() // Gets clientId and clientSecret from environment variables CERBOS_HUB_CLIENT_ID and CERBOS_HUB_CLIENT_SECRET.
    .Build();

var storeId = Environment.GetEnvironmentVariable("CERBOS_HUB_STORE_ID");
var storeClient = hubClient.StoreClient;
```

### GetFiles API

```csharp
using Cerbos.Sdk.Cloud.V1.Store;

var request = GetFilesRequest.NewInstance(
    storeId, 
    "resource_policies/leave_request.yaml",
    "resource_policies/purchase_order.yaml"
);

var response = StoreClient.GetFiles(request);
```

### ListFiles API

```csharp
using Cerbos.Sdk.Cloud.V1.Store;

var request = ListFilesRequest
    .NewInstance(storeId);

var requestWithFilter = ListFilesRequest
    .NewInstance(
        storeId,
        FileFilter.PathContains("resource_policies")
    );

var response = StoreClient.ListFiles(request);
var filteredResponse = StoreClient.ListFiles(requestWithFilter);
```

### ModifyFiles API

```csharp
using Cerbos.Sdk.Cloud.V1.Store;

var path = "./cerbos/policies/leave_request.yaml";
var fullPath = Path.GetFullPath(path);
var fileContents = System.IO.File.ReadAllBytes(fullPath);

var requestAddOrUpdate = ModifyFilesRequest.WithChangeDetails(
    storeId,
    ChangeDetails.Internal("myApp/ModifyFiles/Op=AddOrUpdate", ChangeDetails.Types.Uploader.NewInstance("myApp"), ChangeDetails.Types.Internal.NewInstance("sdk")),
    FileOp.AddOrUpdate(File.NewInstance(path, fileContents))
);

var requestDelete = ModifyFilesRequest.WithChangeDetails(
    storeId,
    ChangeDetails.Internal("myApp/ModifyFiles/Op=Delete", ChangeDetails.Types.Uploader.NewInstance("myApp"), ChangeDetails.Types.Internal.NewInstance("sdk")),
    FileOp.Delete(path)
);

var responseAddOrUpdate = StoreClient.ModifyFiles(requestAddOrUpdate);
var responseDelete = StoreClient.ModifyFiles(requestDelete);
```

### ReplaceFiles API

```csharp
using Cerbos.Sdk.Cloud.V1.Store;

var path = "./cerbos/policies.zip";
var fullPath = Path.GetFullPath(path);
var policiesContents = System.IO.File.ReadAllBytes(fullPath);

var request = ReplaceFilesRequest.WithZippedContents(
    storeId,
    policiesContents,
    null,
    ChangeDetails.Internal("myApp/ReplaceFiles/With=policies.zip", ChangeDetails.Types.Uploader.NewInstance("myApp"), ChangeDetails.Types.Internal.NewInstance("sdk"))
);

var response = StoreClient.ReplaceFiles(request);
```

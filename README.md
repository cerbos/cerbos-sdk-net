Cerbos .NET SDK
===============

.NET client library for the [Cerbos](https://github.com/cerbos/cerbos) open source access control solution. This library
includes RPC clients for accessing the Cerbos PDP.

Find out more about Cerbos at https://cerbos.dev and read the documentation at https://docs.cerbos.dev.

Installation
-------------

- Add `Cerbos.Sdk` NuGet package as dependency to the project.

Examples
--------

### Creating a client without TLS

```csharp
var client = new CerbosClientBuilder("localhost:3593").WithPlaintext().BuildBlockingClient();
```

### Check a single principal and resource

```csharp
CheckResult result = client
    .CheckResources(
        Principal.NewInstance("john", "employee")
        .WithPolicyVersion("20210210")
        .WithAttribute("department", AttributeValue.StringValue("marketing"))
        .WithAttribute("geography", AttributeValue.StringValue("GB")),
        
        Resource.NewInstance("leave_request", "xx125")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
            .WithAttribute("owner", AttributeValue.StringValue("john")),
        
        "view:public", "approve"
    );

if(result.IsAllowed("approve")){ // returns true if `approve` action is allowed
    // ...
}
```

### Check a single principal and multiple resource & action pairs

```csharp
CheckResourcesResult result = client
    .CheckResources(
        Principal.NewInstance("john", "employee")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB")),
        
        ResourceAction.NewInstance("leave_request", "XX125")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
            .WithAttribute("owner", AttributeValue.StringValue("john"))
            .WithActions("view:public", "approve", "defer"),
        
        ResourceAction.NewInstance("leave_request", "XX225")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("GB"))
            .WithAttribute("owner", AttributeValue.StringValue("martha"))
            .WithActions("view:public", "approve"),
        
        ResourceAction.NewInstance("leave_request", "XX325")
            .WithPolicyVersion("20210210")
            .WithAttribute("department", AttributeValue.StringValue("marketing"))
            .WithAttribute("geography", AttributeValue.StringValue("US"))
            .WithAttribute("owner", AttributeValue.StringValue("peggy"))
            .WithActions("view:public", "approve")
    );

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

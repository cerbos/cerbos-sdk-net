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
CheckResult result = client.CheckResources(
                            Principal.NewInstance("john", new []{"employee"})
                            .WithPolicyVersion("20210210")
                            .WithAttribute("department", AttributeValue.StringValue("marketing"))
                            .WithAttribute("geography", AttributeValue.StringValue("GB")),
                            
                            Resource.NewInstance("leave_request", "xx125")
                                .WithPolicyVersion("20210210")
                                .WithAttribute("department", AttributeValue.StringValue("marketing"))
                                .WithAttribute("geography", AttributeValue.StringValue("GB"))
                                .WithAttribute("owner", AttributeValue.StringValue("john")),
                            
                            "view:public", 
                            "approve"
                    );

if(result.IsAllowed("approve")){ // returns true if `approve` action is allowed
    // ...
}
```

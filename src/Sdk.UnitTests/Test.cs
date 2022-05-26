using Cerbos.Sdk;
using Cerbos.Sdk.Builders;
using NuGet.Frameworks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Sdk.UnitTests
{
    public class Tests
    {
        private CerbosBlockingClient _client;
        private const string PrincipalName = "john";
        private readonly string[] _principalRoles = {"user"};
        private readonly string[] _actions = {"view"};
        
        [SetUp]
        public void Setup()
        {
            _client = new CerbosClientBuilder("localhost:3593").WithPlaintext().BuildBlockingClient();
        }

        [Test]
        public void Test1()
        {
            var result = _client.Check(Principal.NewInstance(PrincipalName, _principalRoles), Resource.NewInstance("leave_request"), _actions);
            Assert.AreEqual(true, result.IsAllowed("view"));
            Assert.Pass();
        }
    }
}

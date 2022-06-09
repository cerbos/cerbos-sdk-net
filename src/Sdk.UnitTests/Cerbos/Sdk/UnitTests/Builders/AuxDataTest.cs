using Cerbos.Sdk.Builders;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builders;

public class AuxDataTest
{
    private const string Jwt = "eyJhbGciOiJFUzM4NCIsImtpZCI6IjE5TGZaYXRFZGc4M1lOYzVyMjNndU1KcXJuND0iLCJ0eXAiOiJKV1QifQ.eyJhdWQiOlsiY2VyYm9zLWp3dC10ZXN0cyJdLCJjdXN0b21BcnJheSI6WyJBIiwiQiIsIkMiXSwiY3VzdG9tSW50Ijo0MiwiY3VzdG9tTWFwIjp7IkEiOiJBQSIsIkIiOiJCQiIsIkMiOiJDQyJ9LCJjdXN0b21TdHJpbmciOiJmb29iYXIiLCJleHAiOjE5NTAyNzc5MjYsImlzcyI6ImNlcmJvcy10ZXN0LXN1aXRlIn0._nCHIsuFI3wczeuUv_xjSwaVnIQUdYA9sGf_jVsrsDWloLs3iPWDaA1bXpuIUJVsi8-G6qqdrPI0cOBxEocg1NCm8fyD9T_3hsZV0fYWon_Je6Kl93a3JIW3S6kbvjsL";
    private const string KeySetId = "123";
    private readonly AuxData _auxData = AuxData.WithJwt(Jwt);
    private readonly AuxData _auxData1 = AuxData.WithJwt(Jwt, KeySetId);
    
    [Test]
    public void Test()
    {
        Assert.That(_auxData.ToAuxData().Jwt.Token, Is.EqualTo(Jwt));
        Assert.That(_auxData1.ToAuxData().Jwt.Token, Is.EqualTo(Jwt));
        
        Assert.That(_auxData.ToAuxData().Jwt.KeySetId, Is.EqualTo(""));
        Assert.That(_auxData1.ToAuxData().Jwt.KeySetId, Is.EqualTo(KeySetId));
    }
}
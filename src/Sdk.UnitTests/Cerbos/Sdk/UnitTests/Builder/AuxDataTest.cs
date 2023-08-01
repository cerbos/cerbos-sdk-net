// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class AuxDataTest
{
    private const string Token = "eyJhbGciOiJFUzM4NCIsImtpZCI6IjE5TGZaYXRFZGc4M1lOYzVyMjNndU1KcXJuND0iLCJ0eXAiOiJKV1QifQ.eyJhdWQiOlsiY2VyYm9zLWp3dC10ZXN0cyJdLCJjdXN0b21BcnJheSI6WyJBIiwiQiIsIkMiXSwiY3VzdG9tSW50Ijo0MiwiY3VzdG9tTWFwIjp7IkEiOiJBQSIsIkIiOiJCQiIsIkMiOiJDQyJ9LCJjdXN0b21TdHJpbmciOiJmb29iYXIiLCJleHAiOjE5NTAyNzc5MjYsImlzcyI6ImNlcmJvcy10ZXN0LXN1aXRlIn0._nCHIsuFI3wczeuUv_xjSwaVnIQUdYA9sGf_jVsrsDWloLs3iPWDaA1bXpuIUJVsi8-G6qqdrPI0cOBxEocg1NCm8fyD9T_3hsZV0fYWon_Je6Kl93a3JIW3S6kbvjsL";
    private const string KeySetId = "123";
    
    [Test]
    public void TestToken()
    {
        var auxData = AuxData.WithJwt(Token);
        Assert.That(auxData.ToAuxData().Jwt.Token, Is.EqualTo(Token));
    }

    [Test]
    public void TestTokenAndKeySetId()
    {
        var auxData = AuxData.WithJwt(Token, KeySetId);
        Assert.That(auxData.ToAuxData().Jwt.KeySetId, Is.EqualTo(KeySetId));
        Assert.That(auxData.ToAuxData().Jwt.Token, Is.EqualTo(Token));
    }
}
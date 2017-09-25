using System;
using NUnit.Framework;
using RestSharp;
using Trivial.Api.Gateway.Jeopardy;

namespace Trivial.Api.Gateway.Tests
{
    [TestFixture]
    public class ClientGatewayJeopardyTest
    {
        [Test]
        [Category("U")]
        public void CharacterHandlerTest_SpecialCharacters()
        {
            //Act
            var actual = ClientGatewayJeopardy.CharacterHandler("abc <i>xyz</i> 1");

            //Assert
            Assert.AreEqual("Abc xyz 1", actual);
        }

        [Test]
        [Category("U")]
        public void CharacterHandlerTest_NonSpecialCharacters()
        {
            //Act
            var actual = ClientGatewayJeopardy.CharacterHandler("Abc xyz 1");

            //Assert
            Assert.AreEqual("Abc xyz 1", actual);
        }
    }
}
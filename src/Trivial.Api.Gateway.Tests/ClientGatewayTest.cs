using System;
using NUnit.Framework;
using RestSharp;

namespace Trivial.Api.Gateway.Tests
{
    [TestFixture]
    public class ClientGatewayTest
    {
        [Test]
        [Category("U")]
        public void GivenNullResponse_ThenResponseIsNotValid()
        {
            //Act
            var actual = new ClientGateway().HasErrorOccured(null);

            //Assert
            Assert.IsTrue(actual);
        }

        [Test]
        [Category("U")]
        public void GivenEmptyResponse_ThenResponseIsNotValid()
        {
            //Arrange
            IRestResponse response = new RestResponse();

            //Act
            var actual = new ClientGateway().HasErrorOccured(response);

            //Assert
            Assert.IsFalse(actual);
        }

        [Test]
        [Category("U")]
        public void GivenErrorExceptionResponse_ThenResponseIsNotValid()
        {
            //Arrange
            IRestResponse response = new RestResponse{ErrorException = new ApplicationException()};

            //Act
            var actual = new ClientGateway().HasErrorOccured(response);

            //Assert
            Assert.IsTrue(actual);
        }

        [Test]
        [Category("U")]
        public void GivenErrorMessageResponse_ThenResponseIsNotValid()
        {
            //Arrange
            IRestResponse response = new RestResponse { ErrorMessage = "any" };

            //Act
            var actual = new ClientGateway().HasErrorOccured(response);

            //Assert
            Assert.IsTrue(actual);
        }

        [Test]
        [Category("U")]
        public void GivenPopulatedResponse_ThenResponseIsValid()
        {
            //Arrange
            IRestResponse response = new RestResponse { Content = "any" };

            //Act
            var actual = new ClientGateway().HasErrorOccured(response);

            //Assert
            Assert.IsFalse(actual);
        }
    }
}
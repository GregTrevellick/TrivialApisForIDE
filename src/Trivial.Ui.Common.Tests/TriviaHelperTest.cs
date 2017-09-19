using NUnit.Framework;
using System;

namespace Trivial.Ui.Common.Tests
{
    [TestFixture]
    public class TriviaHelperTest
    {
        [Test]
        [Category("U")]
        public void MidweekAndHaveNotExceededMidweekCountTest()
        {
            //Arrange
            var generalOptionsDto = new GeneralOptionsDto();

            //Act
            var actual = TriviaHelper.MidweekAndHaveNotExceededMidweekCount(generalOptionsDto);

            //Assert
            Assert.IsTrue(actual);
        }
    }
}

using NUnit.Framework;

namespace Trivial.Ui.Common.Tests
{
    [TestFixture]
    public class TriviaHelperTest
    {
        [Test]
        [TestCase(0, 1, true, false)]
        [TestCase(1, 0, true, false)]
        [TestCase(1, 1, true, false)]
        [TestCase(0, 1, false, true)]
        [TestCase(1, 0, false, false)]
        [TestCase(1, 1, false, false)]
        [Category("U")]
        public void MidweekAndHaveNotExceededMidweekCountTest(int popUpCountToday, int maximumPopUpsWeekDay, bool isWeekend, bool expected)
        {
            //Arrange
            var generalOptionsDto = new GeneralOptionsDto
            {
                MaximumPopUpsWeekDay = maximumPopUpsWeekDay,
                PopUpCountToday = popUpCountToday,
            };

            //Act
            var actual = TriviaHelper.MidweekAndHaveNotExceededMidweekCount(generalOptionsDto, isWeekend);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

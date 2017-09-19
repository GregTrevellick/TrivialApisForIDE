using System;
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

        [Test]
        [TestCase(0, 1, true, true)]
        [TestCase(1, 0, true, false)]
        [TestCase(1, 1, true, false)]
        [TestCase(0, 1, false, false)]
        [TestCase(1, 0, false, false)]
        [TestCase(1, 1, false, false)]
        [Category("U")]
        public void WeekEndAndHaveNotExceededWeekEndCountTest(int popUpCountToday, int maximumPopUpsWeekEnd, bool isWeekend, bool expected)
        {
            //Arrange
            var generalOptionsDto = new GeneralOptionsDto
            {
                MaximumPopUpsWeekEnd = maximumPopUpsWeekEnd,
                PopUpCountToday = popUpCountToday,
            };

            //Act
            var actual = TriviaHelper.WeekEndAndHaveNotExceededWeekEndCount(generalOptionsDto, isWeekend);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [Category("U")]
        public void LastPopUpMoreThanXMinutesAgoTest()
        {
            //Act
            Assert.IsFalse(TriviaHelper.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 13, 0, 0), 
                popUpIntervalInMins: 60,
                dateTimeNow:  new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsFalse(TriviaHelper.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 12, 00, 00),
                popUpIntervalInMins: 60,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsTrue(TriviaHelper.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 11, 59, 59),
                popUpIntervalInMins: 60,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsTrue(TriviaHelper.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 13, 00, 00),
                popUpIntervalInMins: 0,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsTrue(TriviaHelper.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 12, 59, 59),
                popUpIntervalInMins: 0,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));

        }
    }
}

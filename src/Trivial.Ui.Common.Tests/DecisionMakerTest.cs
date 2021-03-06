﻿using System;
using NUnit.Framework;

namespace Trivial.Ui.Common.Tests
{
    [TestFixture]
    public class DecisionMakerTest
    {
        [Test]
        [Category("U")]
        public void LastPopUpMoreThanXMinutesAgoTest()
        {
            //Arrange
            var decisionMaker = new DecisionMaker();

            //Act
            Assert.IsFalse(decisionMaker.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 13, 0, 0), 
                popUpIntervalInMins: 60,
                dateTimeNow:  new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsTrue(decisionMaker.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 12, 00, 00),
                popUpIntervalInMins: 60,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsTrue(decisionMaker.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 11, 59, 59),
                popUpIntervalInMins: 60,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsTrue(decisionMaker.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 13, 00, 00),
                popUpIntervalInMins: 0,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));

            Assert.IsTrue(decisionMaker.LastPopUpMoreThanXMinutesAgo(
                lastPopUpDateTime: new DateTime(2017, 01, 01, 12, 59, 59),
                popUpIntervalInMins: 0,
                dateTimeNow: new DateTime(2017, 01, 01, 13, 0, 0)));
        }
    }
}

using CSharpTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WorkDaysCalculatorTests
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendBeforeStart()
        {
            DateTime startDate = new DateTime(2023, 4, 20);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2023, 4, 19), new DateTime(2023, 4, 19)),
                new WeekEnd(new DateTime(2023, 4, 24), new DateTime(2023, 4, 25))

            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2023, 4, 28)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Weekend end date must be after start date.")]
        public void TestWeekendEndDateInPastThrowsException()
        {
            DateTime startDate = new DateTime(2023, 4, 20);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2023, 4, 21), new DateTime(2023, 4, 19))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.Fail(result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Duration must be more than 0.")]
        public void TestDurationNullOrNegativeThrowsException()
        {
            DateTime startDate = new DateTime(2023, 4, 20);
            int count = -5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2023, 4, 21), new DateTime(2023, 4, 23))
            };
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.Fail(result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIncorrectDateTimeThrowsException()
        {
            DateTime startDate = new DateTime(2021, 13, 32);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.Fail(result.ToString());
        }

        [TestMethod]
        public void TestWeekendPeriodsDoNotIntersect()
        {
            DateTime startDate = new DateTime(2023, 4, 20);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2023, 4, 25), new DateTime(2023, 4, 27)),
                new WeekEnd(new DateTime(2023, 4, 21), new DateTime(2023, 4, 21))

            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2023, 5, 09)));
        }
    }
}

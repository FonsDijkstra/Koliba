using System;
using System.Collections.Generic;
using System.Linq;
using Koliba.Business;
using Xunit;

namespace Business.Tests
{
    public sealed class OpeningTimesTest
    {
        [Fact]
        public void Always_closed_has_no_dates()
        {
            var times = new OpeningTimes(new Dictionary<DayOfWeek, OpeningTime>());
            var dates = times.OpeningDates(DateTime.Now, 99);

            Assert.Empty(dates);
        }

        [Fact]
        public void Same_day_before_opening_time()
        {
            var times = new OpeningTimes(new Dictionary<DayOfWeek, OpeningTime> {
                { DayOfWeek.Wednesday, new OpeningTime { Open = TimeSpan.FromHours(8), Duration = TimeSpan.FromHours(8) } }
            });
            var date = times.OpeningDates(new DateTime(2011, 1, 19), 0).Single();

            Assert.Equal(date.Start.Date, new DateTime(2011, 1, 19));
            Assert.Equal(date.Start.TimeOfDay, TimeSpan.FromHours(8));
            Assert.Equal(date.Duration, TimeSpan.FromHours(8));
        }

        [Fact]
        public void Same_day_during_opening_time()
        {
            var times = new OpeningTimes(new Dictionary<DayOfWeek, OpeningTime> {
                { DayOfWeek.Wednesday, new OpeningTime { Open = TimeSpan.FromHours(8), Duration = TimeSpan.FromHours(8) } }
            });
            var date = times.OpeningDates(new DateTime(2011, 1, 19, 12, 0, 0), 0).Single();

            Assert.Equal(date.Start.Date, new DateTime(2011, 1, 19));
            Assert.Equal(date.Start.TimeOfDay, TimeSpan.FromHours(12));
            Assert.Equal(date.Duration, TimeSpan.FromHours(4));
        }

        [Fact]
        public void Same_day_after_opening_time()
        {
            var times = new OpeningTimes(new Dictionary<DayOfWeek, OpeningTime> {
                { DayOfWeek.Wednesday, new OpeningTime { Open = TimeSpan.FromHours(8), Duration = TimeSpan.FromHours(8) } }
            });
            var dates = times.OpeningDates(new DateTime(2011, 1, 19, 20, 0, 0), 0);

            Assert.Empty(dates);
        }

        [Fact]
        public void One_day_after_opening_time()
        {
            var times = new OpeningTimes(new Dictionary<DayOfWeek, OpeningTime> {
                { DayOfWeek.Wednesday, new OpeningTime { Open = TimeSpan.FromHours(8), Duration = TimeSpan.FromHours(8) } },
                { DayOfWeek.Thursday, new OpeningTime { Open = TimeSpan.FromHours(8), Duration = TimeSpan.FromHours(8) } }
            });
            var date = times.OpeningDates(new DateTime(2011, 1, 19, 20, 0, 0), 1).Single();

            Assert.Equal(date.Start.Date, new DateTime(2011, 1, 20));
            Assert.Equal(date.Start.TimeOfDay, TimeSpan.FromHours(8));
            Assert.Equal(date.Duration, TimeSpan.FromHours(8));
        }
    }
}

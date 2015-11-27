using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koliba.Apps.Models
{
    public class OpeningSchedule
    {
        static readonly IDictionary<DayOfWeek, OpeningTime> normalSchedule = new Dictionary<DayOfWeek, OpeningTime> {
            { DayOfWeek.Tuesday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(8) } },
            { DayOfWeek.Wednesday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(8) } },
            { DayOfWeek.Thursday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(8) } },
            { DayOfWeek.Friday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(10) } },
            { DayOfWeek.Saturday, new OpeningTime { Open = TimeSpan.FromHours(12), Duration = TimeSpan.FromHours(14) } },
            { DayOfWeek.Sunday, new OpeningTime { Open = TimeSpan.FromHours(12), Duration = TimeSpan.FromHours(10) } },
        };

        public IEnumerable<ReservationDate> ReservationDates(DateTime now, int nofDaysAhead)
        {
            var today = now.DayOfWeek;
            var schedule = normalSchedule[today];
            if (now.TimeOfDay < schedule.Open + schedule.Duration) {
                yield return new ReservationDate {
                    Start = now,
                    Duration = schedule.Open + schedule.Duration - now.TimeOfDay,
                };
            }

            for (int i = 1; i <= nofDaysAhead; ++i) {
                var day = NextDay(today);
                OpeningTime openingTime;
                if (normalSchedule.TryGetValue(day, out openingTime)) {
                    yield return new ReservationDate {
                        Start = now.Date.AddDays(i).Add(openingTime.Open),
                        Duration = openingTime.Duration,
                    };
                }
            }
        }

        static DayOfWeek NextDay(DayOfWeek day)
        {
            if (day == DayOfWeek.Monday) {
                return DayOfWeek.Tuesday;
            } else if (day == DayOfWeek.Tuesday) {
                return DayOfWeek.Wednesday;
            } else if (day == DayOfWeek.Wednesday) {
                return DayOfWeek.Thursday;
            } else if (day == DayOfWeek.Thursday) {
                return DayOfWeek.Friday;
            } else if (day == DayOfWeek.Friday) {
                return DayOfWeek.Saturday;
            } else if (day == DayOfWeek.Saturday) {
                return DayOfWeek.Sunday;
            } else if (day == DayOfWeek.Sunday) {
                return DayOfWeek.Monday;
            } else {
                throw new ArgumentOutOfRangeException(nameof(day));
            }
        }
    }
}
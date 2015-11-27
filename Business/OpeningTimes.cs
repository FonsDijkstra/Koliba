using System;
using System.Collections.Generic;

namespace Koliba.Business
{
    public struct OpeningTime
    {
        public TimeSpan Open;
        public TimeSpan Duration;
    }

    public struct ReservationDate
    {
        public string Name;
        public DateTime Start;
        public TimeSpan Duration;
    }

    public class OpeningTimes
    {
        public static readonly IDictionary<DayOfWeek, OpeningTime> SCHEDULE = new Dictionary<DayOfWeek, OpeningTime> {
            { DayOfWeek.Tuesday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(8) } },
            { DayOfWeek.Wednesday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(8) } },
            { DayOfWeek.Thursday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(8) } },
            { DayOfWeek.Friday, new OpeningTime { Open = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(10) } },
            { DayOfWeek.Saturday, new OpeningTime { Open = TimeSpan.FromHours(12), Duration = TimeSpan.FromHours(14) } },
            { DayOfWeek.Sunday, new OpeningTime { Open = TimeSpan.FromHours(12), Duration = TimeSpan.FromHours(10) } },
        };

        readonly IDictionary<DayOfWeek, OpeningTime> schedule;

        public OpeningTimes(IDictionary<DayOfWeek, OpeningTime> schedule)
        {
            this.schedule = schedule;
        }

        public IEnumerable<ReservationDate> ReservationDates(DateTime now, int nofDaysAhead)
        {
            var today = now.DayOfWeek;
            var openingTime = schedule[today];
            if (now.TimeOfDay < openingTime.Open + openingTime.Duration) {
                yield return new ReservationDate {
                    Start = now,
                    Duration = openingTime.Open + openingTime.Duration - now.TimeOfDay,
                };
            }

            for (int i = 1; i <= nofDaysAhead; ++i) {
                var day = NextDay(today);
                if (schedule.TryGetValue(day, out openingTime)) {
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

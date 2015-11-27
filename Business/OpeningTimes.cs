using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Koliba.Business
{
    public struct OpeningTime
    {
        public TimeSpan Open;
        public TimeSpan Duration;
    }

    public struct OpeningDate
    {
        public DateTime Start;
        public TimeSpan Duration;

        public string Name
        {
            get
            {
                var diff = Start.Date - DateTime.Today;
                if (diff.Days == 0) {
                    return Resources.Resources.TodayName;
                } else if (diff.Days == 1) {
                    return Resources.Resources.TomorrowName;
                } else if (diff.Days < 7) {
                    return Start.Date.ToString("dddd", CultureInfo.CurrentUICulture).ToStartSentence(CultureInfo.CurrentUICulture);
                } else {
                    return "ToDo";
                }
            }
        }
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

        public OpeningTimes(IDictionary<DayOfWeek, OpeningTime> schedule)
        {
            Schedule = new ReadOnlyDictionary<DayOfWeek, OpeningTime>(schedule);
        }

        public IReadOnlyDictionary<DayOfWeek, OpeningTime> Schedule { get; }

        public IEnumerable<OpeningDate> OpeningDates(DateTime from, int nofDaysAhead)
        {
            OpeningTime openingTime;

            var day = from.DayOfWeek;
            if (Schedule.TryGetValue(day, out openingTime)) {
                if (from.TimeOfDay < openingTime.Open) {
                    yield return new OpeningDate {
                        Start = from.Date.Add(openingTime.Open),
                        Duration = openingTime.Duration,
                    };
                } else if (from.TimeOfDay < openingTime.Open + openingTime.Duration) {
                    yield return new OpeningDate {
                        Start = from,
                        Duration = openingTime.Open + openingTime.Duration - from.TimeOfDay,
                    };
                }
            }

            for (int i = 1; i <= nofDaysAhead; ++i) {
                day = day.NextDay();
                if (Schedule.TryGetValue(day, out openingTime)) {
                    yield return new OpeningDate {
                        Start = from.Date.AddDays(i).Add(openingTime.Open),
                        Duration = openingTime.Duration,
                    };
                }
            }
        }
    }
}

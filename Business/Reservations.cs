using System;
using System.Collections.Generic;
using System.Linq;

namespace Koliba.Business
{
    public struct ReservationTime
    {
        public static readonly TimeSpan[] TYPICAL = new[] {
            new TimeSpan(17, 30, 0),
            new TimeSpan(18,  0, 0),
            new TimeSpan(18, 30, 0),
            new TimeSpan(19,  0, 0),
            new TimeSpan(19, 30, 0),
            new TimeSpan(20,  0, 0),
        };

        public TimeSpan Time;

        public string Name
        {
            get
            {
                if (Time.Hours == 17 && Time.Minutes == 30) {
                    return Resources.Resources.Time1730;
                } else if (Time.Hours == 18) {
                    if (Time.Minutes == 30) {
                        return Resources.Resources.Time1830;
                    } else {
                        return Resources.Resources.Time1800;
                    }
                } else if (Time.Hours == 19) {
                    if (Time.Minutes == 30) {
                        return Resources.Resources.Time1930;
                    } else {
                        return Resources.Resources.Time1900;
                    }
                } else if (Time.Hours == 20 && Time.Minutes == 0) {
                    return Resources.Resources.Time2000;
                } else {
                    return "ToDo";
                }
            }
        }
    }

    public sealed class Reservations
    {
        public IEnumerable<ReservationTime> ReservationTimes(DateTime from, OpeningDate date)
        {
            var min = (from < date.Start ? date.Start : from);
            var max = date.Start + date.Duration;
            if (min < max) {
                foreach (var typical in ReservationTime.TYPICAL.Select(typ => min.Date + typ)) {
                    if (min <= typical && typical <= max) {
                        yield return new ReservationTime { Time = typical.TimeOfDay, };
                    }
                }
            }
        }
    }
}

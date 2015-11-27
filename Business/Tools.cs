using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koliba.Business
{
    public static class Tools
    {
        public static DayOfWeek NextDay(this DayOfWeek day)
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

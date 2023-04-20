using System;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            int daysAdded = 0;
            DateTime currentDate = startDate;

            if (dayCount <= 0)

                throw new ArgumentOutOfRangeException("Duration must be more than 0.");

            // Adding days
            while (daysAdded < dayCount)
            {
                // Check if the current date is a weekend
                bool isWeekend = IsWeekend(currentDate, weekEnds);

                // If it's not a weekend, add the day
                if (!isWeekend)
                {
                    daysAdded++;
                }

                // Move to the next day
                currentDate = currentDate.AddDays(1);
            }

            //Get the last work day
            currentDate = currentDate.AddDays(-1);

            return currentDate;
        }
        private bool IsWeekend(DateTime date, WeekEnd[] weekends)
        {   

            foreach (var weekend in weekends ?? new WeekEnd[0])
            {
                if (date >= weekend.StartDate && date <= weekend.EndDate)
                {
                    return true;
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
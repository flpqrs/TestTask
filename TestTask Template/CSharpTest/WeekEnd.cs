using System;

namespace CSharpTest
{
    public class WeekEnd
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public WeekEnd(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("Weekend end date must be after start date");
            }

            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

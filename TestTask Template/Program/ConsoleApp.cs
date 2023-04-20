using CSharpTest;
using System.Globalization;

namespace Program
{
    public class ConsoleApp
    {
        public static void RunApp()
        {
            Console.WriteLine("Enter start date (dd/mm/yyyy): ");
            DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            Console.WriteLine("Enter duration in days: ");
            int duration = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of weekend periods: ");
            int numberOfWeekends = int.Parse(Console.ReadLine());

            WeekEnd[] weekends = new WeekEnd[numberOfWeekends];

            for (int i = 0; i < numberOfWeekends; i++)
            {
                Console.WriteLine($"Enter start date of weekend {i + 1} (dd/mm/yyyy): ");
                DateTime weekendStartDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine($"Enter end date of weekend {i + 1} (dd/mm/yyyy): ");
                DateTime weekendEndDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                weekends[i] = new WeekEnd(weekendStartDate, weekendEndDate);
            }
            WorkDayCalculator workDayCalculator = new WorkDayCalculator();
            DateTime endDate = workDayCalculator.Calculate(startDate, duration, weekends);

            Console.WriteLine($"End date after excluding weekends: {endDate:dd/MM/yyyy}");
        }
    }
}

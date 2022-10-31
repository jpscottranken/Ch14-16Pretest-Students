using System;

namespace Pretest
{
    public class HourlyEmployee : Employee
    {
        //  Constructor
        public HourlyEmployee(string firstName, string lastName, int empNum,
                              decimal hoursWorked, decimal hourlyRate)
                        : base (firstName, lastName, empNum)
        {
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
        }

        //  Getters and Setters
        decimal HoursWorked { get; set; }
        decimal HourlyRate { get; set; }

        //  Override the existing displayText() method
        public override string displayText()
        {
            return base.displayText() +
                "\r\nHours Worked: " + HoursWorked.ToString("n2") +
                "\r\nHourly Rate: "  + HourlyRate.ToString("c");
        }
    }
}

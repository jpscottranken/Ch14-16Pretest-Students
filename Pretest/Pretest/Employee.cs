using System;

namespace Pretest
{
    public class Employee
    {
        //  Constructor
        public Employee(string firstName, string lastName, int empNum)
        {
            FirstName = firstName;
            LastName  = lastName;
            EmpNum    = empNum;
        }

        //  Getters and Setters
        string FirstName { get; set; }
        string LastName { get; set; }

        int EmpNum { get; set; }

        public virtual string displayText()
        {
            return "Name: " + FirstName + " " + LastName +
                  "\r\nEmployee Number: " + EmpNum;
         }
    }
}

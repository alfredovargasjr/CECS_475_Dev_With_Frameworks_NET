using System;
using System.Collections.Generic;
using System.Text;

namespace CECS_475_Lab2
{
    public abstract class Employee : IPayable, IComparable<IPayable>
    {
        // read-only property that gets employee's first name
        public string FirstName { get; private set; }

        // read-only property that gets employee's last name
        public string LastName { get; private set; }

        // read-only property that gets employee's social security number
        public string SocialSecurityNumber { get; private set; }

        // three-parameter constructor
        public Employee(string first, string last, string ssn)
        {
            FirstName = first;
            LastName = last;
            SocialSecurityNumber = ssn;
        } // end three-parameter Employee constructor

        // return string representation of Employee object, using properties
        public override string ToString()
        {
            return string.Format("{0} {1}\nsocial security number: {2}",
               FirstName, LastName, SocialSecurityNumber + "\nPay Amount: " + this.GetPaymentAmount());
        } // end method ToString

        public abstract decimal GetPaymentAmount();


        int IComparable<IPayable>.CompareTo(IPayable other)
        {
            if (other == null)
            {
                return 1;
            }
            else
                return this.GetPaymentAmount().CompareTo(other.GetPaymentAmount());
        }

        /// <summary>
        ///     static function comparing two objects by SSN
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>
        ///     - return false: (1) if a follows b
        ///     - return false: (0) if a is same as b
        ///     - return true: (-1) if a preceedes b
        ///     - return false: (default) return false
        /// </returns>
        public static bool SSNIsGreater(object a, object b)
        {
            Employee e1 = (Employee)a;
            Employee e2 = (Employee)b;
            switch (e1.SocialSecurityNumber.CompareTo(e2.SocialSecurityNumber))
            {
                case 1:
                    return false;
                case 0:
                    return false;
                case -1:
                    return true;
                default:
                    return false;
            }
        }

    } // end abstract class Employee
}

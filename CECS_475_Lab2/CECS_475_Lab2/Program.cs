using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CECS_475_Lab2
{
    /// <summary>
    ///     Sorting the list of IPayable objects using a selection sort
    /// </summary>
    class SelectionSortClass
    {
        /// <summary>
        ///     Sort method
        ///         Compare two employee objects then swap items for sort
        /// </summary>
        /// <param name="employees">
        ///     List of employees
        /// </param>
        /// <param name="gtMethod">
        ///     function used to compare objects is passed in as a delegate param
        /// </param>
        static public void Sort(List<IPayable> employees, Program.CompareDelegate gtMethod)
        {
            var smallest = 0;
            for (int i = 0; i < employees.Count - 1; i++)
            {
                smallest = i;
                for (int j = i + 1; j < employees.Count; j++)
                {
                    Employee a = (Employee)employees[j];
                    Employee b = (Employee)employees[smallest];
                    if (gtMethod(b, a))
                    {
                        swap(employees, j, smallest);
                    }
                }
            }
        }

        /// <summary>
        ///     Swap two items of a list
        /// </summary>
        /// <param name="list">
        ///     List of employees
        /// </param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        static void swap(List<IPayable> list, int a, int b)
        {
            var t = list[a];
            list[a] = list[b];
            list[b] = t;
        }
    }

    class Program
    {
        /// <summary>
        ///     delegate function, used for sorting list
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public delegate bool CompareDelegate(object a, object b);

        /// <summary>
        ///     print the items of the list
        /// </summary>
        /// <param name="employees"></param>
        static void printEmployees(List<IPayable> employees)
        {
            foreach (IPayable employee in employees)
            {
                Console.WriteLine(employee.ToString());
                Console.WriteLine();
            }
        }

        /// <summary>
        ///     user options menu
        /// </summary>
        /// <returns> user's menu choice </returns>
        static int menu()
        {
            bool validEntry = false;
            int choice = 0;
            while (!validEntry)
            {
                Console.WriteLine(
                    "Lab Assignment 2\nMenu:" +
                    "\n[1] Sort by Last Name (Desc)" +
                    "\n[2] Sort by Pay Amount (Asc)" +
                    "\n[3] Sort by Social Security (Desc)" +
                    "\n[4] Sort by Last Name (Asc), Pay Amount (Desc) - LINQ" +
                    "\n[0] Exit");
                Console.Write("\nChoice: ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    validEntry = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInput is not a string value.");
                    Console.WriteLine();
                    continue;
                }
            }
            return choice;
        }

        static void Main(string[] args)
        {
            List<IPayable> employeeList = new List<IPayable>();
            employeeList.Add(new SalariedEmployee("John", "Smith", "111-11-1111", 700M));
            employeeList.Add(new SalariedEmployee("Antonio", "Smith", "555-55-5555", 800M));
            employeeList.Add(new SalariedEmployee("Victor", "Smith", "444-44-4444", 600M));
            employeeList.Add(new HourlyEmployee("Karen", "Price", "222-22-2222", 16.75M, 40M));
            employeeList.Add(new HourlyEmployee("Ruben", "Zamora", "666-66-6666", 20.00M, 40M));
            employeeList.Add(new CommissionEmployee("Sue", "Jones", "333-33-3333", 10000M, .06M));
            employeeList.Add(new BasePlusCommissionEmployee("Bob", "Lewis", "777-77-7777", 5000M, .04M, 300M));
            employeeList.Add(new BasePlusCommissionEmployee("Lee", "Duarte", "888-88-888", 5000M, .04M, 300M));
            bool exit_program = false;
            while (!exit_program)
            {
                switch (menu())
                {
                    case 1:
                        // Using IComparable Interface
                        // sort using a delegate that references the inline function
                        employeeList.Sort(delegate (IPayable x, IPayable y)
                        {
                            Employee a = (Employee)x;
                            Employee b = (Employee)y;
                            if (a.LastName == null && b.LastName == null) return 0;
                            else if (a.LastName == null) return 1;
                            else if (b.LastName == null) return 0;
                            else return a.LastName.CompareTo(b.LastName);
                        }
                        );
                        printEmployees(employeeList);
                        break;
                    case 2:
                        // Using IComparer interface
                        // using a comparer class, sort the list in ascending order
                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        Employee_SortByPayAmount_AscendingOrder eAsc = new Employee_SortByPayAmount_AscendingOrder();
                        employeeList.Sort(eAsc);
                        watch.Stop();
                        printEmployees(employeeList);
                        Console.WriteLine("\nRun Time: " + watch.ElapsedMilliseconds + "ms\n");
                        break;
                    case 3:
                        // Using selection sort and delegate
                        // Using a delegate object, it will be referencing the function wanted for the sorting algo,
                        //      it is then sent to the selectionSort class to be used.
                        //      - it will sort the list in descending order by employees SSN
                        CompareDelegate EmployeeCompare = new CompareDelegate(Employee.SSNIsGreater);
                        SelectionSortClass.Sort(employeeList, EmployeeCompare);
                        printEmployees(employeeList);
                        break;
                    case 4:
                        // Using LINQ sorting
                        // it will sort the list ascending based on last name, then descending by payment amount
                        var OrderBy = from employee in employeeList
                                      orderby ((Employee)employee).LastName, employee.GetPaymentAmount() descending
                                      select employee;
                        foreach (var employee in OrderBy)
                        {
                            Console.WriteLine(employee);
                            Console.WriteLine();
                        }
                        break;
                    case 0:
                        exit_program = true;
                        break;
                    default:
                        Console.WriteLine("\nNot a valid menu option.\n");
                        break;
                }
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        } // end Main
    }
}

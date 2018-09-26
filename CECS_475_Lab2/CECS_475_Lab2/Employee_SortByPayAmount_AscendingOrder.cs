using System;
using System.Collections.Generic;
using System.Text;

namespace CECS_475_Lab2
{
    /// <summary>
    ///     Sorting class extending IComparer interface
    /// </summary>
    class Employee_SortByPayAmount_AscendingOrder : IComparer<IPayable>
    {
        /// <summary>
        ///     inherited method compare
        ///         - compare two IPayable objects by using
        ///           GetPaymentAmount()
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        ///     result of comparison
        ///         - (-1) if x preceedes y
        ///         - (0) if x is same as y
        ///         - (1) if x follows y
        /// </returns>
        int IComparer<IPayable>.Compare(IPayable x, IPayable y)
        {
            if (x == null && y == null) return 0;
            else if (x == null) return 0;
            else if (y == null) return 1;
            else if (x.GetPaymentAmount() > y.GetPaymentAmount()) return 1;
            else if (x.GetPaymentAmount() < y.GetPaymentAmount()) return -1;
            else return 0;
        }
    }
}

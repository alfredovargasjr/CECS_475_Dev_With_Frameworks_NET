using System;
using System.Collections.Generic;
using System.Text;

namespace CECS_475_Lab2
{
    /// <summary>
    ///     IPayable interface
    ///         - extends IComparable interface
    /// </summary>
    interface IPayable : IComparable<IPayable>
    {
        decimal GetPaymentAmount(); // calculate payment
    }
}

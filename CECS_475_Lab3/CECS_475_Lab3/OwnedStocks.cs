using System;
using System.Collections.Generic;
using System.Text;

namespace CECS_475_Lab3
{
    class OwnedStocks
    {
        public int quantity { get; private set; }
        public Stock StockOwned { get; private set; }

        public OwnedStocks (Stock s, int purchased)
        {
            this.StockOwned = s;
            this.quantity = purchased;
        }

        public override string ToString()
        {
            return this.StockOwned.ToString() + "\n\tAmount Owned: " + this.quantity;
        }
    }
}

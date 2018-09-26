using System;
using System.Collections.Generic;
using System.Text;

namespace CECS_475_Lab3
{
    class StockCustomer
    {
        public string name { get; private set; }
        public List<OwnedStocks> stocks { get; private set; }

        public StockCustomer(string name, List<OwnedStocks> stocks)
        {
            this.name = name;
            this.stocks = stocks;
        }

        public StockCustomer(string name)
        {
            this.name = name;
            this.stocks = new List<OwnedStocks>();
        }

        public void AddStock(Stock stock, int quantity)
        {
            if (stock != null)
            {
                this.stocks.Add(new OwnedStocks(stock, quantity));
                stock.stockEvent += NotifyCustomer;
            }
        }

        public string printStocks()
        {
            string listOfStocks = "";
            if (this.stocks.Count < 1)
                return "\nNo stocks owned.";
            foreach (var stock in this.stocks)
            {
                listOfStocks = listOfStocks + "\t" + stock.ToString() + "\n";
            }
            return string.Format("\t{0}", listOfStocks);
        }

        public override string ToString()
        {
            return string.Format("\nCustomer Name: {0}" +
                                 "\nStocks Owned...\n_________\n {1}\n_________",  this.name, this.printStocks()).PadRight(10);
        }

        public string printStockSold(OwnedStocks stock)
        {
            if (stock != null)
                return string.Format("\nStock {0} threshold reached!\n{1}\n\tProfit: {2}", 
                    stock.StockOwned.name, 
                    stock.StockOwned.ToString(), 
                    (stock.StockOwned.currentValue - stock.StockOwned.initial_value) * stock.quantity).PadRight(10);
            return null;
        }

        public void NotifyCustomer(Object sender, EventData e)
        {
            Stock stock = (Stock)sender;
            OwnedStocks sold = null;
            foreach (var owned in this.stocks)
            {
                if (owned.StockOwned.name == e.stockName)
                {
                    sold = owned;
                    this.stocks.Remove(owned);
                    break;
                }
                
            }
            if (sold != null)
            {
                Console.WriteLine(string.Format("\nNotification\nStock Sold!\nCustomer: {0}", this.name));
                Console.WriteLine(printStockSold(sold));
            }
        }
    }
}

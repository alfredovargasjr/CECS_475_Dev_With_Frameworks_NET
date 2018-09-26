using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CECS_475_Lab3
{
    class Program
    {
        public static int stockThreads { get; private set; }
        public static string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string fileName = "Stocks_Sold.txt";

        static void Main(string[] args)
        {
            Thread th = Thread.CurrentThread;
            // Write the text to a new file named "WriteFile.txt".
            File.WriteAllText(Path.Combine(FolderPath, fileName), "Stock report..." + Environment.NewLine + Environment.NewLine);
            List<Stock> market = new List<Stock>();
            market.Add(new Stock("Vartech", 329.87, 20, 0.21, 100));
            market.Add(new Stock("NetTech", 420.00, 10, 0.10, 100));
            market.Add(new Stock("BigMoney", 539, 18, 0.30, 200));
            foreach (var stock in market)
            {
                stockThreads++;
                stock.stockEvent += c_StockNotifcation;
                stock.stockRecordEvent += c_StockRecord;
                Console.WriteLine(stock.ToString());
            }
            StockCustomer customer1 = new StockCustomer("Alfredo Vargas");
            customer1.AddStock(market[0], 9);
            customer1.AddStock(market[1], 13);
            StockCustomer customer2 = new StockCustomer("Joe Willis");
            customer2.AddStock(market[0], 20);
            StockCustomer customer3 = new StockCustomer("Bill Nine");
            customer3.AddStock(market[1], 18);
            customer3.AddStock(market[2], 20);
            StockCustomer customer4 = new StockCustomer("Hamil Ton");
            customer4.AddStock(market[1], 22);
            StockCustomer customer5 = new StockCustomer("Alex Zander");
            customer5.AddStock(market[1], 5);
            customer5.AddStock(market[0], 14);


            Console.WriteLine(customer1.ToString());
            Console.WriteLine(customer2.ToString());
            Console.WriteLine(customer3.ToString());
            Console.WriteLine(customer4.ToString());
            Console.WriteLine(customer5.ToString());
            Console.WriteLine("\n\nThe Market has opened...");
            while (stockThreads > 0)
            {
                Thread.Sleep(2000);
            }
            Console.WriteLine("\n\nThe Market has closed...");
            Console.WriteLine(customer1.ToString());
            Console.WriteLine(customer2.ToString());
            Console.WriteLine(customer3.ToString());
            Console.WriteLine(customer4.ToString());
            Console.WriteLine(customer5.ToString());
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        // event listener for the Customer Stock Notifcation
        static void c_StockNotifcation(Object sender, EventData e)
        {
            // on a notification event, the thread will stop its process
            //      - stockThreads hold a global variable on the number of threads 
            //        currently running 
            stockThreads--;
            Console.WriteLine("\nThreshhold was passed");
            Stock s = (Stock)sender;
            Console.WriteLine(s.ToString());
        }

        // event listener for the Stock Record event
        //      - the stock solds will be recorded/written onto
        //        a text file
        static void c_StockRecord(Object sender, EventArgs e)
        {
            Stock stock = (Stock)sender;
            if (stock != null)
            {
                string s = string.Format("\nTime: {0}\r{1}\r\nNumber of changes: {2}\r\nEarnings: {3}",
                    DateTime.Now.ToString("h:mm:ss tt"),
                    stock.ToString(),
                    stock.numberChanges,
                    (stock.currentValue - stock.initial_value) * stock.quantity) + Environment.NewLine + Environment.NewLine;
                WriteText(s);
            }
        }

        static void WriteText(string text)
        {
            // Append new lines of text to the file
            File.AppendAllText(Path.Combine(FolderPath, fileName), text);
        }
    }
}

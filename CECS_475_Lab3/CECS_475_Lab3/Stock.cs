using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CECS_475_Lab3
{
    class Stock
    {

        // event declaration
        //      - event to print notification to customer when stock threshold is passed
        //      - event of type <EventData>, event with data members
        public event EventHandler<EventData> stockEvent;
        // event raiser
        //      - on function call, raise event EventData to be heard by 
        //        event listeners
        protected virtual void OnStockEvent(EventData e)
        {
            EventHandler<EventData> handler = stockEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // event declaration
        //      - event to write stock information to a text file
        //        when the stock threshold is reached
        public event EventHandler stockRecordEvent;
        // event raiser
        //      - on function call, raise event stockRecordEvent to be heard
        //        by event listeners
        protected virtual void OnStockRecordEvent(EventArgs e)
        {
            EventHandler handler = this.stockRecordEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // object for mutex lock, allow multi thread synchro
        private System.Object lockThis = new System.Object();

        public string name { get; private set; }
        public int quantity { get; private set; }
        public double initial_value { get; private set; }
        public double currentValue { get; private set; }
        public double maximum_change { get; private set; }
        public double notification_threshold { get; private set; }
        public int numberChanges { get; private set; }

        public Stock(string name, double initial_value, int quantity, 
                     double maximum_change, double notification_threshold)
        {
            this.name = name;
            this.initial_value = initial_value;
            currentValue = initial_value;
            this.quantity = quantity;
            this.maximum_change = maximum_change;
            this.notification_threshold = notification_threshold;
            ThreadStart childThread = new ThreadStart(Activate);
            Thread stockThread = new Thread(childThread);
            stockThread.Start();
        }

        public void Activate()
        {
            // stock current value will continue to change until threshhold is passed
            while (Math.Abs(this.currentValue - this.initial_value) < this.notification_threshold)
            {
                Thread.Sleep(500);
                this.ChangeStockValue();
            }
            
        }

        public void ChangeStockValue()
        {
            ///random changes from 1 - max change
            Random r = new Random();
            var sign = 1;
            do
            {
                sign = r.Next(-1, 2);
            } while (sign == 0);
            // current value of stock = current value +- stock price change 
            //  - stock price change = random percentage (0 - maxChange) * current value
            this.currentValue = (currentValue + (sign * r.NextDouble() * this.maximum_change * this.currentValue));
            this.numberChanges++;
            if (Math.Abs(this.currentValue - this.initial_value) > this.notification_threshold)
            {
                // intialize the EventData event to set data members
                EventData args = new EventData();
                // set data members of the event
                args.currentValue = this.currentValue;
                args.quantity = this.quantity;
                args.stockName = this.name;
                args.initialValue = this.initial_value;
                // mutex lock for process synchronization
                //      - lockThis object defines this instance 
                lock (this.lockThis)
                {
                    // raise event, StockEvent 
                    this.OnStockEvent(args);
                    // raise event, stockRecordEvent
                    this.OnStockRecordEvent(EventArgs.Empty);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("\n\tStock Name: {0}" +
                                 "\n\tInitial Value: {1}" +
                                 "\n\tCurrent Value: {2}", this.name, this.initial_value, this.currentValue).PadRight(10);
        }
    }

    // 
    public class EventData : EventArgs
    {
        public string stockName { get; set; }
        public double currentValue { get; set; }
        public double initialValue { get; set; }
        public int quantity { get; set;  }
    }
}

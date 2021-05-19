using System;

namespace EventSample
{
    class Program
    {
        static void Main(string[] args)
        {
            EventPublisher eventPublisher = new();
            EventSubscriber eventSubscriber = new();
            eventPublisher.OnBalanceExceeds250 += eventSubscriber.HandleOnBalanceExceeds250;
            eventPublisher.CheckBalance(240); 
            eventPublisher.CheckBalance(251); // this method call will raise OnBalanceExceeds250 event.
        }
    }

    public class EventPublisher
    {
        private delegate void Del(string str);
        public event Del OnBalanceExceeds250;
        public void CheckBalance(int balance)
        {
            Console.WriteLine($"Entered balance:{balance}");
            if (balance > 250)
            {
                if (OnBalanceExceeds250 is not null)
                {
                    OnBalanceExceeds250("ATTENTION! The current balance exceeds 250 $"); // this string will pass to HandleOnBalanceExceeds250 method.
                }
            }
        }
    }

    public class EventSubscriber
    {
        public void HandleOnBalanceExceeds250(string str)
        {
            Console.WriteLine(str);
        }
    }
}

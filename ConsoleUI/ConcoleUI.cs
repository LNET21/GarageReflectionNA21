using System;


namespace ConsoleUI
{
    public class ConsoleUI : IUI
    {
        public void Clear()
        {
            Console.Clear();
        }

        public string ParkMeny()
        {
           return "ParkMeny";
           
        }

        public void ParkMeny(bool isFull, string options)
        {
            Console.WriteLine(isFull ?  "No spots left" : ParkMeny() + "\n" + options);
        }

      

        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowMeny()
        {
            Console.WriteLine("Options here...");
        }
    }
}

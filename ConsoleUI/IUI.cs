using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public interface IUI
    {
        void ShowMeny();
        void Print(string message);
        string ParkMeny();
        void Clear();
        void ParkMeny(bool isFull, string options);
    }
}

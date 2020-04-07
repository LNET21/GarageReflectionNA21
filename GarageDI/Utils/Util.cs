using System;
using System.Collections.Generic;
using System.Text;

namespace GarageDI.Utils
{
    public static class Util
    {
        internal static string AskForString(string prompt)
        {
            bool correct = true;
            string answer;

            do 
            {
                Console.WriteLine(prompt);
                answer = Console.ReadLine();

                if (!string.IsNullOrEmpty(answer))
                {
                    correct = false;
                }

            } while (correct);

            return answer; 
        }

        internal static int AskForInt(string prompt)
        {
            bool success;
            uint answer; 
            do 
            {
                string input = AskForString(prompt);

                success = uint.TryParse(input, out answer);

                if (!success)
                {
                    Console.WriteLine("Wrong format only numbers. Negative numbers not allowed");
                }

            } while (!success);

            return (int)answer;
        }

        internal static int AskForKey(string prompt)
        {
            bool success;
            int keyPressed;

            do
            {
                Console.Write(prompt);
                var input = Console.ReadKey(intercept: true).KeyChar.ToString();
                success = int.TryParse(input, out keyPressed);
                if (!string.IsNullOrEmpty(input) && success)
                success = true;
              
            } while (!success);

            return keyPressed;
        }
    }
}

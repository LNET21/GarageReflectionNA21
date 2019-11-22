using System;
using System.Collections.Generic;
using System.Text;

namespace GarageDI.Utils
{
    //Static class cant make instances of. Exactly as the Console class for example
    public static class Util
    {
        //internal visible in the same assembly
        //must be static in a static class
        //string return type
        //AskForString method name
        internal static string AskForString(string prompt)
        {
            bool correct = true;
            string answer; //Scope

            do //Repeat
            {
                Console.WriteLine(prompt);
                answer = Console.ReadLine();

                //If answer is not null or empty string
                if (!string.IsNullOrEmpty(answer))
                {
                    //Set bool correct to false to exit loop
                    correct = false;
                }

            } while (correct); //until we have get a correct value

            return answer; //return value
        }

        internal static int AskForInt(string prompt)
        {
            bool success;
            int answer; //Scope

            do //Repeat
            {
                string input = AskForString(prompt);

                //Try to parse string to int returns bool
                //If true exit loop
                success = int.TryParse(input, out answer);

                //If we not successfully parsed string input to int answer
                if (!success)
                {
                    //Write errormessage
                    Console.WriteLine("Wrong format only numbers");
                }

            } while (!success);

            //Returns parsed string
            return answer;
        }

        internal static int AskForKey(string prompt)
        {

            bool success;
            int keyPressed;

            do
            {
                Console.WriteLine(prompt);
                var input = Console.ReadKey(intercept: true).KeyChar.ToString();
                success = int.TryParse(input, out keyPressed);
                if (!string.IsNullOrEmpty(input) && success)
                success = true;
              
            } while (!success);

            return keyPressed;
        }
    }
}

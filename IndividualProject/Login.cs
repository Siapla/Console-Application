using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class Login
    {
        public static string Screen()
        {
            string input;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Log in");
                Console.WriteLine("2.Exit");
                input = Console.ReadLine();
            } while (input != "1" && input != "2" );
            return input;      
        }

        
        public static void ValidationFailed()
        {
            Console.WriteLine("\n\nWrong username or password.\nPress any key to go to log in page.");
            Console.ReadKey();
            MainApp.Application();
        }
    }
    
}

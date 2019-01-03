using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class Menu
    {

        public static void User()
        {
            Console.Clear();
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.My messages");
            Console.WriteLine("2.All messages");
            Console.WriteLine("3.Send message");
            Console.WriteLine("4.Conversation");
        }

        public static void PowerUser()
        {
            Menu.User();
            Console.WriteLine("5.Edit Message");
        }

        public static void Admin()
        {
            Menu.PowerUser();
            Console.WriteLine("6.Delete Message");
        }

        public static void SuperAdmin()
        {

            Console.WriteLine("7.Create user");
            Console.WriteLine("8.View users");
            Console.WriteLine("9.Delete user");
            Console.WriteLine("10.Update password");
            Console.WriteLine("11.Update role");

        }

        public static void InvalidInput()
        {
            Console.WriteLine("Invalid input, press any key to go to main menu");
        }

       



    }


}

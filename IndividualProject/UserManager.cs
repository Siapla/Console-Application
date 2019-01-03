using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class UserManager
    {
        public static string UserName()
        {
            Console.Clear();
            Console.Write("Username: ");
            string username = Console.ReadLine().Trim();
            return username;
        }
        public static void Print(List<User> users)
        {
            Console.Clear();
            foreach (var item in users)
            {
                Console.WriteLine($"Username: {item.UserName} Role: {item.Role}");
            }
            Console.WriteLine("\nPress any key to go to main menu");
            Console.ReadKey();
        }

        public static string GetPassword(string pass)
        {
            Console.Write($"{pass}: ");
            var password = "";
            var ch = Console.ReadKey(true).KeyChar;
            while (ch > ' ' || ch == (char)8)
            {
                if (ch == (char)8)
                {
                    string ast = "";
                    if (password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        for (int i = 0; i < password.Length; i++)
                        {
                            ast += "*";
                        }
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new String(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write($"{pass}: {ast}");
                    }

                    ch = Console.ReadKey(true).KeyChar;
                }
                else
                {
                    password += ch;
                    Console.Write('*');
                    ch = Console.ReadKey(true).KeyChar;
                }

            }
            while (password.Length < 4)
            {
                Console.Clear();
                Console.Write("Password must be at least 4 characters\n");
                password = UserManager.GetPassword(pass);
            }
            return password;
        }

        public static User Create()
        {
            int i = 0;
            string input;
            int role = 0;
            User newUser = null;
            string username = UserManager.UserName();
            while (username.Length < 3)
            {
                Console.Clear();
                Console.Write("Username must be at least 3 characters\nUsername:");
                username = Console.ReadLine().Trim();
            }
            var db = new DatabaseAccess();
            if (db.UserExists(username))
            {
                Console.WriteLine("User Exists, press any key to go to main menu");
                Console.ReadKey();
            }
            else
            {
                string password = UserManager.GetPassword("Password");                
                var salt = Password.GetSalt();
                var hash = Password.Hash(password, salt);
                password = Convert.ToBase64String(hash);
                string Salt = Convert.ToBase64String(salt);
                Console.WriteLine("\n\nGive me the user Role\n1.User\n2.Poweruser\n3.Admin");
                do
                {
                    if (i > 0)
                    {
                        Console.WriteLine("Invalid input, press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("Give me the user Role\n1.User\n2.Poweruser\n3.Admin");
                    }
                    input = Console.ReadLine();
                    i++;
                } while (!int.TryParse(input, out role) || (role < 0 || role > 3));
                newUser = new User(username, password, (Role)role, Salt);
            }
            return newUser;
        }

        public static string Delete()
        {
            Console.Clear();
            Console.Write($"Give me the username of the user you want to delete: ");
            return UserManager.UsernameValidation();
         }

        public static User Update(bool updatePass)
        {
            string input;
            int i = 0;
            int role;
            string action = null;
            action = updatePass ? "update password" : "update Role";
            Console.Clear();
            Console.Write($"Give me the username of the user you want to {action}: ");
            var UserToUpdate = UserManager.UsernameValidation();
            if (string.IsNullOrEmpty(UserToUpdate))
                return null;
            if (updatePass)
            {
                var password = UserManager.GetPassword("New password");
                return new User { Password = password, UserName = UserToUpdate };
            }
            else
            {
                Console.WriteLine("New Role\n1.User\n2.Poweruser\n3.Admin");
                do
                {
                    if (i > 0)
                    {                       
                        Console.WriteLine("Invalid input, press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine($"New Role for {UserToUpdate}\n1.User\n2.Poweruser\n3.Admin");
                    }
                    input = Console.ReadLine();
                    i++;
                } while (!int.TryParse(input, out role) || (role < 0 || role > 3));
                var role1 = (Role)role;
                return new User { UserName = UserToUpdate, Role = role1 };
            }
        }

        public static string ConversationWith()
        {
            Console.Clear();
            Console.Write("Conversation with: ");
            return UserManager.UsernameValidation();
        }

        public static string UsernameValidation()
        {
            string username = Console.ReadLine();
            var db = new DatabaseAccess();
            
            if (!db.UserExists(username))
            {

                Console.Clear();
                Console.WriteLine($"There is no user with username: {username}, press any key to go to main menu");
                Console.ReadKey();
                return null;

            }
            return username;
        }

    }


}

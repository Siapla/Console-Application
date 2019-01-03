using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class MsgManager
    {
        public static void Print(List<Message> msg)
        {
            Console.Clear();
            if (msg.Count != 0)
            {
                foreach (var item in msg)
                {
                    Console.WriteLine($"Id: {item.Id}\nSender:{item.Sender.UserName}\nReceiver:{item.Receiver.UserName}\n{item.DateTime}\n\n{item.MessageData}\n\n\n");
                }
                Console.WriteLine("Press any key to go to main menu");
            }
            else
            {
                Console.WriteLine("There is no messages, press any key to go to main menu");
            }
            Console.ReadKey();
        }

        public static Message NewMsg(User logedinUser)
        {
            Message message = null;
            Console.Clear();
            Console.Write("Receiver: ");
            string receiverUsername = Console.ReadLine();
            var db = new DatabaseAccess();
            bool checkReceiver = db.UserExists(receiverUsername);
            if (!checkReceiver || receiverUsername == logedinUser.UserName)
            {
                Console.Clear();
                string warning = !checkReceiver ? $"There is no user with username: {receiverUsername}, press any key to go to main menu"
                                                     : "You are not allowed to message yourself,press any key to go to main menu";
                Console.WriteLine(warning);
                Console.ReadKey();
            }
            else
            {
                Console.Write("Message: ");
                string data = MsgManager.CheckLength();
                var receiver = new User { UserName = receiverUsername };
                message = new Message(data, logedinUser, receiver);

            }
            return message;
        }

        public static Message EditOrDelete(bool edit)
        {
            int i = 0;
            string id;
            int msgId;
            string msg = edit == true ? "edit" : "delete";
            Console.Clear();
            Console.Write($"Message id that you want to {msg}: ");
            do
            {
                if (i > 0)
                {
                    Console.Clear();
                    Console.Write($"Invalid input\nMessage id that you want to {msg}: ");
                }
                id = Console.ReadLine();
                i++;
            } while (!int.TryParse(id, out msgId));
            var db = new DatabaseAccess();
            var message = db.MessageExistance(msgId);
            if (message == null)
            {
                Console.WriteLine($"There is no message with id {id}, press any key to go to main menu");
                Console.ReadKey();
            }
            else
            {
                if (edit)
                {
                    Console.WriteLine($"\nMessage: {message.MessageData}");
                    Console.WriteLine("\nEdit message: ");
                    message.MessageData = MsgManager.CheckLength();
                }
            }
            return message;
        }

        public static bool Unread(int unreadCount)
        {
            string message = unreadCount == 1 ? $"You have {unreadCount} new message" : $"You have {unreadCount} new messages\n";
            Console.WriteLine(message);
            Console.Write("\nPress 1 to see your unread messages or any key to go to main menu: ");
            string choice = Console.ReadLine();
            return choice == "1";
        }

        public static string CheckLength()
        {
            string message = Console.ReadLine();
            while (message.Length > 250)
            {
                Console.Write("There is a limit of 250 characters per message, please type your message again\nMessage: ");
                message = Console.ReadLine();
            }
            return message;
        }
    }
}

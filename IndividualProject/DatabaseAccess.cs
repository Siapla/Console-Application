using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IndividualProject
{
    public class DatabaseAccess
    {
        public List<Message> UnReadMessages(string username)
        {
            using (var context = new AppContext())
            {
                var logedin = context.Users.Find(username);
                return context.Messages.Include("Sender").Where(x => x.Receiver.UserName == logedin.UserName && x.ReadState == false).ToList(); 
            }
        }

        public void ChangeReadState(string username)
        {
            using (var context = new AppContext())
            {
                var logedin = context.Users.Where(user => user.UserName == username).FirstOrDefault();
                var unreadMessages = context.Messages.Include("Sender").Where(x => x.Receiver.UserName == logedin.UserName && x.ReadState == false).ToList();
                foreach (var item in unreadMessages)
                {
                    item.ReadState = true;
                }
                context.SaveChanges();
            }
            
        }
        public User UserValidation(string username, string password)
        {

            using (var context = new AppContext())
            {
                var LogedInUser = context.Users.Find(username);
                if (LogedInUser!=null)
                {
                    var salt = LogedInUser.Salt;
                    var hash = Password.Hash(password, Convert.FromBase64String(salt));
                    password = Convert.ToBase64String(hash);
                    if (password == LogedInUser.Password)
                        return LogedInUser;
                    LogedInUser = null;
                }
                return LogedInUser;
            }

        }

        public void AddUser(User user)
        {
            using (var context = new AppContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public List<User> AllUsers()
        {
            using (var context = new AppContext())
            {
                return context.Users.ToList();           
            }

        }
        public void RemoveUser(string username)
        {
            using (var context = new AppContext())
            {
                var messages = context.Messages.Where(i => i.Sender.UserName == username || i.Receiver.UserName == username);
                
                foreach (var item in messages)
                {
                    context.Messages.Remove(item);
                }
                context.SaveChanges();
                var user = context.Users.Find(username);
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
 

        public List<Message> AllMessages()
        {
            using (var context = new AppContext())
            {
                return context.Messages.Include("Sender").Include("Receiver").ToList();
            }

        }

        public List<Message> MyMessages(User user)
        {
            using (var context = new AppContext())
            {
                return context.Messages.Include("Sender").Include("Receiver").Where(i => i.Receiver.UserName == user.UserName).ToList();

            }

        }

        public bool UserExists(string username)
        {
            using (var context = new AppContext())
            {
                var user = context.Users.Find(username);
                if (user != null)
                    return true;
                return false;
            }
        }

        public void SendMessage(Message message)
        {
            using (var context = new AppContext())
            {
               User sender = context.Users.Where(user => message.Sender.UserName == user.UserName ).First();
               User receiver = context.Users.Where(user => message.Receiver.UserName == user.UserName).First();
                message.Receiver = receiver;
                message.Sender = sender;
                context.Messages.Add(message);            
                context.SaveChanges();               
            }
        }

        public Message MessageExistance(int msgId)
        {
            using (var context = new AppContext())
            {
                return context.Messages.Find(msgId);
            }

        }

        public void EditMessage(Message message)
        {
            using (var context = new AppContext())
            {
                context.Entry(message).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteMessage(Message message)
        {
            using (var context = new AppContext())
            {
                context.Entry(message).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


        public void UpdateRole(string username,Role role)
        {
            using (var context = new AppContext())
            {
                var updatedUser = context.Users.Find(username);
                updatedUser.Role = role;
                context.SaveChanges();
            }
        }

        public void UpdatePassword(string username, string password)
        {
            using (var context = new AppContext())
            {
                var updatedUser = context.Users.Find(username);
                var salt = Password.GetSalt();
                var hash = Password.Hash(password, salt);
                var stringSalt= Convert.ToBase64String(salt);
                password = Convert.ToBase64String(hash);
                updatedUser.Salt = stringSalt;
                updatedUser.Password = password;
                context.SaveChanges();
            }
        }


        public List<Message> Conversation(string logedinusername, string username)
        {
            using (var context = new AppContext())
            {
                User a = context.Users.Find(logedinusername);
                User b = context.Users.Find(username);
                var conversation = context.Messages.Where(x => (x.Sender.UserName == a.UserName && x.Receiver.UserName == b.UserName) ||
                (x.Sender.UserName == b.UserName && x.Receiver.UserName == a.UserName)).OrderBy(x => x.DateTime).ToList();

                var falseReadState = conversation.Where(x => x.Receiver.UserName==a.UserName && x.ReadState == false).ToList();

                if (falseReadState.Count !=0 )
                {
                    foreach (var item in falseReadState)
                    {
                        item.ReadState = true;                        
                    }
                    context.SaveChanges();
                }
                return conversation;
            }

        }
    }
}

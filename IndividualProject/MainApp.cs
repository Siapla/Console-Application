using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class MainApp
    {
        public static void Application()
        {
            int exit = -1;
            string inputCheck;
            int max = 0;
            User logedInUser = null;

            string input = Login.Screen();
            switch (input)
            {
                case "1":
                    string username = UserManager.UserName();
                    string password = UserManager.GetPassword("Password");

                    var db = new DatabaseAccess();                    
                    logedInUser = db.UserValidation(username, password);               
                    if (logedInUser == null)
                    {
                        Login.ValidationFailed();
                        MainApp.Application();
                    }
                    Console.Clear();
                    var unreadMessages = db.UnReadMessages(logedInUser.UserName);
                    if (unreadMessages.Count > 0)
                    {
                        if (MsgManager.Unread(unreadMessages.Count))
                        {
                            MsgManager.Print(unreadMessages);
                            db.ChangeReadState(logedInUser.UserName);
                        }                                                            
                    }                                        
                    while (exit != 0)
                    {
                        switch (logedInUser.Role)
                        {
                            case Role.User:
                                max = 4;
                                Menu.User();
                                break;
                            case Role.PowerUser:
                                Menu.PowerUser();
                                max = 5;
                                break;
                            case Role.Admin:
                                Menu.Admin();
                                max = 6;
                                break;
                            case Role.SuperAdmin:
                                max = 11;
                                Menu.Admin();
                                Menu.SuperAdmin();
                                break;
                        }

                        inputCheck = Console.ReadLine();


                        if (!int.TryParse(inputCheck, out exit) || (exit < 0 || exit > max)) 
                        {
                            Console.Clear();
                            Menu.InvalidInput();
                            Console.ReadKey();
                            
                            exit = -1;
                        } 

                        switch (exit)
                        {
                            case 1:
                                var myMessages = db.MyMessages(logedInUser);
                                MsgManager.Print(myMessages);
                                db.ChangeReadState(logedInUser.UserName);
                                break;
                            case 2:
                                var allMessages = db.AllMessages();
                                MsgManager.Print(allMessages);
                                db.ChangeReadState(logedInUser.UserName);
                                break;
                            case 3:
                                var newMsg = MsgManager.NewMsg(logedInUser);
                                if (newMsg != null)
                                {
                                    db.SendMessage(newMsg);
                                    FileAccess.SaveMessage(newMsg);
                                }
                                break;
                            case 4:
                                var user = UserManager.ConversationWith();
                                if (!string.IsNullOrEmpty(user))
                                {
                                    var converstation = db.Conversation(logedInUser.UserName, user);
                                    MsgManager.Print(converstation);
                                }
                                break;
                            case 5:

                                bool edit = true;
                                var editedMsg = MsgManager.EditOrDelete(edit);
                                if (editedMsg != null)
                                {
                                    db.EditMessage(editedMsg);
                                }
                                break;
                            case 6:
                                edit = false;
                                var deletedMsg = MsgManager.EditOrDelete(edit);
                                if (deletedMsg != null)
                                {
                                    db.DeleteMessage(deletedMsg);
                                }
                                break;
                            case 7:
                                var UserToAdd = UserManager.Create();
                                if (UserToAdd != null)
                                {
                                    db.AddUser(UserToAdd);
                                }

                                break;
                            case 8:

                                var dataBaseUsers = db.AllUsers();
                                UserManager.Print(dataBaseUsers);
                                break;
                            case 9:
                                string userToDelete = UserManager.Delete();
                                if (!string.IsNullOrEmpty(userToDelete))
                                {
                                    db.RemoveUser(userToDelete);
                                }
                                break;
                            case 10:
                                bool updatepass = true;
                                User updatePassword = UserManager.Update(updatepass);
                                if (updatePassword != null)
                                    db.UpdatePassword(updatePassword.UserName, updatePassword.Password);
                               
                                break;
                            case 11:
                                updatepass = false;
                                User updateRole = UserManager.Update(updatepass);
                                if (updateRole != null)
                                    db.UpdateRole(updateRole.UserName, updateRole.Role);
                                break;
                            case 0:
                                Environment.Exit(0);
                                break;
                        }

                    }
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}

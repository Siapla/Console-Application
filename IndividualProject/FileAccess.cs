using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IndividualProject
{
    public class FileAccess
    {
        //Αποθήκευση των συνομιλιών σε αρχεία .txt ανα 2 άτομα
        public static void SaveMessage(Message messageToSave)
        {
            
            string messageToFile = $"Id: {messageToSave.Id}\nSender:{messageToSave.Sender.UserName}" +
               $"\nReceiver:{messageToSave.Receiver.UserName}\n{messageToSave.DateTime}\n\n{messageToSave.MessageData}\n\n\n";

            string foldername = Directory.GetCurrentDirectory();
            foldername = Path.GetFullPath(Path.Combine(foldername, @"..\..\..\"));
            string pathstring = Path.Combine(foldername, "Messages");
            string pathstring2 = Path.Combine(foldername, "Messages");
            Directory.CreateDirectory(pathstring);
            string filename = string.Concat(messageToSave.Sender.UserName, "_", messageToSave.Receiver.UserName,".txt");
            pathstring = Path.Combine(pathstring, filename);
            string filename2 = string.Concat(messageToSave.Receiver.UserName, "_", messageToSave.Sender.UserName, ".txt");
            pathstring2 = Path.Combine(pathstring2, filename2);
            if (File.Exists(pathstring2))
            {
                File.AppendAllText(pathstring2, messageToFile);
            }
            else
            {
                File.AppendAllText(pathstring, messageToFile);
            }            
        }
    }
}

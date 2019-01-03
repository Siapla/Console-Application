using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class Message
    {
       

        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        [MaxLength(250)]
        public string MessageData { get; set; }
        
        public virtual User Sender { get; set; }
        
        public virtual User Receiver { get; set; }

        public bool ReadState { get; set; }

        public Message(string messageData, User sender, User receiver)
        {
            DateTime = DateTime.Now;
            MessageData = messageData;
            Sender = sender;
            Receiver = receiver;
            ReadState = false;
        }

        public Message()
        {
        }
    }
}

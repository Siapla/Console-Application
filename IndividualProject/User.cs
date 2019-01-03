using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class User 
    {

        [Key]
        [MinLength(3)]
        public string UserName { get; set; }

        
        public string Password { get; set; }

        public string Salt { get; set; }

        public Role Role { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Message> MessagesSended { get; set; }

        [InverseProperty("Receiver")]
        public virtual ICollection<Message> MessagesReceived { get; set; }

        public User(string userName, string password, Role role,string salt)
        {
            UserName = userName;
            Password = password;
            Role = role;
            Salt = salt;
        }

        public User()
        {
        }


    }
}

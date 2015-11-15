using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace email_client.domain
{
    public class User
    {
        private string password;
        private string username;
        private int id;

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public static User copy(User user)
        {
            return new User(user.Username, user.Password);
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public int Id { get { return id; } set { id = value; } }
    }
}

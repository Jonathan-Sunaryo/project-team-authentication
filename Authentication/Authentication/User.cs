using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication
{
    class User
    {
        
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string userName { get; set; }

      

        public User(string firstName, string lastName, string password, string userName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.userName = userName;
            

           
        }
    }
}

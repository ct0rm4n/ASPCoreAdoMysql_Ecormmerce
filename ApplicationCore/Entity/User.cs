using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entity
{
    class User
    {
        public User()
        {

        }
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public bool Delete { get; set; }

    }

}

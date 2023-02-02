using Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class User
    {
        public User(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
       
    }
}

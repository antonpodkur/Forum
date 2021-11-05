using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public ICollection<Post> Posts { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
     }
}
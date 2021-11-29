using System;
using System.Collections;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
    }
}
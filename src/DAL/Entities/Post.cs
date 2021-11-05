using System;

namespace DAL.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
using System;

namespace DAL.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
        
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
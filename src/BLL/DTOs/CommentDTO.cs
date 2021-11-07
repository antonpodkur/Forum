using System;
using DAL.Entities;

namespace BLL.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
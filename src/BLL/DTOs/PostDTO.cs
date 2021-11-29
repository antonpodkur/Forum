using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace BLL.DTOs
{
    public class PostDTO
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
        
        public ICollection<Comment> Comments { get; set; } 
    }
}
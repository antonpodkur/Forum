using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace BLL.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
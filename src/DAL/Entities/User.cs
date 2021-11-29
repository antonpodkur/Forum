using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class User: IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public ICollection<RefreshToken> RefreshTokens { get; set; }
     }
}
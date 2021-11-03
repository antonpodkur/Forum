using Microsoft.AspNetCore.Identity;

namespace API.Auth
{
    public class AuthUser: IdentityUser
    {
        public AuthUser() { }
        public AuthUser(string userName): base(userName) { }
    }
}
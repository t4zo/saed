using System.Collections.Generic;

namespace SAED.Api.Entities.Dto
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }

        public UserRequest WithoutPassword()
        {
            Password = "";
            return this;
        }
    }
}
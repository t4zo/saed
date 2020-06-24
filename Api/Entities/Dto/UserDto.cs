using System.Collections.Generic;

namespace SAED.Api.Entities.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }

        public UserDto WithoutPassword()
        {
            Password = "";
            return this;
        }
    }
}

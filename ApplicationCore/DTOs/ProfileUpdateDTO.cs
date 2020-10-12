using Microsoft.AspNetCore.Http;

namespace ApplicationCore.DTOs
{
    public class ProfileUpdateDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}

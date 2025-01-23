using Microsoft.AspNetCore.Identity;

namespace Exam1.Models
{
    public class User:IdentityUser
    {
        public string FullName {  get; set; }   
        public string PassWord {  get; set; }   
        public string Email {  get; set; }
    }
}

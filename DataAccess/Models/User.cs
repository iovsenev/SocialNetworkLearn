using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }

        public string GetFullName() => $"{LastName} {FirstName} {MiddleName}";

        public User()
        {
            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTThOhXxL4MjcM8bm5sduHd1sX3_201RbErASMz1nHEPrGxbcFwfcVhAJbsJdolxCCapkI&usqp=CAU";
            Status = "online";
            About = "Что-то обо мне!";
        }
    }
}

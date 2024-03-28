using DataAccess.Models;

namespace SocialNetwork.Models
{
    public class UserViewModel
    {
        public User User { get; }

        public List<User> Friends { get; set; }

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}

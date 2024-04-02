using DataAccess.Models;

namespace SocialNetwork.Models
{
    public class ChatViewModel
    {
        public User User { get; set; }
        public User RecipientUser { get; set; }
        public List<Message> History { get; set; }
        public MessageViewModel NewMessage { get; set; }

        public ChatViewModel()
        {
            NewMessage = new MessageViewModel();
        }
    }
}

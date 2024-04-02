using DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class MessageViewModel
    {
        public DateTime Date { get; set; }
        public User Sender { get; set; }
        public User Recipient { get; set; }

        [Display(Name = "Сообщение", Prompt = "Введите сообщение")]
        public string Message { get; set; } 
    }
}
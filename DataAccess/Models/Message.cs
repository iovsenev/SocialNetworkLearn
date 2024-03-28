using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("Message")]
    public class Message
    {
        [Required]
        public int Id { get; set; }

        [Required, DisplayName("Date")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Message")]
        public string Text { get; set; }

        [Required]
        [DisplayName("SenderId")]
        public string SenderId { get; set; }

        public User Sender { get; set; }

        [Required, DisplayName("RecipientId")]
        public string RecipientId { get; set; }

        public User Recipient { get; set; }

    }
}

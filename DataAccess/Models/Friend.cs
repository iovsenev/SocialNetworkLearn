
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("UserFriends")]
    public class Friend
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("UserId")]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [DisplayName("FriendId")]
        public string CurrentFriendId { get; set; }

        public User CurrentFriend { get; set; } 
    }
}

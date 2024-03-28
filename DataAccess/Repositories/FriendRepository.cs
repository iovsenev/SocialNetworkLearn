using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class FriendRepository : Repository<Friend>
    {
        public FriendRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddFriendAsync(User target, User currentFriend)
        {
            var friend = Set
                .AsEnumerable()
                .FirstOrDefault(x =>
                       x.UserId == target.Id && x.CurrentFriendId == currentFriend.Id);
            if (friend == null)
            {
                var item = new Friend()
                {
                    User = target,
                    UserId = target.Id,
                    CurrentFriendId = currentFriend.Id,
                    CurrentFriend = currentFriend,
                };

                await CreateAsync(item);
            }
        }

        public async Task<List<User>> GetFriendsByUserAsync(User user)
        {
            return Set
                .Include(x => x.CurrentFriend)
                .AsEnumerable()
                .Where(x => x.UserId == user.Id)
                .Select(x => x.CurrentFriend)
                .ToList();
        }

        public async Task DeleteFriendAsync(User user, User currentFriend)
        {
            var friend = Set
                .AsEnumerable()
                .FirstOrDefault(x =>
                x.UserId == user.Id && x.CurrentFriendId == currentFriend.Id);
            if (friend != null)
            {
                await DeleteAsync(friend);
            }
        }
    }
}

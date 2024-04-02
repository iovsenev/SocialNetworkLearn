using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Message>> GetMessages(User sender, User recipient)
        {
            Set.Include(x => x.Recipient);
            Set.Include(x => x.Sender);

            var outcoming = Set
                .AsEnumerable()
                .Where(x => x.SenderId == sender.Id &&  x.RecipientId == recipient.Id);
            var incoming = Set
                .AsEnumerable()
                .Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id);

            var result = new List<Message>();
            result.AddRange(incoming);
            result.AddRange(outcoming);
            return result
                .OrderBy(x=> x.Date)
                .ToList();
        }
    }
}

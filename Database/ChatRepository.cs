using AutoMapper;
using Core.Chat;
using Core.Chat.Entities;
using Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ChatRepository : IChatRepository
    {
        public BaseDbContext _dbContext;

        public ChatRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddConversation(Conversation conversation, CancellationToken token)
        {
            await _dbContext.AddAsync(conversation, token);
        }

        public async Task AddMessage(Message message, CancellationToken token)
        {
            await _dbContext.AddAsync(message, token);
        }
        public async Task<Conversation?> GetConversationWithMessages(Guid meUuid, Guid interlocutorUuid, CancellationToken token)
        {
            return await _dbContext.Conversation
                .Where(o => o.User1 == meUuid && o.User2 == interlocutorUuid ||
                    o.User1 == interlocutorUuid && o.User2 == meUuid)
                .Include(o => o.Messages)
                .FirstOrDefaultAsync(token);
        }


        public async Task<Conversation?> GetConversation(Guid meUuid, Guid interlocutorUuid, CancellationToken token)
        {
            return await _dbContext.Conversation
                .AsNoTracking()
                .Where(o => o.User1 == meUuid && o.User2 == interlocutorUuid ||
                    o.User1 == interlocutorUuid && o.User2 == meUuid)
                .FirstOrDefaultAsync(token);
        }

         public async Task<IEnumerable<ConversationItem>> GetMyConversations(Guid userUuid, CancellationToken token)
         {
            return await _dbContext.Conversation
                .Where(o => o.User1 == userUuid || o.User2 == userUuid)
                .Join(
                    _dbContext.User,
                    c => c.User1 == userUuid ? c.User2 : c.User1,
                    user => user.Uuid,
                    (conversation, user) => new ConversationItem
                    {
                        Uuid = conversation.Uuid,
                        UserUuid = user.Uuid,
                        FirstName = user.FirstName, 
                        LastName = user.LastName,
                        LastMessage = conversation.Messages.OrderByDescending(m => m.SendDate).First()
                    })
                .ToListAsync();
        }

        public async Task Save(CancellationToken token)
        {
            await _dbContext.SaveChangesAsync(token);
        }
    }
}

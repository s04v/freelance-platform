using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public interface IChatRepository
    {
        Task AddConversation(Conversation conversation, CancellationToken token);
        Task AddMessage(Message message, CancellationToken token);
        Task<Conversation?> GetConversationWithMessages(Guid meUuid, Guid interlocutorUuid, CancellationToken token);
        Task<Conversation?> GetConversation(Guid meUuid, Guid interlocutorUuid, CancellationToken token);
        Task<IEnumerable<ConversationItem>> GetMyConversations(Guid userUuid, CancellationToken token);
        Task Save(CancellationToken token);
    }
}

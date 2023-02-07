using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Shared.Dtos;

namespace Shop.Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public MessageRepository(StoreContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public void AddGroup(Group group) { _context.Groups.Add(group); }

    public void AddMessage(Message message) { _context.Messages.Add(message); }

    public void DeleteMessage(Message message) { _context.Messages.Remove(message); }

    public async Task<Connection?> GetConnection(string connectionId)
    { return await _context.Connections.FindAsync(connectionId); }

    public async Task<Group?> GetGroupForConnection(string connectionId)
    {
        return await _context.Groups
            .Include(x => x.Connections)
            .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
            .FirstOrDefaultAsync();
    }

    public async Task<Message?> GetMessage(int id) { return await _context.Messages.FindAsync(id); }

    public async Task<Group?> GetMessageGroup(string groupName)
    { return await _context.Groups.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName); }

    public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
    {
        var query = _context.Messages.OrderByDescending(x => x.MessageSent).AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && !u.RecipientDeleted),
            "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username && !u.SenderDeleted),
            _ => query.Where(u => u.RecipientUsername == messageParams.Username && !u.RecipientDeleted && u.DateRead == null)
        };

        var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

        var omer = await PagedList<MessageDto>
            .CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        return omer;
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
    {
        var query = _context.Messages
            .Where(
                m => m.RecipientUsername == currentUserName &&
                    !m.RecipientDeleted &&
                    m.SenderUsername == recipientUserName ||
                    m.RecipientUsername == recipientUserName &&
                    !m.SenderDeleted &&
                    m.SenderUsername == currentUserName)
            .OrderBy(m => m.MessageSent)
            .AsQueryable();

        var unreadMessages = query.Where(m => m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

        if (unreadMessages.Any())
        {
            foreach (var message in unreadMessages)
            {
                message.DateRead = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        return await query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public void RemoveConnection(Connection connection) { _context.Connections.Remove(connection); }

    public async Task<bool> SaveAllAsync() { return await _context.SaveChangesAsync() > 0; }
}
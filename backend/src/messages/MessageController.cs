namespace SkyExplorer;

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/messages")]
public class MessageController(AppDbContext context) : TimeFrameController<Message, MessageSetupDto, MessageUpdateDto>(context) {
	private static readonly Expression<Func<Message, DateTime>> GetMessageDateTime = m => m.SendingDate;

	protected override DbSet<Message> Set => Repository.Messages;
	protected override IQueryable<Message> GetQuery => Set.Include(m => m.Sender).Include(m => m.Recipient);

	protected override Expression<Func<Message, DateTime>> GetDateTime => GetMessageDateTime;
}
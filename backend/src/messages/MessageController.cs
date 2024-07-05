namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/messages")]
public class MessageController(AppDbContext repo) : Controller<Message, MessageSetupDTO, MessageUpdateDTO>(repo) {
	protected override DbSet<Message> Set => Repository.Messages;
}
namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/Message")]
public class MessageController(AppDbContext repo) : Controller<Message, MessageCreateDTO, MessageUpdateDTO>(repo) {
	[HttpGet]
	public Task<List<Message>> GetAll() =>
		Repository.Messages.ToListAsync();

	[HttpGet("{id}")]
	public ActionResult<Message> Get(int id) =>
		Repository.Messages.Find(id) switch {
			Message Message => Ok(Message),
			null => NotFound(),
		};

	[HttpPost]
	public async Task<ActionResult<Message>> AddMessage([FromForm] MessageCreateDTO dto) =>
		Ok(await Repository.Messages.AddAsync(new(dto)));
}
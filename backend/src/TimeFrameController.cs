namespace SkyExplorer;

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public abstract class TimeFrameController<T, TSetupDto, TUpdateDto>(AppDbContext context) : Controller<T, TSetupDto, TUpdateDto>(context) where T : class, IEntity where TSetupDto : class, IEntitySetup<T> where TUpdateDto : class, IEntityUpdate<T> {
	protected abstract Expression<Func<T, DateTime>> GetDateTime { get; }

	[HttpGet]
	public new async Task<ActionResult<List<T>>> GetInTimeFrame([FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, int offset = 0) {
		return Ok(await GetQuery
			.InTimeFrame(GetDateTime, timeFrame, offset)
			.ToListAsync()
		);
	}
}
namespace SkyExplorer;

public interface IEntitySetup<T> where T : class {
	public abstract T? Create(AppDbContext context, out string error);
}

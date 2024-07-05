namespace SkyExplorer;

public interface IEntityUpdate<T> where T : class {
	public abstract bool TryUpdate(T entity, AppDbContext context, out string error);
}
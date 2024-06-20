namespace SkyExplorer;

public abstract record Entity<T> {
	public Entity() { }
	public Entity(T dto) {
		Update(dto);
	}


	public abstract void Update(T dto);
}
namespace SkyExplorer;

public abstract record Entity<TCreateDTO, TUpdateDTO> {
	public Entity() { }
	public Entity(TCreateDTO dto) { }


	public abstract void Update(TUpdateDTO dto);
}
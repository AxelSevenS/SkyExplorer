namespace SkyExplorer;
public interface IEntity<T, TCreateDTO, TUpdateDTO> where T : class, IEntity<T, TCreateDTO, TUpdateDTO> {
	public static abstract T CreateFrom(TCreateDTO dto);
	public abstract void Update(TUpdateDTO dto);
}
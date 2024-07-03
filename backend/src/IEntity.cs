namespace SkyExplorer;
public interface IEntity<TData, TCreateDTO, TUpdateDTO> where TData : class, IEntity<TData, TCreateDTO, TUpdateDTO> {
	public static abstract TData CreateFrom(TCreateDTO dto);
	public abstract void Update(TUpdateDTO dto);
}
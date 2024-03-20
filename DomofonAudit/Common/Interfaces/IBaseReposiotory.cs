namespace Common.Interfaces
{
    public interface IBaseDao<T>
    {
        public Task<T> Create(T item);
        public Task<IEnumerable<T>> GetAll();
    }
}

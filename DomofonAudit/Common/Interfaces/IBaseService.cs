namespace Common.Interfaces
{
    public interface IBaseService<T>
    {
        public Task<T> Create(T item);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
    }
}

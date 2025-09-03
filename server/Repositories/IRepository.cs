public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    T Create(T data);
    T Update(int id, T updateData);
    bool Delete(int id);
}
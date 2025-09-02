namespace Questionnaires.API.Repositories
{
    public interface IRepository<T> where T : class
    {
        public int Add(T item);
        public void Edit(T item);
        public void Delete(T item);
        public T Get(int id);
        public List<T> GetAll();
    }
}

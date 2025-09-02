using Questionnaires.API.Data;

namespace Questionnaires.API.Repositories
{
    public class Repository<T>: IRepository<T> where T : class
    {
        public QuestionnairesDbContext _context { get; set; }
        public Repository(QuestionnairesDbContext context)
        {
            _context = context;
        }
        public int Add(T item)
        {
            _context.Set<T>().Add(item);       
            return _context.SaveChanges();     
        }

        public void Delete(T item)
        {
            _context.Set<T>().Add(item);       
            _context.SaveChanges();
        }

        public void Edit(T item)
        {
            _context.Set<T>().Update(item);    
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id); 
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList(); 
        }
    }
}

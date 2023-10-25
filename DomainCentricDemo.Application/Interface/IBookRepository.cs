using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Application.Interface {
    public interface IBookRepository {
        void Create(Book book);
        void Commit();
        void Save(Book book);
        void Delete(Book book);
        Book Load(int id);
        IEnumerable<Book> GetAll();
    }
}

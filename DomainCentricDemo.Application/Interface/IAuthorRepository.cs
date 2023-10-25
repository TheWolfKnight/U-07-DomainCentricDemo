using DomainCentricDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Application.Interface {
    public interface IAuthorRepository {

        void Create(Author author);
        void Commit();
        void Save(Author author);
        void Delete(Author author);
        Author Load(int id);
        IEnumerable<Author> GetAll();
    }
}

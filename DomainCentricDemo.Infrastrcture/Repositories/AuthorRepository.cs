using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Domain;
using DomainCentricDemo.Infrastrcture.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Infrastrcture.Repositories {
    public class AuthorRepository : IAuthorRepository {

        private readonly BookContext _db = null!;

        public AuthorRepository(BookContext db) => _db = db;

        void IAuthorRepository.Commit() => _db.SaveChanges();

        void IAuthorRepository.Create(Author author) => _db.Authors.Add(author);

        void IAuthorRepository.Delete(Author author) => _db.Authors.Remove(author);

        Author IAuthorRepository.Load(int id) => _db.Authors
            .AsNoTracking()
            .Include(auth => auth.Books)
            .ThenInclude(book => book.Authors)
            .First(auth => auth.Id == id);

        IEnumerable<Author> IAuthorRepository.GetAll() => _db.Authors.AsNoTracking()
            .Include(auth => auth.Books)
            .ThenInclude(book => book.Authors);

        void IAuthorRepository.Save(Author author) => _db.Authors.Update(author);
    }
}

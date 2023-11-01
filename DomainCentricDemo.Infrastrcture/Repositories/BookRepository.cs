﻿using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace DomainCentricDemo.Infrastrcture.Repositories {
    public class BookRepository : IBookRepository {
        private readonly BookContext _db = null!;

        public BookRepository(BookContext db) => _db = db;

        void IBookRepository.Commit() => _db.SaveChanges();

        void IBookRepository.Delete(Book book) => _db.Books.Remove(book);

        Book IBookRepository.Load(int id) => _db.Books
            .AsNoTracking()
            .Include(book => book.Authors)
            .First(book => book.Id == id);

        IEnumerable<Book> IBookRepository.GetAll() => _db.Books
            .AsNoTracking()
            .Include(book => book.Authors);

        void IBookRepository.Create(Book book) => _db.Books.Add(book);

        void IBookRepository.Save(Book book) {
            if (book.Authors != null)
                foreach (Author author in book.Authors)
                    _db.Attach(author);

            _db.Books.Update(book);
        }
    }
}

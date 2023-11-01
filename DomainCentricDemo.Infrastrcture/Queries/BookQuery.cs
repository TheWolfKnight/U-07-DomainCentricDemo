using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace DomainCentricDemo.Infrastrcture.Queries;

public class BookQuery : IBookQuery {
    private readonly IBookRepository _Repo;

    private readonly IMapper _Mapper = null!;

    public BookQuery(IBookRepository repo) {
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<Domain.Book, BookDto>();
            cfg.CreateMap<Domain.Author, AuthorDto>();
        });
        _Mapper = new Mapper(config);
        this._Repo = repo;
    }

    BookDto IBookQuery.Get(int id) {
        Domain.Book? book = _Repo.Load(id);
        if (book == null) return null!;

        return _Mapper.Map<BookDto>(book);
    }

    public IEnumerable<BookDto> GetAll() {
        foreach (Domain.Book book in _Repo.GetAll())
            yield return _Mapper.Map<BookDto>(book);

        yield break;
    }
}
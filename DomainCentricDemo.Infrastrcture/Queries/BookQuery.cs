using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace DomainCentricDemo.Infrastrcture.Queries;

public class BookQuery : IBookQuery {
    private readonly IBookRepository _repo;

    private readonly IMapper _Mapper = null!;

    public BookQuery(IBookRepository repo) {
        MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Book, BookDto>());
        _Mapper = new Mapper(config);
        this._repo = repo;
    }

    BookDto IBookQuery.Get(int id) {
        Domain.Book? book = _repo.Load(id);
        if (book == null) return null!;

        MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Book, BookDto>());
        Mapper mapper = new Mapper(config);

        return mapper.Map<BookDto>(book);
    }

    public IEnumerable<BookDto> GetAll() {
        foreach (Domain.Book book in _repo.GetAll())
            yield return _Mapper.Map<BookDto>(book);

        yield break;
    }
}
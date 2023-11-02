using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;

namespace DomainCentricDemo.Application.Implementation {
    public class BookCommand : IBookCommand {

        private readonly IBookRepository _BookRepository;
        private readonly IMapper _Mapper = null!;

        public BookCommand(IBookRepository bookRepository, IAuthorRepository authorRepo) {

            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<BookCommandRequestDto, Domain.Book>()
                    .BeforeMap((dto, dom) => dom.Authors = dto.AuthorIds.Select(authorRepo.Load));
                config.CreateMap<BookUpdateRequestDto, Domain.Book>();
            });

            _Mapper = new Mapper(config);

            _BookRepository = bookRepository;
        }

        void IBookCommand.Create(BookCommandRequestDto createRequest) {

            //Create domain object
            Domain.Book book = _Mapper.Map<Domain.Book>(createRequest);

            //Persist domain object
            _BookRepository.Save(book);
            _BookRepository.Commit();
        }

        void IBookCommand.Delete(BookDeleteRequestDto deleteRequest) {
            // load
            Domain.Book book = _BookRepository.Load(deleteRequest.Id);

            // commit
            _BookRepository.Delete(book);
            _BookRepository.Commit();

        }

        void IBookCommand.Update(BookUpdateRequestDto updateRequest) {

            // load
            Domain.Book book = _BookRepository.Load(updateRequest.Id);

            // update
            book = _Mapper.Map<Domain.Book>(updateRequest);

            // commit
            _BookRepository.Save(book);
            _BookRepository.Commit();

        }

    }
}

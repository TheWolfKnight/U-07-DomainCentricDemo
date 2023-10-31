using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;

namespace DomainCentricDemo.Application.Implementation {
    public class BookCommand : IBookCommand {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _Mapper = null!;

        public BookCommand(IBookRepository bookRepository) {

            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<BookCommandRequestDto, Domain.Book>();
                config.CreateMap<BookUpdateRequestDto, Domain.Book>();
            });

            _Mapper = new Mapper(config);

            _bookRepository = bookRepository;
        }

        void IBookCommand.Create(BookCommandRequestDto createRequest) {
            //Create domain object
            Domain.Book book = _Mapper.Map<Domain.Book>(createRequest);

            //Persist domain object
            _bookRepository.Create(book);
            _bookRepository.Commit();
        }

        void IBookCommand.Delete(BookDeleteRequestDto deleteRequest) {
            // load
            Domain.Book book = _bookRepository.Load(deleteRequest.Id);

            // commit
            _bookRepository.Delete(book);
            _bookRepository.Commit();

        }

        void IBookCommand.Update(BookUpdateRequestDto updateRequest) {

            // load
            Domain.Book book = _bookRepository.Load(updateRequest.Id);

            // update
            book = _Mapper.Map<Domain.Book>(updateRequest);

            // commit
            _bookRepository.Save(book);
            _bookRepository.Commit();

        }

    }
}

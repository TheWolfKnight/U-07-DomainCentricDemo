using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Application.Implementation {
    public class BookCommand : IBookCommand {
        private readonly IBookRepository _bookRepository;

        public BookCommand(IBookRepository bookRepository) {
            _bookRepository = bookRepository;
        }

        void IBookCommand.Create(BookCommandRequestDto createRequest) {
            //Create domain object
            var book = new Book {
                Authors = createRequest.Authors,
                Description = createRequest.Description,
                Title = createRequest.Title,
                RowVersion = createRequest.RowVersion,
            };

            //Persist domain object
            _bookRepository.Create(book);
            _bookRepository.Commit();

        }

        void IBookCommand.Delete(BookDeleteRequestDto deleteRequest) {
            // load
            Book book = _bookRepository.Load(deleteRequest.Id);

            // commit
            _bookRepository.Delete(book);
            _bookRepository.Commit();

        }

        void IBookCommand.Update(BookUpdateRequestDto updateRequest) {

            // load
            Book book = _bookRepository.Load(updateRequest.Id);

            // update
            book.Title = updateRequest.Title;
            book.Authors = updateRequest.Authors.Select(auth => new Author { Id = auth.Id }).ToList();
            book.Description = updateRequest.Description;
            book.RowVersion = updateRequest.RowVersion;

            // commit
            _bookRepository.Save(book);
            _bookRepository.Commit();

        }

    }
}

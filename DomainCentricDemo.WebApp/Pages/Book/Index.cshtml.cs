using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace DomainCentricDemo.WebApp.Pages.Book {
    public class IndexModel : PageModel {
        private readonly IBookQuery _BookQuery;
        private readonly IAuthorQuery _AuthorQuery;

        private readonly IMapper _Mapper;

        public List<BookViewModel> Books { get; set; } = null!;

        public IndexModel(IBookQuery queryService, IAuthorQuery authorQuery) {
            _BookQuery = queryService;
            Books = new List<BookViewModel>();

            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<BookDto, BookViewModel>()
                    .BeforeMap((dto, viewModel) => {
                        viewModel.AuthorIds = dto.Authors?.Select(auth => auth.Id);
                    });
            });
            _Mapper = new Mapper(config);
            _AuthorQuery = authorQuery;
        }

        /// <summary>
        /// Her vil vi gerne returnere en liste af b�ger
        /// </summary>
        public void OnGet() {
            IEnumerable<BookDto> bookDtos = _BookQuery.GetAll();

            foreach (BookDto book in bookDtos) {
                Books.Add(_Mapper.Map<BookViewModel>(book));
            }
        }

        public IEnumerable<AuthorDto> GetBookAuthors(BookViewModel book) {
            if (book.AuthorIds == null) yield break;
            foreach (AuthorDto author in book.AuthorIds.Select(_AuthorQuery.Get))
                yield return author;
            yield break;
        }

    }
}

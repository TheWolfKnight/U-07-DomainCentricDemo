using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Infrastrcture.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DomainCentricDemo.WebApp.MapperProfiles;


namespace DomainCentricDemo.WebApp.Pages.Book {
    public class IndexModel : PageModel {
        private readonly IBookQuery _BookQuery;
        private readonly IAuthorQuery _AuthorQuery;

        private readonly IMapper _Mapper;

        public IEnumerable<BookViewModel> Books { get; set; } = null!;

        public IndexModel(IBookQuery bookQuery, IAuthorQuery authorQuery) {
            _BookQuery = bookQuery;
            _AuthorQuery = authorQuery;

            MapperConfiguration config = new MapperConfiguration(config => {
                Profile prfile = new BookMapperProfile(authorQuery);
                config.AddProfile(prfile);
            });
            _Mapper = new Mapper(config);
        }

        /// <summary>
        /// Her vil vi gerne returnere en liste af b�ger
        /// </summary>
        public IActionResult OnGet() {
            Books = _BookQuery.GetAll().Select(_Mapper.Map<BookViewModel>);
            return Page();
        }

        public IEnumerable<AuthorDto> GetBookAuthors(BookViewModel book) {
            if (book.AuthorIds == null) yield break;
            foreach (AuthorDto author in book.AuthorIds.Select(_AuthorQuery.Get))
                yield return author;
            yield break;
        }

    }
}

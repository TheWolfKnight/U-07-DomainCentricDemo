using AutoMapper;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.WebApp.MapperProfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Author {
    public class IndexModel : PageModel
    {
        public IEnumerable<AuthorViewModel> Authors { get;set; } = default!;

        private readonly IAuthorQuery _Query = null!;
        private readonly IBookQuery _BookQuery = null!;
        private readonly IMapper _Mapper = null!;

        public IndexModel(IAuthorQuery queryService, IBookQuery bookQuery)
        {
            _Query = queryService;
            _BookQuery = bookQuery;

            MapperConfiguration config = new MapperConfiguration(config => {
                Profile prfile = new AuthorMapperProfile(bookQuery);
                config.AddProfile(prfile);
            });
            _Mapper = new Mapper(config);
        }

        public IActionResult OnGet()
        {
            Authors = _Query.GetAll().Select(_Mapper.Map<AuthorViewModel>);
            return Page();
        }

        public string GetBooksTitles(AuthorViewModel author) {

            string result = string.Empty;
            result = string.Join(", ", author.BookIds!.Take(4).Select(_BookQuery.Get).Select(book => book.Title));
            result = string.Join("", result.Take(17));
            result = result.PadRight(20, '.');
            return result;
        }
    }
}

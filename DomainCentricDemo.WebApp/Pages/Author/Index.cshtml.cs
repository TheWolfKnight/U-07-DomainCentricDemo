using Microsoft.AspNetCore.Mvc.RazorPages;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc;

using DomainCentricDemo.Application.Dto;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class IndexModel : PageModel
    {
        public List<AuthorViewModel> Authors { get;set; } = default!;

        private readonly IAuthorQuery _Query = null!;
        private readonly IBookQuery _BookQuery = null!;
        private readonly IMapper _Mapper = null!;

        public IndexModel(IAuthorQuery queryService, IBookQuery bookQuery)
        {
            _Query = queryService;
            _BookQuery = bookQuery;
            Authors = new List<AuthorViewModel>();

            AutoMapper.IConfigurationProvider config = new MapperConfiguration(config => {
                config.CreateMap<AuthorDto, AuthorViewModel>()
                    .BeforeMap((dto, viewModel) => viewModel.BookIds = dto.Books.Select(book => book.Id));
            });
            _Mapper = new Mapper(config);
        }

        public IActionResult OnGet()
        {
            foreach (AuthorDto dto in _Query.GetAll()) {
                Authors.Add(_Mapper.Map<AuthorViewModel>(dto));
            }

            return Page();
        }

        public string GetBooksTitles(AuthorViewModel author) {

            string result = string.Empty;
            result = string.Join(", ", author.BookIds.Take(4).Select(_BookQuery.Get).Select(book => book.Title));
            result = string.Join("", result.Take(17));
            result = result.PadRight(20, '.');
            return result;
        }
    }
}

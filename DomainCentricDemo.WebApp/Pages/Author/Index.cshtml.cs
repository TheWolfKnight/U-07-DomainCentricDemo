using Microsoft.AspNetCore.Mvc.RazorPages;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc;

using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class IndexModel : PageModel
    {

        private readonly IAuthorQuery _Query = null!;
        public List<AuthorViewModel> Authors { get;set; } = default!;

        public IndexModel(IAuthorQuery queryService)
        {
            _Query = queryService;
            Authors = new List<AuthorViewModel>();
        }


        public IActionResult OnGet()
        {
            foreach (AuthorDto dto in _Query.GetAll()) {
                Authors.Add(new AuthorViewModel {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    SirName = dto.SirName,
                    Books = dto.Books,
                    RowVersion = dto.RowVersion
                });
            }

            return Page();
        }
    }
}

using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book {
    public class DetailsModel : PageModel {
        private readonly IBookQuery _Query = null!;

        [BindProperty]
        public BookViewModel Book { get; set; } = default!;

        public DetailsModel(IBookQuery query) {
            _Query = query;
        }

        public IActionResult OnGet(int? id) {
            if (!id.HasValue) return NotFound();

            BookDto book = _Query.Get(id.Value);
            if (book == null) return NotFound();

            Book = new BookViewModel {
                Id = book.Id,
                Title = book.Title,
                Authors = book.Authors,
                Description = book.Description,
            };

            return Page();
        }
    }
}

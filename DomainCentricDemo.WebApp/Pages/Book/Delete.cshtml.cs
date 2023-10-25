using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book {
    public class DeleteModel : PageModel {
        private readonly IBookCommand _Command = null!;
        private readonly IBookQuery _Query = null!;

        [BindProperty]
        public BookViewModel Book { get; set; } = default!;

        public DeleteModel(IBookQuery query, IBookCommand command) {
            _Query = query;
            _Command = command;
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

        public IActionResult OnPostAsync(int? id) {
            if (!id.HasValue) return NotFound();

            _Command.Delete(new BookDeleteRequestDto { Id = id.Value });

            return RedirectToPage("./Index");
        }
    }
}

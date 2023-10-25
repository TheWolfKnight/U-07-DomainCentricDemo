using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book {
    public class EditModel : PageModel {

        private readonly IBookQuery _Query;
        private readonly IBookCommand _Command;

        [BindProperty]
        public BookViewModel Book { get; set; } = default!;

        public EditModel(IBookQuery query, IBookCommand command) {
            _Query = query;
            _Command = command;
        }

        public IActionResult OnGet(int? id) {
            if (id == null) return NotFound();

            BookDto book = _Query.Get(id.Value);
            if (book == null) return NotFound();

            Book = new BookViewModel {
                Id = book.Id,
                Title = book.Title,
                Authors = book.Authors,
                Description = book.Description,
                RowVersion = book.RowVersion,
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost() {
            if (!ModelState.IsValid) return Page();

            _Command.Update(new BookUpdateRequestDto {
                Id = Book.Id,
                Title = Book.Title,
                Authors = Book.Authors,
                Description = Book.Description,
                RowVersion = Book.RowVersion
            });

            return RedirectToPage("./Index");
        }
    }
}

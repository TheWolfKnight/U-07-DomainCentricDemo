using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book;

public class CreateModel : PageModel {
    private readonly IBookCommand _bookCommand;

    public CreateModel(IBookCommand bookCommand) {
        _bookCommand = bookCommand;
    }

    [BindProperty] public BookViewModel Book { get; set; } = default!;

    public IActionResult OnGet() {
        return Page();
    }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public IActionResult OnPost() {
        if (!ModelState.IsValid) return Page();

        List<Domain.Author> authors = Book.Authors.Select(auth => new Domain.Author { Id = auth.Id }).ToList();

        _bookCommand.Create(new BookCommandRequestDto { Authors = authors, Description = Book.Description, Title = Book.Title });

        return RedirectToPage("./Index");
    }
}
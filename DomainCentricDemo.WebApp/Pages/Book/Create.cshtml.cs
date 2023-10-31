using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.WebApp.Pages.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book;

public class CreateModel : PageModel {
    private readonly IBookCommand _bookCommand;

    private readonly IMapper _Mapper;

    [BindProperty]
    public BookViewModel Book { get; set; } = default!;

    public CreateModel(IBookCommand bookCommand) {
        _bookCommand = bookCommand;

        MapperConfiguration config = new MapperConfiguration(config => {
            config.CreateMap<BookViewModel, BookCommandRequestDto>();
        });
        _Mapper = new Mapper(config);
    }

    public IActionResult OnGet() {
        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public IActionResult OnPost() {
        if (!ModelState.IsValid) return Page();

        _bookCommand.Create(_Mapper.Map<BookCommandRequestDto>(Book));

        return RedirectToPage("./Index");
    }
}
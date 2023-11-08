using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.WebApp.MapperProfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomainCentricDemo.WebApp.Pages.Book;

public class CreateModel : PageModel {

    public readonly IAuthorQuery AuthorQuery;

    private readonly IBookCommand _BookCommand;

    private readonly IMapper _Mapper;

    [BindProperty]
    public BookViewModel Book { get; set; } = default!;

    public SelectList AuthorList { get; set; }

    public CreateModel(IBookCommand bookCommand, IAuthorQuery authorQuery, IBookQuery bookQuery) {
        _BookCommand = bookCommand;
        AuthorQuery = authorQuery;

        MapperConfiguration config = new MapperConfiguration(config => {
            Profile prfile = new BookMapperProfile(authorQuery);
            config.AddProfile(prfile);
        });
        _Mapper = new Mapper(config);

        AuthorList = new SelectList(authorQuery.GetAll()
                                     .Select(auth => new { Id = auth.Id, FullName = $"{auth.SirName} {auth.FirstName}" }),
                                    "Id", "FullName");
    }

    public IActionResult OnGet() {
        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public IActionResult OnPost() {
        if (!ModelState.IsValid) return Page();

        _BookCommand.Create(_Mapper.Map<BookCommandRequestDto>(Book));

        return RedirectToPage("./Index");
    }
}
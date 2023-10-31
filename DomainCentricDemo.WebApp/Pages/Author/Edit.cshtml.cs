using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainCentricDemo.Domain;
using DomainCentricDemo.Infrastrcture;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Application.Dto;
using AutoMapper;

namespace DomainCentricDemo.WebApp.Pages.Author
{
  public class EditModel : PageModel
  {

    private readonly IAuthorCommand _Command;
    private readonly IAuthorQuery _Query;

    private readonly IMapper _Mapper;

    public EditModel(IAuthorQuery query, IAuthorCommand command)
    {
      _Command = command;
      _Query = query;

      MapperConfiguration config = new MapperConfiguration(config =>
      {
        config.CreateMap<AuthorDto, AuthorViewModel>();
        config.CreateMap<AuthorViewModel, AuthorCommandRequestDto>();
      });
      _Mapper = new Mapper(config);
    }

    [BindProperty]
    public AuthorViewModel Author { get; set; } = default!;

    public IActionResult OnGet(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      AuthorDto author = _Query.Get(id.Value);
      if (author == null) return NotFound();

      Author = new AuthorViewModel
      {
        Id = author.Id,
        FirstName = author.FirstName,
        SirName = author.SirName,
        Books = author.Books,
        RowVersion = author.RowVersion,
      };

      return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public IActionResult OnPost()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _Command.Update(_Mapper.Map<AuthorUpdateRequestDto>(Author));


      return RedirectToPage("./Index");
    }


  }
}

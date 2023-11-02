using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DomainCentricDemo.Application.Interface;
using AutoMapper;
using DomainCentricDemo.Application.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class CreateModel : PageModel
    {

        private readonly IAuthorCommand _Command = null!;

        private readonly IMapper _Mapper = null!;

        [BindProperty]
        public AuthorViewModel Author { get; set; } = default!;

        public SelectList BookList { get; set; } = null!;

        public CreateModel(IAuthorCommand command, IBookQuery bookQuery) {
            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<AuthorViewModel, AuthorCommandRequestDto>();
            });
            _Mapper = new Mapper(config);

            _Command = command;

            BookList = new SelectList(
                bookQuery
                    .GetAll()
                    .Select(book => new { Id = book.Id, Title = book.Title }),
                "Id", "Title");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            AuthorCommandRequestDto dto = _Mapper.Map<AuthorCommandRequestDto>(Author);

            _Command.Create(dto);

            return RedirectToPage("./Index");
        }
    }
}

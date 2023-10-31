using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DomainCentricDemo.Application.Interface;
using AutoMapper;
using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class CreateModel : PageModel
    {
        public readonly IBookQuery _BookQuery = null!;

        private readonly IAuthorCommand _Command = null!;

        private readonly IMapper _Mapper = null!;

        [BindProperty]
        public AuthorViewModel Author { get; set; } = default!;

        public CreateModel(IAuthorCommand command, IBookQuery bookQuery) {
            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<AuthorCommandRequestDto, AuthorViewModel>();
            });
            _Mapper = new Mapper(config);

            _Command = command;
            _BookQuery = bookQuery;
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

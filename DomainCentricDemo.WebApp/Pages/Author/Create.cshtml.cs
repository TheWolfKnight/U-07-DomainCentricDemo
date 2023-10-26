using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DomainCentricDemo.Domain;
using DomainCentricDemo.Infrastrcture;
using DomainCentricDemo.Application.Interface;
using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Infrastrcture.Migrations;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorCommand _Command = null!;

        private readonly IMapper _Mapper = null!;

        [BindProperty]
        public AuthorViewModel Author { get; set; } = default!;

        public CreateModel(IAuthorCommand command)
        {
            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<AuthorCommandRequestDto, AuthorViewModel>();
            });
            _Mapper = new Mapper(config);

            _Command = command;
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

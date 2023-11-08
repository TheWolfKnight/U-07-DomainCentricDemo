using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DomainCentricDemo.Domain;
using DomainCentricDemo.Infrastrcture;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Application.Dto;
using AutoMapper;
using DomainCentricDemo.WebApp.MapperProfiles;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorCommand _Command;
        private readonly IAuthorQuery _Query;

        private readonly IMapper _Mapper;

        [BindProperty]
        public AuthorViewModel Author { get; set; } = default!;

        public DeleteModel(IAuthorCommand command, IAuthorQuery authorQuery, IBookQuery bookQuery)
        {
            MapperConfiguration config = new MapperConfiguration(config => {
                Profile prfile = new AuthorMapperProfile(bookQuery);
                config.AddProfile(prfile);
            });
            _Mapper = new Mapper(config);

            _Query = authorQuery;
            _Command = command;
        }


        public IActionResult ÖnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            AuthorDto author = _Query.Get(id.Value);
            AuthorDeleteRequestDto dto = _Mapper.Map<AuthorDeleteRequestDto>(author);

            _Command.Delete(dto);
            return RedirectToPage("./Index");
        }
    }
}

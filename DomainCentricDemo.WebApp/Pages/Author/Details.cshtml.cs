using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using AutoMapper;
using DomainCentricDemo.WebApp.MapperProfiles;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class DetailsModel : PageModel
    {

        private readonly IAuthorQuery _Query;

        private readonly IMapper _Mapper;

        [BindProperty]
        public AuthorViewModel Author { get; set; } = default!; 

        public DetailsModel(IAuthorQuery authorQuery, IBookQuery bookQuery)
        {
            MapperConfiguration config = new MapperConfiguration(config => {
                Profile prfile = new AuthorMapperProfile(bookQuery);
                config.AddProfile(prfile);
            });
            _Mapper = new Mapper(config);

            _Query = authorQuery;
        }

        public IActionResult OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            AuthorDto author = _Query.Get(id.Value);

            Author = _Mapper.Map<AuthorViewModel>(author);

            return Page();
        }
    }
}

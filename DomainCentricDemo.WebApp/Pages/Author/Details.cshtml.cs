using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using AutoMapper;

namespace DomainCentricDemo.WebApp.Pages.Author
{
    public class DetailsModel : PageModel
    {

        private readonly IAuthorQuery _Query;

        private readonly IMapper _Mapper;

        [BindProperty]
        public AuthorViewModel Author { get; set; } = default!; 

        public DetailsModel(IAuthorQuery query)
        {

            MapperConfiguration config = new MapperConfiguration(config => config.CreateMap<AuthorViewModel, AuthorDto>());
            _Mapper = new Mapper(config);

            _Query = query;
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

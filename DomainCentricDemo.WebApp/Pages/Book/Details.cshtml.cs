using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.WebApp.MapperProfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book {
    public class DetailsModel : PageModel {
        private readonly IBookQuery _Query = null!;

        private readonly IMapper _Mapper;

        [BindProperty]
        public BookViewModel Book { get; set; } = default!;

        public DetailsModel(IBookQuery query, IAuthorQuery authorQuery) {
            _Query = query;

            MapperConfiguration config = new MapperConfiguration(config => {
                Profile prfile = new BookMapperProfile(authorQuery);
                config.AddProfile(prfile);
            });
            _Mapper = new Mapper(config);

        }

        public IActionResult OnGet(int? id) {
            if (!id.HasValue) return NotFound();

            BookDto book = _Query.Get(id.Value);
            if (book == null) return NotFound();

            Book = _Mapper.Map<BookViewModel>(book);

            return Page();
        }
    }
}

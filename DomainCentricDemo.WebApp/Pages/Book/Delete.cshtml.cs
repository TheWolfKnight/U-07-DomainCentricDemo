using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book {
    public class DeleteModel : PageModel {
        private readonly IBookCommand _Command = null!;
        private readonly IBookQuery _Query = null!;

        private readonly IMapper _Mapper;

        [BindProperty]
        public BookViewModel Book { get; set; } = default!;

        public DeleteModel(IBookQuery query, IBookCommand command) {
            _Query = query;
            _Command = command;

            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<Domain.Book, BookViewModel>();
                config.CreateMap<BookViewModel, BookDeleteRequestDto>();
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

        public IActionResult OnPostAsync(int? id) {
            if (!id.HasValue) return NotFound();

            _Command.Delete(_Mapper.Map<BookDeleteRequestDto>(Book));

            return RedirectToPage("./Index");
        }
    }
}

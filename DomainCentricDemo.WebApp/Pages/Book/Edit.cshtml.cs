using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.WebApp.Pages.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.WebApp.Pages.Book {
    public class EditModel : PageModel {

        private readonly IBookQuery _Query;
        private readonly IBookCommand _Command;

        private readonly IMapper _Mapper;

        [BindProperty]
        public BookViewModel Book { get; set; } = default!;

        public EditModel(IBookQuery query, IBookCommand command) {
            _Query = query;
            _Command = command;

            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<BookDto,BookViewModel>();
                config.CreateMap<BookViewModel, BookUpdateRequestDto>();
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost() {
            if (!ModelState.IsValid) return Page();

            _Command.Update(_Mapper.Map<BookUpdateRequestDto>(Book));

            return RedirectToPage("./Index");
        }
    }
}

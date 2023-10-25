using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace DomainCentricDemo.WebApp.Pages.Book {
    public class IndexModel : PageModel {
        private readonly IBookQuery _queryService;
        public List<BookViewModel> Books { get; set; } = null!;

        public IndexModel(IBookQuery queryService) {
            _queryService = queryService;
            Books = new List<BookViewModel>();
        }

        /// <summary>
        ///Her vil vi gerne returnere en liste af bøger
        /// </summary>
        public void OnGet() {
            IEnumerable<BookDto> bookDtos = _queryService.GetAll();

            foreach (BookDto book in bookDtos) {
                Books.Add(new BookViewModel {
                    Id = book.Id,
                    Title = book.Title,
                    Authors = book.Authors,
                    Description = book.Description
                });
            }
        }
    }
}

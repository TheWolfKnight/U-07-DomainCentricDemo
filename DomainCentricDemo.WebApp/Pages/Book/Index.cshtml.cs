using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace DomainCentricDemo.WebApp.Pages.Book {
    public class IndexModel : PageModel {
        private readonly IBookQuery _queryService;

        private readonly IMapper _Mapper;

        public List<BookViewModel> Books { get; set; } = null!;

        public IndexModel(IBookQuery queryService) {
            _queryService = queryService;
            Books = new List<BookViewModel>();

            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<BookDto, BookViewModel>();
            });
            _Mapper = new Mapper(config);

        }

        /// <summary>
        ///Her vil vi gerne returnere en liste af b�ger
        /// </summary>
        public void OnGet() {
            IEnumerable<BookDto> bookDtos = _queryService.GetAll();

            foreach (BookDto book in bookDtos) {
                Books.Add(_Mapper.Map<BookViewModel>(book));
            }
        }
    }
}

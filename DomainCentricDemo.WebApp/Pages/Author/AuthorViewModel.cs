
using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.WebApp.Pages.Author {
    public class AuthorViewModel {


        public int Id { get; set; }

        public string FirstName { get; set; }
        public string SirName { get; set; }

        public List<BookDto> Books { get; set; }

        public byte[] RowVersion { get; set; }

    }
}


using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.WebApp.Pages.Author {
    public class AuthorViewModel {


        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string SirName { get; set; } = string.Empty;

        public List<BookDto> Books { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

    }
}

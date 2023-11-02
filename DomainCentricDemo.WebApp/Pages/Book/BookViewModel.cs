
namespace DomainCentricDemo.WebApp.Pages.Book {

    //Kopieret fra DTO'en, da det er den vej vi arbejder - indefra og ud.
    public class BookViewModel {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<int> AuthorIds { get; set; }

        public byte[]? RowVersion { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DomainCentricDemo.Domain {
    public class Book {
        // Skal kunne ændres på - så kaldes det entities.
        // Man deler ting op i ting, der ændrer sig. Entities kan ændre sig, og de har derfor et id

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [MinLength(1)]
        [MaxLength(5)]
        public ICollection<Author>? Authors { get; set; } = new List<Author>();

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Skal have noget funktionalitet hen ad vejen for at det bliver en god model
    }
}
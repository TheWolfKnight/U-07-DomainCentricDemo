﻿using System.ComponentModel.DataAnnotations;

namespace DomainCentricDemo.Domain {
    public class Book {
        // Skal kunne ændres på - så kaldes det entities.
        // Man deler ting op i ting, der ændrer sig. Entities kan ændre sig, og de har derfor et id

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Author> Authors { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        // Skal have noget funktionalitet hen ad vejen for at det bliver en god model
    }
}
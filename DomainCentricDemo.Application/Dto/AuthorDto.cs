using DomainCentricDemo.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Application.Dto {
    public class AuthorDto {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string SirName { get; set; } = string.Empty;

        public IEnumerable<BookDto> Books { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;
    }
}

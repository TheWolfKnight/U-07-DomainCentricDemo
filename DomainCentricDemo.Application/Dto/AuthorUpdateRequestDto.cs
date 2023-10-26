using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Application.Dto {
    public class AuthorUpdateRequestDto {
        public int Id { get; internal set; }

        public string FirstName { get; set; }
        public string SirName { get; set; }

        public List<BookDto> Books { get; set; }

        public byte[] RowVersion { get; set; }

    }
}

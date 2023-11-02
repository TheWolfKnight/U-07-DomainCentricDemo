using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Application.Dto {
    public class AuthorCommandRequestDto {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string SirName { get; set; } = string.Empty;

        public IEnumerable<int> BookIds { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

    }
}

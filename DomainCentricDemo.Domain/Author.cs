using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Domain {
    public class Author {

        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string SirName { get; set; } = string.Empty;

        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}

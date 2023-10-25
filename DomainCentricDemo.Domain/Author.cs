using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Domain {
    public class Author {

        public int Id { get; set; }

        public string FirstName { get; set; }
        public string SirName { get; set; }

        public List<Book> Books { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

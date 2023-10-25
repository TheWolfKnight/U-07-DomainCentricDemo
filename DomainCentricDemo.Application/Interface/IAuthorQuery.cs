using DomainCentricDemo.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Application.Interface {
    public interface IAuthorQuery {
        AuthorDto Get(int id);
        IEnumerable<AuthorDto> GetAll();
    }
}

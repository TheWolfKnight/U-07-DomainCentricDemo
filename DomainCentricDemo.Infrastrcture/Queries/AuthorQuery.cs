using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCentricDemo.Infrastrcture.Queries {
    public class AuthorQuery : IAuthorQuery {
        private readonly IAuthorRepository _repo = null!;

        private readonly IMapper _Mapper = null!;

        public AuthorQuery(IAuthorRepository repo) {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Author, AuthorDto>());
            _Mapper = new Mapper(config);

            this._repo = repo;
        }

        AuthorDto IAuthorQuery.Get(int id) {
            Domain.Author author = _repo.Load(id);
            if (author == null) return null!;

            AuthorDto dto = _Mapper.Map<AuthorDto>(author);
            return dto;
        }

        IEnumerable<AuthorDto> IAuthorQuery.GetAll() {
            foreach (Domain.Author author in _repo.GetAll()) {
                yield return _Mapper.Map<AuthorDto>(author);
            }
            yield break;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;

namespace DomainCentricDemo.Application.Implementation {
    public class AuthorCommand : IAuthorCommand {

        private readonly IAuthorRepository _AuthorRepository = null!;

        private readonly IMapper _Mapper = null!;

        public AuthorCommand(IAuthorRepository authorRepository, IBookRepository bookRepo) {
            MapperConfiguration config = new MapperConfiguration(config => {
                config.CreateMap<AuthorCommandRequestDto, Domain.Author>()
                    .BeforeMap((dto, dom) => dom.Books = dto.BookIds.Select(bookRepo.Load).ToArray());
                config.CreateMap<AuthorUpdateRequestDto, Domain.Author>();
            });
            _Mapper = new Mapper(config);

            _AuthorRepository = authorRepository;
        }

        void IAuthorCommand.Create(AuthorCommandRequestDto createRequest) {
            Domain.Author author = _Mapper.Map<Domain.Author>(createRequest);

            _AuthorRepository.Save(author);
            _AuthorRepository.Commit();
        }

        void IAuthorCommand.Delete(AuthorDeleteRequestDto deleteRequest) {

            Domain.Author author = _AuthorRepository.Load(deleteRequest.Id);

            _AuthorRepository.Delete(author);
            _AuthorRepository.Commit();

        }

        void IAuthorCommand.Update(AuthorUpdateRequestDto updateRequest) {
            Domain.Author author = _AuthorRepository.Load(updateRequest.Id);

            author = _Mapper.Map<Domain.Author>(updateRequest);

            _AuthorRepository.Save(author);
            _AuthorRepository.Commit();
        }
    }
}

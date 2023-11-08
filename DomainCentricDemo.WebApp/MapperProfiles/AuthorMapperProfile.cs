
using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.WebApp.Pages.Author;

namespace DomainCentricDemo.WebApp.MapperProfiles {
    public class AuthorMapperProfile : Profile {

        public AuthorMapperProfile(IBookQuery bookQuery) {
            CreateMap<AuthorDto, AuthorViewModel>()
                .BeforeMap((dto, viewModel) => viewModel.BookIds = dto.Books.Select(book => book.Id))
                .ReverseMap()
                .BeforeMap((viewModel, dto) => dto.Books = viewModel.BookIds.Select(bookQuery.Get));
            CreateMap<AuthorViewModel, AuthorUpdateRequestDto>();
            CreateMap<AuthorViewModel, AuthorDeleteRequestDto>();
            CreateMap<AuthorViewModel, AuthorCommandRequestDto>();
        }
    }
}

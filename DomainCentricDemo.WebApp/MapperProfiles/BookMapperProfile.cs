
using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.WebApp.Pages.Book;

namespace DomainCentricDemo.WebApp.MapperProfiles {
    public class BookMapperProfile : Profile {

        public BookMapperProfile(IAuthorQuery authorQuery) {
            CreateMap<BookDto, BookViewModel>()
                .BeforeMap((dto, viewModel) => viewModel.AuthorIds = dto.Authors!.Select(auth => auth.Id))
                .ReverseMap()
                .BeforeMap((viewModel, dto) => dto.Authors = viewModel.AuthorIds.Select(authorQuery.Get));

            CreateMap<BookViewModel, BookUpdateRequestDto>()
                .BeforeMap((viewModel, dto) => dto.Authors = viewModel.AuthorIds.Select(authorQuery.Get));
            CreateMap<BookViewModel, BookCommandRequestDto>();
            CreateMap<BookViewModel, BookDeleteRequestDto>();
        }

    }
}

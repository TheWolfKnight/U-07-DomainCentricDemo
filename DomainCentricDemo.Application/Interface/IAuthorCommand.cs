using DomainCentricDemo.Application.Dto;


namespace DomainCentricDemo.Application.Interface {
    public interface IAuthorCommand {

        void Create(AuthorCommandRequestDto createRequest);
        void Delete(AuthorDeleteRequestDto deleteRequest);
        void Update(AuthorUpdateRequestDto updateRequest);
    }
}

using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.Application.Interface {
    //Starter med at definere kontrakten mellem det yderste og mellemste lag. 
    //Der skal kun være adgang til interfaces og dto'er
    public interface IBookCommand {
        void Create(BookCommandRequestDto createRequest);
        void Delete(BookDeleteRequestDto deleteRequest);
        void Update(BookUpdateRequestDto bookUpdateRequestDto);
    }
}

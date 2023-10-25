using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Application.Dto;

public class BookCommandRequestDto {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Author> Authors { get; set; } = null!;

    public byte[] RowVersion { get; set; }
}
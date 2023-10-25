namespace DomainCentricDemo.Application.Dto {
    public class BookDeleteRequestDto {

        public int Id { get; set; }

        public byte[]? RowVersion { get; set; }

    }
}
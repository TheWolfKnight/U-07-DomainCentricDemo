﻿namespace DomainCentricDemo.Application.Dto {
    public class BookUpdateRequestDto {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<AuthorDto>? Authors { get; set; }

        public byte[]? RowVersion { get; set; }
    }
}
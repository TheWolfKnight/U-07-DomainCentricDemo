using System.ComponentModel.DataAnnotations;

namespace DomainCentricDemo.Application.Dto;

/// <summary>
/// Dto for Books - de er ofte ens ift. domæne-modellen til start,
/// men over tid kan der måske ske ændringer.
/// Her er det rart med en dto
/// </summary>
public class BookDto {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IEnumerable<AuthorDto>? Authors { get; set; }

    public byte[]? RowVersion { get; set; }
}
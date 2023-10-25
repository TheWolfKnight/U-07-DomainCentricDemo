using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.Application.Interface;

public interface IBookQuery {
    //Signatur
    //Det er indforstået i et interface at den er public
    BookDto Get(int id);
    IEnumerable<BookDto> GetAll();
}
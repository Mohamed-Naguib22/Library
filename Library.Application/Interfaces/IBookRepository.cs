using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookById(int bookId);
    }
}

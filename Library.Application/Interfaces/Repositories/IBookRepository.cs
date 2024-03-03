using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<Book> GetBookById(int bookId);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> SearchAsync(string query);
        Task<IEnumerable<Book>> GetGenreBooksAsync(int genreId);
        IQueryable<Book> GetAllBooksQuery();
    }
}

using Library.Application.Interfaces;
using Library.Domain.Models;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Book> GetBookById(int bookId)
        {
            var book = await _context.Books.Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            return book;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = await _context.Books.Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ToListAsync();

            return books;
        }
    }
}

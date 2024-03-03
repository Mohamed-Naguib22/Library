using Library.Application.Interfaces.Repositories;
using Library.Domain.Models;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Book> GetBookById(int bookId) =>
            await _context.Books.Include(b => b.Author).Include(b => b.Ratings)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        
        public async Task<IEnumerable<Book>> GetAllBooksAsync() => await GetAllBooksQuery().ToListAsync();
        
        public async Task<IEnumerable<Book>> SearchAsync(string query) =>
            await GetAllBooksQuery().Where(b => 
                b.Title.ToLower().Contains(query.ToLower()) || b.Author.Name.ToLower().Contains(query.ToLower())).ToListAsync();
        
        public async Task<IEnumerable<Book>> GetGenreBooksAsync(int genreId) => 
            await GetAllBooksQuery().Where(b => b.BookGenres.Any(bg => bg.GenreId == genreId)).ToListAsync();

        public IQueryable<Book> GetAllBooksQuery() =>
           _context.Books.Include(b => b.Author).Include(b => b.Ratings).Include(b => b.BookGenres)
               .ThenInclude(bg => bg.Genre).OrderBy(b => b.Title);
    }
}

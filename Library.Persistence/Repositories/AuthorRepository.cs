using Library.Application.Interfaces.Repositories;
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
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync() =>
            await _context.Authors.Include(a => a.Books).OrderBy(a => a.Name).ToListAsync();

        public async Task<Author> GetAuthorByIdAsync(int authorId) =>
            await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == authorId);
    }
}

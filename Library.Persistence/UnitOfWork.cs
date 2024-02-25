using Library.Application.Interfaces;
using Library.Domain.Models;
using Library.Persistence.Data;
using Library.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBookRepository Books { get; private set; }
        public IBaseRepository<Author> Authors { get; private set; }
        public IBaseRepository<Genre> Genres { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Authors = new BaseRepository<Author>(_context);
            Genres = new BaseRepository<Genre>(_context);
            Books = new BookRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

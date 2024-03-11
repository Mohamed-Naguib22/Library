using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
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
        public IAuthorRepository Authors { get; private set; }
        public IBaseRepository<Genre> Genres { get; private set; }
        public ICartRepository Carts { get; private set; }
        public IBaseRepository<CartItem> CartItems { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IBaseRepository<OrderItem> OrderItems { get; private set; }
        public IWishlistRepository Wishlists { get; private set; }
        public IBaseRepository<WishlistItem> WishlistItems { get; private set; }
        public IBaseRepository<SearchQuery> SearchQueries { get; private set; }
        public IBaseRepository<Rating> Ratings { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Authors = new AuthorRepository(_context);
            Genres = new BaseRepository<Genre>(_context);
            Books = new BookRepository(_context);
            Carts = new CartRepository(_context);
            CartItems = new BaseRepository<CartItem>(_context);
            Orders = new OrderRepository(_context);
            OrderItems = new BaseRepository<OrderItem>(_context);
            Wishlists = new WishlistRepository(_context);
            WishlistItems = new BaseRepository<WishlistItem>(_context);
            SearchQueries = new BaseRepository<SearchQuery>(_context);
            Ratings = new BaseRepository<Rating>(_context);
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

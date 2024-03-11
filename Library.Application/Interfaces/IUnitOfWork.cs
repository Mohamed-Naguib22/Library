using Library.Application.Interfaces.Repositories;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IBaseRepository<Genre> Genres { get; }
        ICartRepository Carts { get; }
        IBaseRepository<CartItem> CartItems { get; }
        IOrderRepository Orders { get; }
        IBaseRepository<OrderItem> OrderItems { get; }
        IWishlistRepository Wishlists { get; }
        IBaseRepository<WishlistItem> WishlistItems { get; }
        IBaseRepository<SearchQuery> SearchQueries { get; }
        IBaseRepository<Rating> Ratings { get; }
        Task<int> CompleteAsync();
    }
}

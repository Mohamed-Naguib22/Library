using Library.Application.Dtos.CartDtos;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Models;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Cart> GatCartAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems).ThenInclude(ci => ci.Book)
                .SingleOrDefaultAsync(c => c.ApplicationUserId == userId);
        }

        public async Task<CartItem> GetCartItem(int bookId, int cartId)
        {
             return await _context.CartItems
                .Include(ci => ci.Book).FirstOrDefaultAsync(ci => ci.BookId == bookId && ci.CartId == cartId);
        }
    }
}

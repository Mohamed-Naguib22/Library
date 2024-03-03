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
    public class WishlistRepository : BaseRepository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Wishlist> GetWishlistAsync(string userId)
        {
            return await _context.Wishlists
                .Include(c => c.WishlistItems).ThenInclude(ci => ci.Book)
                .SingleOrDefaultAsync(c => c.ApplicationUserId == userId);
        }
    }
}

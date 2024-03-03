using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Repositories
{
    public interface IWishlistRepository : IBaseRepository<Wishlist>
    {
        Task<Wishlist> GetWishlistAsync(string userId);
    }
}

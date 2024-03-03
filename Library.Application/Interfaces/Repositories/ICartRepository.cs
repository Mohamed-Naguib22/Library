﻿using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart> GatCartAsync(string userId);
        Task<CartItem> GetCartItem(int bookId, int cartId);
    }
}

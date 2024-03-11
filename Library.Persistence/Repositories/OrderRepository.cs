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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string userId) =>
            await _context.Order.Include(o => o.OrderItems).ThenInclude(oi => oi.Book)
                .Where(o => o.ApplicationUserId == userId && o.IsPaid).ToListAsync();
    }
}

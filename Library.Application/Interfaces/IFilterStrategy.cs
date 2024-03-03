using Library.Application.Dtos.BookDtos;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IFilterStrategy<TEntity, TFilterDto>
    {
        bool CanApply(TFilterDto filter);
        IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query);
    }
}

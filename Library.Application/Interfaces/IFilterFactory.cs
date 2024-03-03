using Library.Application.Dtos.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IFilterFactory<TEntity, TFilterDto>
    {
        IEnumerable<IFilterStrategy<TEntity, TFilterDto>> CreateFilterStrategies(TFilterDto filter);
    }
}

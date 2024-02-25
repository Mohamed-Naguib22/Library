﻿using Library.Domain.Models;
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
        IBaseRepository<Author> Authors { get; }
        IBaseRepository<Genre> Genres { get; }
        Task<int> CompleteAsync();
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IImageHandler
    {
        IImageHandler SetNext(IImageHandler handler);
        bool Handle(IFormFile imageFile);
    }
}

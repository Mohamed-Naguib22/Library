using Library.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.ImageHandlers
{
    public class BaseImageHandler : IImageHandler
    {
        private IImageHandler _nextHandler;

        public IImageHandler SetNext(IImageHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual bool Handle(IFormFile imageFile)
        {
            if (_nextHandler != null)
                return _nextHandler.Handle(imageFile);

            else
                return false;
        }
    }
}

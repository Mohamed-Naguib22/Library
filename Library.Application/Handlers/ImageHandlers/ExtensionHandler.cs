using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.ImageHandlers
{
    public class ExtensionHandler : BaseImageHandler
    {
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".png", ".jpeg" };

        public override bool Handle(IFormFile imageFile)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(imageFile.FileName).ToLower()))
            {
                return base.Handle(imageFile);
            }

            else
            {
                return false;
            }
        }
    }
}

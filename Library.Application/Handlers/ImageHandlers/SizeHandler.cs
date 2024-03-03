using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.ImageHandlers
{
    public class SizeHandler : BaseImageHandler
    {
        private const long MAX_SIZE = 1048576;

        public override bool Handle(IFormFile imageFile)
        {
            if (imageFile.Length > MAX_SIZE)
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

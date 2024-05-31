using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShopWeb.Core.src.Entity
{
    public class ImageUploadModel
    {
        public IFormFile Image { get; set; }
    }
}
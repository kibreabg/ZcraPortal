using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZcraPortal.Model
{
    public class UploadFile
    {
        public IFormFile file { get; set; }

        public string gtype { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.ImageData
{
    public class ImageInputModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Stream Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.ImageData
{
    public class ProductImageDto
    {
        public int ProductId { get; set; }
        public string Folder { get; set; }
        public Guid Id { get; set; }
    }
}

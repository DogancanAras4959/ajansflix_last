using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("productImages")]
    public class ProductImages
    {
        public int ProductId { get; set; }
        public string Folder { get; set; }
        public Guid Id { get; set; }
    }
}

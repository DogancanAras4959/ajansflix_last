using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("products")]
    public class Products : BaseEntity, IEntity
    {
        public Products()
        {
            productDetails = new List<ProductDetails>();
            quantityComponents = new List<QuantityComponent>();
            videosdata = new List<VideosData>();
        }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductMetaName { get; set; }
        public string ProductMetaDescription { get; set; }
        public string ProductAlternateDesc { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Banner { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public Categories category { get; set; }
        public virtual ICollection<ProductDetails> productDetails { get; set; }
        public virtual ICollection<QuantityComponent> quantityComponents { get; set; }
        public virtual ICollection<VideosData> videosdata { get; set; }


    }
}

using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("favoriteProducts")]
    public class FavoriteProducts : BaseEntity
    {
        public FavoriteProducts()
        {

        }

        [ForeignKey("product")]
        public int ProductId { get; set; }

        [ForeignKey("customer")]
        public int CustomerId { get; set; }

        public Products product { get; set; }
        public Customers customer { get; set; }
    }
}

using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("quantityComponent")]
    public class QuantityComponent : BaseEntity, IEntity
    {
        public QuantityComponent()
        {

        }

        public int CompQuantity { get; set; }
        public int CompPrice { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public DateTime CompDeadLine { get; set; }

        public Products product { get; set; }
    }
}

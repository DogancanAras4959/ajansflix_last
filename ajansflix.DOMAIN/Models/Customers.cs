using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("customers")]
    public class Customers : BaseEntity
    {
        public Customers()
        {
            orderList = new List<Orders>();
        }

        public string NameSurname { get; set; }
        public string Messagess { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
     
        public ICollection<Orders> orderList { get; set; }
    }
}

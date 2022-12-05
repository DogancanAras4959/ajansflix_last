using ajansflix.CORE.CartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.CORE.EmailConfig
{
    public class CustomerBook
    {
        public CustomerBook()
        {
            cartResult = new List<CartItem>();
        }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public decimal Total  { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<CartItem> cartResult { get; set; }
    }
}

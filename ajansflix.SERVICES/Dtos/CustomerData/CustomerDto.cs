using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.CustomerData
{
    public class CustomerDto : BaseEntityDto
    {
        public string NameSurname { get; set; }
        public string Messagess { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Company { get; set; }

    }
}

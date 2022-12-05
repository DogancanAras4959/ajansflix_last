using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.CustomerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface ICustomerService : ICrudService<Customers, CustomerDto>
    {
        List<CustomerDto> GetAllAppoinments();
        int InsertCustomer(CustomerDto model);
        //CustomerDto getCustomer(string username);
        //bool logIn(string username, string password);
    }
}

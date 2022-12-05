using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.CustomerData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class CustomerService : CrudService<Customers, CustomerDto>, ICustomerService
    {
        public CustomerService(IRepository<Customers> repository, IMapper mapper) : base(repository, mapper)
        {
                
        }

        public List<CustomerDto> GetAllAppoinments()
        {
            var entities = _repository.Where(x => x.IsActive == false).ToList();
            var entityDtos = _mapper.Map<IList<Customers>, List<CustomerDto>>(entities);
            return entityDtos;
        }

        //public CustomerDto getCustomer(string username)
        //{
        //    var entities = _repository.Where(x => x.UserName == username).SingleOrDefault();
        //    var entityDto = _mapper.Map<Customers, CustomerDto>(entities);
        //    return entityDto;
        //}

        public override List<CustomerDto> GetAll()
        {
            var entities = _repository.Where(x => x.IsActive == true).ToList();
            var entityDtos = _mapper.Map<IList<Customers>, List<CustomerDto>>(entities);
            return entityDtos;
        }

        public int InsertCustomer(CustomerDto model)
        {
            var entity = _mapper.Map<CustomerDto, Customers>(model);
            _repository.Add(entity);
            _repository.Save();
            model.Id = entity.Id;
            return model.Id;
        }

        //public bool logIn(string username, string password)
        //{
        //    var entites = _repository.Where(x => x.Password == password && x.UserName == username).SingleOrDefault();
        //    var entityDto = _mapper.Map<Customers, CustomerDto>(entites);

        //    if (entityDto != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}

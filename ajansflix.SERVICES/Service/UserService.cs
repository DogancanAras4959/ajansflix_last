using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.UserData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class UserService : CrudService<Users, UserDto>, IUserService
    {
        public UserService(IRepository<Users> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public override List<UserDto> GetAll()
        {
            var entities = _repository.Include("role").ToList();
            var entityDtos = _mapper.Map<IList<Users>, List<UserDto>>(entities);
            return entityDtos;
        }

        public UserDto getUserByName(string userName)
        {
            var entities = _repository.Where(x => x.UserName == userName).SingleOrDefault();
            var entityDto = _mapper.Map<Users, UserDto>(entities);
            return entityDto;
        }

        public bool login(string username, string password)
        {
            var entites = _repository.Where(x => x.Password == password && x.UserName == username).SingleOrDefault();
            var entityDto = _mapper.Map<Users, UserDto>(entites);

            if (entityDto != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

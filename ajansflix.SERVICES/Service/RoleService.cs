using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.RoleData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class RoleService : CrudService<Roles, RoleDto>, IRoleService
    {
        public RoleService(IRepository<Roles> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

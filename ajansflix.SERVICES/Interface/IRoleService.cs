using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.RoleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IRoleService : ICrudService<Roles, RoleDto>
    {
    }
}

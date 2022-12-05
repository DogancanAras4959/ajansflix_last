using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.QuantityCompData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class QuantityComponentService : CrudService<QuantityComponent, QuantityComponentDto>, IQuantityComponentService
    {
        public QuantityComponentService(IRepository<QuantityComponent> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}

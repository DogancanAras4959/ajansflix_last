using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.DetailData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IDetailService : ICrudService<Details, DetailDto>
    {
        DetailDto detailGet(string name);
        int InsertDetailById(DetailDto model);
    }
}

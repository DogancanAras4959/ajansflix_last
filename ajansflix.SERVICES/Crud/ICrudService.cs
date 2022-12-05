using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Crud
{
    public interface ICrudService<TEntity, TEntityDto>
    {
        TEntityDto Insert(TEntityDto entityDto);
        TEntityDto Update(TEntityDto entityDto);
        void Delete(int id);
        TEntityDto Get(int id);
        List<TEntityDto> GetAll();
    }
}

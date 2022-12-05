using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.DetailData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class DetailService : CrudService<Details, DetailDto>, IDetailService
    {
        public DetailService(IRepository<Details> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public DetailDto detailGet(string name)
        {
            var entity = _repository.Where(x=> x.DetailName == name).SingleOrDefault();
            var entityDto = _mapper.Map<Details, DetailDto>(entity);
            return entityDto;
        }

        public int InsertDetailById(DetailDto model)
        {
            var entity = _mapper.Map<DetailDto, Details>(model);
            _repository.Add(entity);
            _repository.Save();
            model.Id = entity.Id;
            return model.Id;
        }
    }
}

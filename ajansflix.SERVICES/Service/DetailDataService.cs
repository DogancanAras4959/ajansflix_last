using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.DetailDataData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class DetailDataService : CrudService<DetailsData, DetailDataDto>, IDetailDataService
    {
        public DetailDataService(IRepository<DetailsData> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<DetailDataDto> getData(int Id)
        {
            var entity = _repository.Where(x => x.DetailId == Id && x.IsActive == true).OrderByDescending(x => x.CreatedTime).AsNoTracking().Include("detail").ToList();
            var entityDto = _mapper.Map<List<DetailsData>, List<DetailDataDto>>(entity);
            return entityDto;
        }

        public List<DetailDataDto> getDataAdmin(int Id)
        {
            var entity = _repository.Where(x => x.DetailId == Id).OrderByDescending(x => x.CreatedTime).AsNoTracking().Include("detail").ToList();
            var entityDto = _mapper.Map<List<DetailsData>, List<DetailDataDto>>(entity);
            return entityDto;
        }

        public DetailDataDto getDataObject(int Id)
        {
            var entity = _repository.Where(x => x.Id == Id && x.IsActive == true).AsNoTracking().Include("detail").SingleOrDefault();
            var entityDto = _mapper.Map<DetailsData, DetailDataDto>(entity);
            return entityDto;
        }
    }
}

using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.DetailDataData;
using ajansflix.SERVICES.Service;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IDetailDataService : ICrudService<DetailsData, DetailDataDto>
    {
        List<DetailDataDto> getData(int Id);
        List<DetailDataDto> getDataAdmin(int Id);

        DetailDataDto getDataObject(int Id);

    }
}

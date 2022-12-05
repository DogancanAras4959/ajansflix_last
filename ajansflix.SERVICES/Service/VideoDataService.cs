using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.ProductsData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class VideoDataService : CrudService<VideosData,VideoDataDto>, IVideoDataService
    {
        public VideoDataService(IRepository<VideosData> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}

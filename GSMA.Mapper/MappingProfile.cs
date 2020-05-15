using AutoMapper;
using GSMA.DataProvider.Data;
using GSMA.Models;
using System.Collections.Generic;
using System.Linq;

namespace GSMA.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Egmdetails, EGMDetailModel>();
            CreateMap<EGMDetailModel, Egmdetails>();
            CreateMap<List<Egmdetails>, List<EGMDetailModel>>();
            CreateMap<List<EGMDetailModel>, List<Egmdetails>>();

            CreateMap<Egmseals, EGMSealModel>();
            CreateMap<EGMSealModel, Egmseals>();
            CreateMap<List<EGMSealModel>, List<Egmseals>>();
            CreateMap<List<Egmseals>, List<EGMSealModel>>();
        }

        //public static List<TDestination> MapList<TSource, TDestination>(this IMapper mapper, List<TSource> source)
        //{
        //    return source.Select(x => mapper.Map<TDestination>(x)).ToList();
        //}
    }
}

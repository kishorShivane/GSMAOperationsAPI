using AutoMapper;
using GSMA.DataProvider.Data;
using GSMA.Models;
using System.Collections.Generic;

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

            CreateMap<SealDetails, SealDetailModel>();
            CreateMap<SealDetailModel, SealDetails>();
            CreateMap<List<SealDetailModel>, List<SealDetails>>();
            CreateMap<List<SealDetails>, List<SealDetailModel>>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<List<UserModel>, List<User>>();
            CreateMap<List<User>, List<UserModel>>();

            CreateMap<UserType, UserTypeModel>();
            CreateMap<UserTypeModel, UserType>();
            CreateMap<List<UserTypeModel>, List<UserType>>();
            CreateMap<List<UserType>, List<UserTypeModel>>();

        }

        //public static List<TDestination> MapList<TSource, TDestination>(this IMapper mapper, List<TSource> source)
        //{
        //    return source.Select(x => mapper.Map<TDestination>(x)).ToList();
        //}
    }
}

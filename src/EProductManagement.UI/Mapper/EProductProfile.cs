using AutoMapper;
using EProductManagement.Domain.Entities;
using EProductManagement.UI.Model;

namespace EProductManagement.UI.Mapper
{
    public class EProductProfile : Profile
    {
        public EProductProfile()
        {
            CreateMap<EProduct, EProductModel>()
                .ReverseMap();
            CreateMap<EProduct, EProductForCategoryModel>()
                .ReverseMap();
        }
    }
}

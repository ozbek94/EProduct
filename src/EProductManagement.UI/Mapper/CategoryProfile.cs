using AutoMapper;
using EProductManagement.Domain.Entities;
using EProductManagement.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreationModel, Category>()
                .ReverseMap();
            CreateMap<CategoryModel, Category>()
                .ReverseMap();
        }
    }
}

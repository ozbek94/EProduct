using AutoMapper;
using EProductManagement.Domain.Entities;
using EProductManagement.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Mapper
{
    public class StockTransactionProfile : Profile
    {
        public StockTransactionProfile()
        {
            CreateMap<StockTransaction, StockTransactionModel>()
                .ReverseMap();
            CreateMap<StockTransaction, StockTransactionToUserModel>()
                .ReverseMap();
        }

    }
}

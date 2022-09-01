using EProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EProductManagement.Data.Configs
{
    public class ProductBalanceConfig : IEntityTypeConfiguration<ProductBalance>
    {
        public void Configure(EntityTypeBuilder<ProductBalance> builder)
        {
            builder.HasQueryFilter(x => x.DeleteTime == null);
        }
    }
}

using EProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EProductManagement.Data.Configs
{
    public class EProductConfig : IEntityTypeConfiguration<EProduct>
    {
        public void Configure(EntityTypeBuilder<EProduct> builder)
        {
            builder.HasQueryFilter(e => e.DeleteTime == null);
        }
    }
}

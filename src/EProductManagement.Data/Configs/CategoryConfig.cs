using EProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EProductManagement.Data.Configs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.HasQueryFilter(e => e.DeleteTime == null);
                builder.HasQueryFilter(x => x.UpperCategoryId == null);
            }
    }
}

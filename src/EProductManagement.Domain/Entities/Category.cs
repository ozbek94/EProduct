using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProductManagement.Domain.Entities
{
    [Table("Category")]
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<EProduct> EProducts { get; set; }
        public int? UpperCategoryId { get; set; }
        public virtual Category UpperCategory { get; set; }

    }
}

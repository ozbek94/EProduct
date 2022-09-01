using EProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.UI.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UpperCategoryId { get; set; }
        public List<EProductForCategoryModel> EProducts { get; set; }
    }
}

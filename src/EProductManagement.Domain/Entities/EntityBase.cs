using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProductManagement.Domain.Entities
{
    public class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public EntityBase()
        {
            InsertTime = DateTime.Now;
        }
    }
}

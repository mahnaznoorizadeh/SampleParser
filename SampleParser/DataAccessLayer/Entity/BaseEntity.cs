using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleParser.DataAccessLayer.Entity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            InsertDateTime = DateTime.Now;
        }

        [Required]
        public int Id { get; set; }
        public DateTime InsertDateTime { get; set; }
        
        [DefaultValue(0)]
        public bool IsDeleted { get; set; }
        public string Comment { get; set; }
    }
}
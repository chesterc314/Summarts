using System;
using System.ComponentModel.DataAnnotations;
namespace SummArts.Models
{
    public abstract class Entity<IdType>
    {
        public IdType Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }
    }
}
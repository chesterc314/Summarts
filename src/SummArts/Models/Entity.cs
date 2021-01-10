using System;
using System.ComponentModel;

namespace SummArts.Models
{
    public abstract class Entity<IdType>
    {
        public IdType Id { get; set; }
        [DisplayName("Created Date(UTC)")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Updated Date(UTC)")]
        public DateTime UpdatedDate { get; set; }
    }
}
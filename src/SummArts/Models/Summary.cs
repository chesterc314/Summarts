using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SummArts.Models
{
    public class Summary : Entity<int>
    {
        [Required]
        public string Title { get; set; }
        [DisplayName("Summary")]
        public string SummaryText { get; set; }
        [DisplayName("Raw Text")]
        public string RawText { get; set; }
        [DisplayName("Source Url")]
        [Url]
        public string SourceUrl { get; set; }
        public Category Category { get; set; }
    }
}
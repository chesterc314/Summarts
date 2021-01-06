using System.ComponentModel;

namespace SummArts.Models
{
    public class Summary : Entity<int>
    {
        public string Title { get; set; }
        [DisplayName("Summary")]
        public string SummaryText { get; set; }
        [DisplayName("Source Url")]
        public string SourceUrl { get; set; }
        public Category Category { get; set; }
    }
}
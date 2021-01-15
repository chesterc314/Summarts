using Cassandra.Mapping;
using SummArts.Models;

namespace Persistence.DataStaxAstraMappings
{
    public class SummaryMappings : Mappings
    {
        public SummaryMappings()
        {
            For<Summary>()
              .TableName("summary")
              .PartitionKey(u => u.Id)
              .Column(u => u.Id, cm => cm.WithName("summary_id"))
              .Column(u => u.CreatedDate, cm => cm.WithName("created_date"))
              .Column(u => u.UpdatedDate, cm => cm.WithName("updated_date"))
              .Column(u => u.Category, cm => cm.WithName("category").WithDbType<int>())
              .Column(u => u.Sentiment, cm => cm.WithName("sentiment").WithDbType<int>())
              .Column(u => u.Title, cm => cm.WithName("title"))
              .Column(u => u.RawText, cm => cm.WithName("raw_text"))
              .Column(u => u.SummaryText, cm => cm.WithName("summary_text"))
              .Column(u => u.SourceUrl, cm => cm.WithName("source_url"));
        }
    }
}
using System;

namespace SummArts.Helpers 
{
    public interface IDateProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
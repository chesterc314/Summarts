using System;

namespace SummArts.Helpers 
{
    public class DateProvider : IDateProvider
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
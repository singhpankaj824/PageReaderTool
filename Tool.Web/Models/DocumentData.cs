using System.Collections.Generic;

namespace Tool.Web.Models
{
    public class DocumentData
    {
        public IEnumerable<string> ImageURL { get; set; }
        public int TotalNumberOfWords { get; set; }
        public IEnumerable<WordCount> TopOccurringWords { get; set; }
    }

    public class WordCount
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
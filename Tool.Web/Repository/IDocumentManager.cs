using HtmlAgilityPack;
using System.Collections.Generic;
using Tool.Web.Models;

namespace Tool.Web.Repository
{
    public interface IDocumentManager
    {
        void LoadDocument(string URL);
        IEnumerable<string> GetAttributeValueOfElement(string elementName, string attribureName);
        IEnumerable<HtmlNode> GetElement(string elementName);
        int TotalNumberOfWord(bool excludeIntegers = true);
        IEnumerable<WordCount> Search(int topCount = 10);

    }
}

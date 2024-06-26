﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Tool.Web.Models;

namespace Tool.Web.Repository
{
    public class DocumentManager : IDocumentManager
    {
        private HtmlDocument document = null;
        private List<string> words { get; set; } = new List<string>();
        public static string domainName { get; private set; }
        /// <summary>
        /// Load webpage as HTML document in memory
        /// </summary>
        /// <param name="URL"></param>
        public void LoadDocument(string URL)
        {
            document = new HtmlDocument();
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(URL);
                DocumentManager.domainName = URL;
                document.LoadHtml(htmlCode);
            }
        }
        /// <summary>
        /// Get element from HTML document using element name
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public IEnumerable<HtmlNode> GetElement(string elementName)
        {
            var htmlNode = document.DocumentNode.Descendants(elementName);
            return htmlNode;
        }
        /// <summary>
        /// Get Attribute of specific elements from HTML document
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="attribureName"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAttributeValueOfElement(string elementName, string attribureName)
        {
            var urls = document.DocumentNode.Descendants(elementName).Select(element => element.GetAttributeValue(attribureName, null)).Where(attributeVal => !string.IsNullOrEmpty(attributeVal));
            return urls;
        }
        /// <summary>
        /// Get total number of words on webpage
        /// </summary>
        /// <param name="excludeIntegers"></param>
        /// <returns></returns>
        public int TotalNumberOfWord(bool excludeIntegers = true)
        {
            GetAllWords(excludeIntegers, true);
            return words.Count;

        }

        private void GetAllWords(bool excludeIntegers, bool includeMeta = false)
        {
            var rootDocument = document.DocumentNode.SelectNodes("//body//text()[not(parent::script)]");
            if (includeMeta)
            {
                rootDocument = document.DocumentNode.SelectNodes("//text()[not(parent::script)]");
            }

            foreach (var innerText in rootDocument.Select(c => c.InnerText))
            {
                if (string.IsNullOrEmpty(innerText))
                {
                    continue;
                }
                var text = HttpUtility.HtmlDecode(innerText);
                if (excludeIntegers)
                {
                    text = Regex.Replace(innerText, @"[^a-zA-Z]+", " ");
                }

                words.AddRange(text.Split(new[] { ' ', '\r', '\n', '.', '/', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(x => !string.IsNullOrWhiteSpace(x)));
            }
        }
        /// <summary>
        /// Search top 10 occurring words from HTML Document
        /// </summary>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public IEnumerable<WordCount> Search(int topCount = 10)
        {
            if (words.Count == 0)
            {
                GetAllWords(true);
            }
            return words.GroupBy(x => x).Select(y => new WordCount { Count = y.Count(), Name = y.Key }).OrderByDescending(t => t.Count).Take(topCount);
        }

    }
}
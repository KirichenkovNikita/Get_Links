using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace GetLinks.Classes
{
    class Worker
    {
        private HtmlWeb hw;
        private HtmlDocument doc;

        public Worker()
        {
            hw = new HtmlWeb();
        }

        public void SetUrl(string Url)
        {
            doc = hw.Load(Url);
        }

        public List<string> GetLinks()
        {
            List<string> Links = new List<string>();

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                Links.Add(link.Attributes["href"].Value);
            }

            return Links;
        }

    }
}

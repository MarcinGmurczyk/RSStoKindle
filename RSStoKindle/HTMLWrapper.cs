using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSStoKindle
{
    public class WrapperHTML
    {
        private HtmlDocument _originalHTML;

        public string Path { get; private set; }

        public string HTMLString
        {
            get { return HtmlCode.DocumentNode.OuterHtml; }
        }

        public HtmlDocument HtmlCode { get; private set; }

        public string Text { get; private set; }

        public WrapperHTML(string path)
        {
            Path = path;
        }

        public void RetrieveHtml()
        {
            HtmlWeb web = new HtmlWeb();
            _originalHTML = HtmlCode = web.Load(Path);
        }

        public bool SaveHTML(Uri path)
        {
            try
            {
                if (File.Exists(path.LocalPath))
                {
                    File.Delete(path.LocalPath);
                }
                HtmlCode.Save(path.LocalPath, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// remove links and comments also
        /// </summary>
        public void CleanHTML()
        {
            HtmlCode.DocumentNode.Descendants()
                .Where(n => n.Name == "script" || n.Name == "#comment")
                .ToList()
                .ForEach(n => n.Remove());

            foreach (HtmlNode link in HtmlCode.DocumentNode.SelectNodes("//a"))
            {
                HtmlNode text = HtmlCode.CreateElement("p");
                text.InnerHtml = link.InnerHtml;
                link.ParentNode.ReplaceChild(text, link);
            }
        }

        public void RemoveEmptyDivs()
        {
            HtmlCode.DocumentNode.Descendants("div")
                .Where(n => n.InnerHtml.Trim() == string.Empty)
                .ToList()
                .ForEach(n => n.Remove());
        }

        public bool RemoveElement(string xpath)
        {
            var node = HtmlCode.DocumentNode.SelectSingleNode(xpath);
            if (node == null)
            {
                return false;
            }
            else
            {
                var ele = node.ParentNode;
                ele.RemoveChild(node);
            }
            return true;
        }

        public bool RemoveAllExceptThis(string xpath)
        {
            var node = HtmlCode.DocumentNode.SelectSingleNode(xpath);
            if (node == null)
            {
                return false;
            }
            else
            {
                HtmlCode = new HtmlDocument();
                var htmlTag = HtmlCode.CreateElement("html");
                var htmlHead = HtmlCode.CreateElement("head");
                var htmlEncoding = HtmlCode.CreateElement("meta");
                htmlEncoding.SetAttributeValue("http-equiv", "Content-Type");
                htmlEncoding.SetAttributeValue("content", "text/html; charset=utf-8");
                var htmlBody = HtmlCode.CreateElement("body");

                htmlHead.AppendChild(htmlEncoding);
                htmlTag.AppendChild(htmlHead);
                htmlTag.AppendChild(htmlBody);
                htmlBody.AppendChild(node);
                HtmlCode.DocumentNode.AppendChild(htmlTag);
            }
            return true;
        }
    }
}
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
                HtmlCode.Save(path.LocalPath);
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
        public void SanitazeHTML()
        {
            var a = HtmlCode.DocumentNode.Descendants();

            HtmlCode.DocumentNode.Descendants()
                .Where(n => n.Name == "script" || n.Name == "#comment")
                .ToList()
                .ForEach(n => n.Remove());

            //foreach (HtmlNode link in HtmlCode.DocumentNode.SelectNodes("//a"))
            //{
            //    HtmlNode text = HtmlCode.CreateElement("p");
            //    link.ParentNode.ReplaceChild(text, link);
            //}


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
                HtmlCode.DocumentNode.AppendChild(node);
            }
            return true;
        }
    }
}
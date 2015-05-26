using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

        public HtmlDocument HtmlCode { get; set; }

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

        public void ResetHTML()
        {
            var str = _originalHTML.DocumentNode.OuterHtml;
            var originalHTML = new HtmlAgilityPack.HtmlDocument();
            originalHTML.LoadHtml(str);
            HtmlCode = originalHTML;            
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
                System.Windows.Forms.MessageBox.Show("Nie udało się zapisać pliku");
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

        private CutHTMLInfo CheckForNotRemovedElements()
        {
            var host = new Uri(Path).Host;

            var notRemovedElements = HtmlCode.DocumentNode.Descendants().ToList();
            var list = new List<HtmlAttributeCollection>();

            foreach (var item in notRemovedElements)
            {
                list.Add(item.Attributes);
            }

            return new CutHTMLInfo(host, list);
        }

        //public void RemoveEmptyDivs()
        //{
        //    HtmlCode.DocumentNode.Descendants("div")
        //        .Where(n => n.InnerHtml.Trim() == string.Empty)
        //        .ToList()
        //        .ForEach(n => n.Remove());
        //}

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

        public void SaveCutHTMLInfo()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Directory.GetCurrentDirectory() + "\\documents\\" + new Uri(Path).Host + new Uri(Path).Host + 
                                            "cut.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, CheckForNotRemovedElements());
            stream.Close();
        }

        public void LoadAndCutHTMLInfo()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Directory.GetCurrentDirectory() + "\\documents\\" + new Uri(Path).Host + new Uri(Path).Host +
                                            "cut.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            CutHTMLInfo cutInfo = (CutHTMLInfo)formatter.Deserialize(stream);
            stream.Close();

            var doc = new HtmlDocument();
            var str = _originalHTML.DocumentNode.OuterHtml;
            doc.LoadHtml(str);

            doc.DocumentNode.Descendants()
                .ToList()
                .ForEach(n =>
                {
                    foreach (var item in cutInfo.NotRemovedElements)
                    {
                        if (!n.Attributes.Equals(item))
                        {
                            n.Remove();
                        }
                    }                    
                });
        }
    }

    [Serializable]
    public class CutHTMLInfo
    {
        public List<HtmlAttributeCollection> NotRemovedElements { get; set; }
        public string HostName { get; set; }

        public CutHTMLInfo(string hostName, List<HtmlAttributeCollection> notRemovedElements)
        {
            HostName = hostName;
            NotRemovedElements = notRemovedElements;
        }
    }
}
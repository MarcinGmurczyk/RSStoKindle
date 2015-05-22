using Gecko;
using System;
using System.IO;
using System.Windows.Forms;

namespace RSStoKindle
{
    public partial class WebBrowserForm : Form
    {
        public GeckoWebBrowser Browser = new GeckoWebBrowser();
        public WrapperHTML wrapperHTML;
        public Uri WebPath;
        public Uri FilePath;

        public WebBrowserForm(Uri webAddress)
        {
            Xpcom.Initialize(Directory.GetCurrentDirectory() + "\\xulrunner");
            InitializeComponent();
            WebPath = webAddress;
            wrapperHTML = new WrapperHTML(webAddress.AbsoluteUri);
            wrapperHTML.RetrieveHtml();
            wrapperHTML.SanitazeHTML();
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + WebPath.Host + WebPath.LocalPath);
            FilePath = new Uri(Directory.GetCurrentDirectory() + "\\" + WebPath.Host + WebPath.LocalPath + "\\a.html");

            wrapperHTML.SaveHTML(FilePath);

            Browser.Parent = this;
            Browser.Dock = DockStyle.Fill;
            Browser.Navigate(FilePath.LocalPath);
            Browser.DomClick += Browser_DomClick;
            Browser.DomMouseOver += Browser_DomMouseOver;
            Browser.DomMouseOut += Browser_DomMouseOut;
        }

        void Browser_DomMouseOut(object sender, DomMouseEventArgs e)
        {
            e.Target.CastToGeckoElement().RemoveAttribute("style");
        }

        void Browser_DomMouseOver(object sender, DomMouseEventArgs e)
        {
            e.Target.CastToGeckoElement().SetAttribute("style", "background-color:red;");
        }


        private void Browser_DomClick(object sender, DomMouseEventArgs e)
        {
            var a = e.Target.CastToGeckoElement().GetSmallXpath();
            wrapperHTML.RemoveElement(a);
            wrapperHTML.SaveHTML(FilePath);
            Browser.Reload();
        }
    }

    public static class EXten
    {
        public static string GetSmallXpath(this GeckoNode node)
        {
            if (node.NodeType == NodeType.Attribute)
            {
                return String.Format("{0}/@{1}", GetSmallXpath(((GeckoAttribute)node).OwnerDocument), node.LocalName);
            }
            if (node.ParentNode == null)
            {
                return "";
            }
            string elementId = ((GeckoHtmlElement)node).Id;
            if (!String.IsNullOrEmpty(elementId))
            {
                return String.Format("//*[@id=\"{0}\"]", elementId);
            }

            int indexInParent = 1;
            GeckoNode siblingNode = node.PreviousSibling;

            while (siblingNode != null)
            {
                if (siblingNode.LocalName == node.LocalName)
                {
                    indexInParent++;
                }
                siblingNode = siblingNode.PreviousSibling;
            }

            return String.Format("{0}/{1}[{2}]", GetSmallXpath(node.ParentNode), node.LocalName, indexInParent);
        }
    }
}
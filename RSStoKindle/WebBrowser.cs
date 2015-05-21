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
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + WebPath.Host + WebPath.LocalPath);
            FilePath = new Uri(Directory.GetCurrentDirectory() + "\\" + WebPath.Host + WebPath.LocalPath + "\\a.html");

            wrapperHTML.SaveHTML(FilePath);

            Browser.Parent = this;
            Browser.Dock = DockStyle.Fill;
            Browser.Navigate(FilePath.LocalPath);
            Browser.DomClick += Browser_DomClick;
        }

        private void Browser_DomClick(object sender, DomMouseEventArgs e)
        {
            var a = e.Target.CastToGeckoElement().GetSmallXpath();
            wrapperHTML.RemoveElement(a);
            wrapperHTML.SaveHTML(FilePath);
            Browser.Reload();
        }

        private void Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                MessageBox.Show("a");
            }
        }

        private void Browser_MouseHover(object sender, EventArgs e)
        {
            MessageBox.Show("a");
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
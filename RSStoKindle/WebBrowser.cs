using Gecko;
using HtmlAgilityPack;
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

        private GeckoNode _currentElementClasses;

        public WebBrowserForm(Uri webAddress)
        {
            Xpcom.Initialize(Directory.GetCurrentDirectory() + "\\xulrunner");
            InitializeComponent();
            WebPath = webAddress;
            wrapperHTML = new WrapperHTML(webAddress.AbsoluteUri);
            wrapperHTML.RetrieveHtml();
            wrapperHTML.CleanHTML();
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + WebPath.Host + WebPath.LocalPath);
            FilePath = new Uri(Directory.GetCurrentDirectory() + "\\" + WebPath.Host + WebPath.LocalPath + "\\a.html");

            wrapperHTML.SaveHTML(FilePath);

            Browser.Parent = this.panelBrowser;
            Browser.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;
            Browser.Navigate(FilePath.LocalPath);
            this.labelPageHost.Text = webAddress.Host;
            this.button1.Click += button1_Click;
            this.button2.Click += button2_Click;

            Browser.DomClick += Browser_DomClick;
            Browser.DomMouseOver += Browser_DomMouseOver;
            Browser.DomMouseOut += Browser_DomMouseOut;
        }

        void button2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Browser_DomMouseOut(object sender, DomMouseEventArgs e)
        {
            e.Target.CastToGeckoElement().SetAttribute("style", RemoveHighlight());
            _currentElementClasses = null;
        }

        private void Browser_DomMouseOver(object sender, DomMouseEventArgs e)
        {
            _currentElementClasses = e.Target.CastToGeckoElement().Attributes["style"];
            e.Target.CastToGeckoElement().SetAttribute("style", AddHighlight());
        }

        private string AddHighlight()
        {
            if (_currentElementClasses == null)
            {
                return "background-color:red;";
            }
            else
            {

                return _currentElementClasses.NodeValue + "background-color:red;";
            }
        }

        private string RemoveHighlight()
        {
            if (_currentElementClasses == null || _currentElementClasses.NodeValue == "background-color:red;")
            {
                return "";
            }
            else
            {
                return _currentElementClasses.NodeValue.Replace("background-color:red;", string.Empty);
            }
        }


        private void Browser_DomClick(object sender, DomMouseEventArgs e)
        {
            var a = e.Target.CastToGeckoElement().GetSmallXpath();
            if (this.radioButton1.Checked)
            {
                wrapperHTML.RemoveElement(a);
            }
            else
            {
                wrapperHTML.RemoveAllExceptThis(a);
            }            
            wrapperHTML.SaveHTML(FilePath);
            wrapperHTML.RemoveEmptyDivs();
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
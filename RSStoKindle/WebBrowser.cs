using Gecko;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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
        public Stack<HtmlAgilityPack.HtmlDocument> History = new Stack<HtmlAgilityPack.HtmlDocument>();

        private GeckoNode _currentElementClasses;

        public WebBrowserForm(Uri webAddress)
        {
            Xpcom.Initialize(Directory.GetCurrentDirectory() + "\\xulrunner");
            InitializeComponent();
            WebPath = webAddress;
            wrapperHTML = new WrapperHTML(webAddress.AbsoluteUri);
            wrapperHTML.RetrieveHtml();
            wrapperHTML.CleanHTML();
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\documents\\" + WebPath.Host + WebPath.LocalPath);
            FilePath = new Uri(Directory.GetCurrentDirectory() + "\\documents\\" + WebPath.Host + WebPath.LocalPath + "\\a.html");

            wrapperHTML.SaveHTML(FilePath);

            Browser.Parent = this.panelBrowser;
            Browser.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;
            Browser.Navigate(FilePath.LocalPath);
            this.labelPageHost.Text = webAddress.Host;

            Browser.DomClick += Browser_DomClick;
            Browser.DomMouseOver += Browser_DomMouseOver;
            Browser.DomMouseOut += Browser_DomMouseOut;
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
            var str = wrapperHTML.HTMLString;
            var oldHTML = new HtmlAgilityPack.HtmlDocument();
            oldHTML.LoadHtml(str);
            History.Push(oldHTML);
            buttonBack.Enabled = true;
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
            //wrapperHTML.RemoveEmptyDivs();
            Browser.Reload();
        }

        private void buttonDecline_Click(object sender, EventArgs e)
        {
           var result = MessageBox.Show("Czy na pewno chcesz odrzucić edycję dokumentu?", "Odrzuć dokument",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
           switch (result)
           {
               case DialogResult.No:
                   break;
               case DialogResult.Yes:
                   this.Close();
                   break;
           }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            try
            {
                wrapperHTML.HtmlCode = History.Pop();
                wrapperHTML.SaveHTML(FilePath);
                Browser.Reload();
            }
            catch (InvalidOperationException ex) 
            {
                MessageBox.Show("blad kolejki historii");
            }
            finally
            {
                if (History.Count == 0) buttonBack.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser.Reload();
        }

        private void buttonLoadOriginal_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy chcesz załadować niezmodyfikowany plik HTML?", "Alert",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    wrapperHTML.ResetHTML();
                    History.Clear();
                    buttonBack.Enabled = false;
                    Browser.Reload();
                    break;
            }
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
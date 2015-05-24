using System;
using System.Windows.Forms;

namespace RSStoKindle
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var a = new WebBrowserForm(new Uri("http://www.kwantowo.pl/2015/05/22/kb-22-miesiecznica-budzika/"));
            a.Show();
        }
    }
}
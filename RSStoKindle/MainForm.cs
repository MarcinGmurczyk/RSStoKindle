using System;
using System.Windows.Forms;

namespace RSStoKindle
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var a = new WebBrowserForm(new Uri("http://www.kwantowo.pl/2015/05/21/kb-21-nietypowe-glazy-na-komecie-67p/"));
            a.Show();
        }
    }
}
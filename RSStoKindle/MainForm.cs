using System;
using System.Windows.Forms;

namespace RSStoKindle
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var a = new WebBrowserForm(new Uri("http://kosmosbeztajemnic.blogspot.com/2015/05/gwiazdy-neutronowepulsary-i-magnetary.html"));
            a.Show();
        }
    }
}
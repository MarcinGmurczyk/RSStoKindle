using System;
using System.Windows.Forms;

namespace RSStoKindle
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = new WebBrowserForm(new Uri(textBox1.Text));
            a.Show();
        }
    }
}
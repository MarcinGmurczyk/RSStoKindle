using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CodeForm : Form
    {
        public CodeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Access resource by Getter (Recommended as by http://stackoverflow.com/a/14503044/2003325)
            MessageBox.Show(WinFormStrings.errorInsuffMemory);
        }
    }
}

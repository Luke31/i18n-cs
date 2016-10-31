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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Declare a Resource Manager instance. (Include namespace if set)
            ResourceManager LocRM = new ResourceManager("WinForms.WinFormStrings", typeof(Form1).Assembly);
            // Assign the string for the "errorInsuffMemory" key to a message box.
            MessageBox.Show(LocRM.GetString("errorInsuffMemory"));
        }
    }
}

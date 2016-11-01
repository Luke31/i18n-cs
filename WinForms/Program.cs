using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
// C#
using System.Globalization;
namespace WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Uncomment to force specific locale
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP"); //Japanese (Default of this project)
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB"); //English

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CodeForm());
        }
    }
}

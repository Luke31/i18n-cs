using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.CodeDefaultEnglish.MultiLangModule;

namespace Company.CodeDefaultEnglish
{
    class Program
    {
        static void Main(string[] args)
        {
            //Uncomment to force specific locale
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP"); //Japanese (Default of this project)
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB"); //English

            var comp = new MultiLangComponent();
            comp.RunComponent();

            Console.ReadLine();
        }
    }
}

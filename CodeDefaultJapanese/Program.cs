using System;
using System.Globalization;
using System.Threading;
using Company.Code.MultiLangModule;

namespace Company.CodeDefaultJapanese
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

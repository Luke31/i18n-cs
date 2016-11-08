using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPythonCsharp.Api;
using IronPythonCsharp.Executors;

namespace IronPythonCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Executor.SetupPython();

            IPythonApi sample = new PythonApi();
            bool result = sample.AddStore("test");
            Console.WriteLine(result);
        }
    }
}

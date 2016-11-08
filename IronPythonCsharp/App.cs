using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IronPythonCsharp
{
    public class App
    {
        public static readonly string AssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}

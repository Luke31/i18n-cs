using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Runtime;

namespace IronPythonCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = AssemblyDir;
            int exitCode = new Program().MainImpl();
        }

        private int MainImpl()
        {
            InitializeEngine();
            
            try
            {
                runModule();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        private void InitializeEngine()
        {
            //Previous Frames supprt:
            //
            //var lang = Python.CreateLanguageSetup(null);
            //lang.Options["Frames"] = ScriptingRuntimeHelpers.True;
            ////lang.Options["FullFrames"] = ScriptingRuntimeHelpers.True;
            //var setup = new ScriptRuntimeSetup();
            //setup.LanguageSetups.Add(lang);
            //var runtime = new ScriptRuntime(setup);
            //var engine = runtime.GetEngine("py");

            var engine = Python.CreateEngine();

            var scope = engine.CreateScope();

            var src = engine.CreateScriptSourceFromString(String.Format(
@"import clr
import sys
sys.path.append(r'{0}')
clr.AddReference('mscorlib')
clr.AddReference('System.Core')
clr.AddReferenceToFileAndPath('stdipy.dll')
clr.AddReferenceToFileAndPath('stdipyencod.dll')
clr.AddReferenceToFileAndPath('sample.dll')
clr.AddReferenceToFileAndPath('package.dll')
from sample import core", Program.AssemblyDir), SourceCodeKind.Statements);
            src.Execute(scope);
            
            runModule = engine.Operations.GetMember<Action>(scope.GetVariable("core"), "runModule");
        }
        
        private Action runModule;

        public static readonly string AssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    }
}

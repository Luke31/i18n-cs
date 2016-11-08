using System;
using System.Collections.Generic;
using System.Linq;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using log4net;
using log4net.Core;

namespace IronPythonCsharp.Api
{
    /// <summary>
    /// Python 連携
    /// </summary>
    public class PythonApi : IPythonApi
    {
        #region deleagtes デリゲート

        public delegate void Action<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

        #endregion

        #region fields フィールド

        /// ストア追加
        Func<string, bool> _add_store;
        /// ストアのサーバー設定を確認
        Action<string> _check_servers_configuration;

        /// <summary>
        /// ログ出力オブジェクトです。
        /// </summary>
        static ILog _logger = LogManager.GetLogger(typeof(PythonApi));

        #endregion

        #region universal methods 一般メソッド

        /// <summary>
        /// エンジンを初期化します。
        /// </summary>
        public void InitializeEngine()
        {
            _logger.Debug("InitializeEngine start");
           
            // スクリプトエンジンを作る
            var engine = Python.CreateEngine();

            //TODO No raw script path used
            // スクリプトのフォルダを指定する
            //if (StoreConsoleConfig.UseRawScript)
            //{
            //    AddRawScriptPath(engine);
            //}

            AddRawScriptPath(engine);
            // 変数スコープの作成
            var scope = engine.CreateScope();

            // import スクリプトの準備
            ScriptSource[] sources = GetImportSources(engine);

            // import スクリプトを実行する
            _logger.Debug("InitializeEngine execute");
            foreach (var source in sources)
            {
                source.Execute(scope);
            }

            // 関数ポインタの取得
            _logger.Debug("InitializeEngine get pointer");
            SetFunctionPointer(engine, scope);

            _logger.Debug("InitializeEngine end");
        }

        /// <summary>
        /// Python の dict をパースします。
        /// </summary>
        /// <param name="dict">パースするディクショナリ。</param>
        public Dictionary<string, object> ParsePythonDictionary(PythonDictionary dict)
        {
            if (dict == null) { return null; }

            var ret = new Dictionary<string, object>();
            foreach (string key in dict.Keys)
            {
                ret.Add(key, dict.get(key));
            }
            return ret;
        }

        /// <summary>
        /// スクリプトファイルのディレクトリをモジュールの検索パスに追加します。
        /// </summary>
        private static void AddRawScriptPath(ScriptEngine engine)
        {
            var paths = engine.GetSearchPaths();
            var list = paths.ToList();
            //TODO: list.Add(StoreConsoleConfig.ScriptPath);

            engine.SetSearchPaths(list);
        }

        /// <summary>
        /// Python スクリプトをインポートするためのソースコードを取得します。
        /// </summary>
        /// <remarks>通常は zfsipy から import を行いますが、設定ファイルの <c>UseRawScript</c>
        /// が有効な場合は直接スクリプトを import しようとします。</remarks>
        /// <param name="engine">スクリプトエンジン</param>
        /// <returns>ソースコード</returns>
        private static ScriptSource[] GetImportSources(ScriptEngine engine)
        {
            var rawSources = new List<string>();

            rawSources.Add(String.Format(@"import clr
import sys
sys.path.append(r'{0}')
clr.AddReference('mscorlib')
clr.AddReference('System.Core')
clr.AddReferenceToFileAndPath('stdipy.dll')
{1}", App.AssemblyDir, "clr.AddReferenceToFileAndPath('Sample.dll')"));
            rawSources.Add("from sample import core");
            var ret = rawSources.Select(x => engine.CreateScriptSourceFromString(x, SourceCodeKind.Statements));
            return ret.ToArray();
        }

        /// <summary>
        /// python関数の関数ポインタをセットします。
        /// </summary>
        /// <param name="engine">スクリプトエンジン</param>
        /// <param name="scope">スクリプトスコープ</param>
        private void SetFunctionPointer(ScriptEngine engine, ScriptScope scope)
        {
            _add_store = GetPythonFunctionPointer<Func<string, bool>>(engine, scope, "core", "add_store");
            _check_servers_configuration = GetPythonFunctionPointer<Action<string>>(engine, scope, "api", "check_servers_configuration");
        }

        /// <summary>
        /// Python の関数を取得します。
        /// </summary>
        /// <typeparam name="T">関数の型。</typeparam>
        /// <param name="engine">ScriptEngine のインスタンス。</param>
        /// <param name="scope">ScriptScope のインスタンス。</param>
        /// <param name="variableName">Scope の変数名。</param>
        /// <param name="name">関数名。</param>
        /// <returns>取得した関数。</returns>
        private T GetPythonFunctionPointer<T>(ScriptEngine engine, ScriptScope scope, string variableName, string name)
            where T : class
        {
            object ret = null;
            object variable = null;
            if (!scope.TryGetVariable(variableName, out variable))
            {
                _logger.ErrorFormat("Failed to Get Python Variable {0}", variableName);
                return null;
            }
            try
            {
                ret = engine.Operations.GetMember<T>(variable, name);
            }
            catch
            {
                _logger.ErrorFormat("Failed to Get Python Api {0} in {1}", name, variableName);
            }
            return ret as T;
        }

        #endregion

        #region Python関数

        /// <summary>
        /// ストアを追加します。
        /// </summary>
        /// <param name="store">ストア情報。</param>
        /// <returns>成否。</returns>
        public bool AddStore(string store)
        {
            return _add_store(store);
        }

        /// <summary>
        /// ストアのサーバー設定を確認します。
        /// </summary>
        /// <remarks>設定に問題がある場合には例外を発生させます。</remarks>
        /// <param name="store">ストア情報。</param>
        public void CheckServersConfiguration(string store)
        {
            _logger.DebugFormat("[CheckServersConfiguration - Begin] store: {0}",
                store);
            _check_servers_configuration(store);
            _logger.DebugFormat("[CheckServersConfiguration - End]");
        }

        #endregion
    }
}
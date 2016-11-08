using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPythonCsharp.Api;
using log4net;
using log4net.Core;

namespace IronPythonCsharp.Api
{
    /// <summary>
    /// 端末のモードです。
    /// </summary>
    public enum DeviceMode
    {
        Maintenance = 2,
        Test = 1,
        Production = 0
    }

    /// <summary>
    /// ディスクアクセスモードです。
    /// </summary>
    public enum RevisionAccessMode
    {
        Maintenance = 2,
        Test = 1,
        Production = 0
    }

    /// <summary>
    /// IronPython と通信するためのインターフェース
    /// </summary>
    public interface IPythonApi
    {
        /// <summary>
        /// ストアを追加します。
        /// </summary>
        /// <param name="store">ストア情報。</param>
        /// <returns>成否。</returns>
        bool AddStore(string store);

        /// <summary>
        /// ストアのサーバー設定を確認します。
        /// </summary>
        /// <remarks>設定に問題がある場合には例外を発生させます。</remarks>
        /// <param name="store">ストア情報。</param>
        void CheckServersConfiguration(string store);
    }
}

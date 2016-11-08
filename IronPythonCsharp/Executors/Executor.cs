using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using IronPythonCsharp.Api;
using log4net;

namespace IronPythonCsharp.Executors
{
    /// <summary>
    /// Python の各種処理の実行・経過表示を行うためのクラスです。
    /// </summary>
    /// <remarks>
    /// <para>
    /// このクラスは PythonApi へのラッパーとして使用します。
    /// インスタンス化してサブクラスの XxxxExecutor を利用すると、
    /// 処理の開始時、完了時、処理中の状況などがイベントとして発行されます。
    /// </para>
    /// </remarks>
    public class Executor
    {
        #region フィールド
        /// <summary>
        /// ログ出力するオブジェクトです。
        /// </summary>
        protected static ILog _logger = LogManager.GetLogger("Executor");

        /// <summary>
        /// 実行されているスレッドの ID です。
        /// インスタンス化されたときに利用されます。
        /// </summary>
        private int executeThreadId = -1;

        #endregion

        #region プロパティ

        /// <summary>
        /// PythonApiです。
        /// </summary>
        public static IPythonApi Python { get; protected set; }

        /// <summary>
        /// 実行結果を取得します。
        /// </summary>
        public object Result { get; protected set; }

        #endregion

        #region イベント

        /// <summary>
        /// アクションの開始前に呼び出されます。
        /// </summary>
        public event EventHandler<ActionBeginingEventArgs> ActionBegining;

        protected void OnActionBegining(ActionBeginingEventArgs e)
        {
            if (ActionBegining != null)
            {
                ActionBegining(this, e);
            }
        }

        /// <summary>
        /// アクションが終了したときに呼び出されます。
        /// </summary>
        public event EventHandler<ActionFinishedEventArgs> ActionFinished;

        protected virtual void OnActionFinished(ActionFinishedEventArgs e)
        {
            if (ActionFinished != null)
            {
                ActionFinished(this, e);
            }
        }

        #endregion

        #region メソッド

        /// <summary>
        /// Pythonを準備します。
        /// </summary>
        public static void SetupPython()
        {
            if (Python != null)
            {
                throw new InvalidOperationException("setup completed");
            }
            var pythonApi = new PythonApi();
            pythonApi.InitializeEngine();
            Python = pythonApi;
        }

        /// <summary>
        /// 戻り値を持たない関数を同期で実行します。
        /// </summary>
        /// <param name="executer">処理の実体。</param>
        /// <param name="reload">完了時のリロードの設定。</param>
        protected void ExecuteAction(Action executor)
        {
            _logger.Debug("ExecuteAction start.");

            Debug.Assert(executeThreadId == -1, "Executor は一度しか呼べません");

            executeThreadId = Thread.CurrentThread.ManagedThreadId;
            OnActionBegining(new ActionBeginingEventArgs(String.Empty, String.Empty));
            

            try
            {
                executor();
            }
            catch (Exception ex)
            {
                _logger.Error("ExecuteAction に失敗しました", ex);
                throw;
            }
            finally
            {
                //Omitted callback and monitor for demo
            }

            // 完了を通知する
            OnActionFinished(new ActionFinishedEventArgs());

            _logger.Debug("ExecuteLightActionEnd.");
        }

        /// <summary>
        /// 戻り値として T 型の値を返す関数を同期で実行します。
        /// </summary>
        /// <param name="executor">実行する関数。</param>
        /// 
        /// <typeparam name="T">戻り値の型。</typeparam>
        /// <returns>実行結果。</returns>
        protected T ExecuteFunction<T>(Func<T> executor)
        {
            _logger.Debug("ExecuteLightAction start.");

            Debug.Assert(executeThreadId == -1, "Executor は一度しか呼べません");

            executeThreadId = Thread.CurrentThread.ManagedThreadId;
            OnActionBegining(new ActionBeginingEventArgs(String.Empty, String.Empty));
            
            // 実行する
            T ret = default(T);
            try
            {
                ret = executor();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                //Omitted callback and monitor for demo
            }

            // 完了を通知する
            OnActionFinished(new ActionFinishedEventArgs());

            _logger.DebugFormat("ExecuteLightActionEnd: {0}", ret);
            return ret;
        }

        /// <summary>
        /// 実行を行います。サブクラスで実装してください。
        /// </summary>
        public virtual void Execute()
        {
            throw new NotImplementedException("subclass must implement Execute() method");
        }

        #endregion

        #region イベントハンドラ

        /// <summary>
        /// ExecuteFunction および ExecuteAction を実行したスレッドで
        /// ProgressSectionsChange が発生したときの処理です。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void Callback_ProgressSectionsChange(object sender, ProgressSectionsChangeEventArgs e)
        //{
        //    // シングルトンインスタンスの場合、または、
        //    // 現在のスレッド以外で発生したイベントについては何もしない
        //    if (executeThreadId != Thread.CurrentThread.ManagedThreadId)
        //    {
        //        return;
        //    }

        //    // 通知を行う
        //    OnProgressSectionsChange(e);
        //}

        /// <summary>
        /// ExecuteFunction および ExecuteAction を実行したスレッドで
        /// Progress が発生したときの処理です。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void Callback_Progress(object sender, ActionProgressEventArgs e)
        //{
        //    // シングルトンインスタンスの場合、または、
        //    // 現在のスレッド以外で発生したイベントについては何もしない
        //    if (executeThreadId != Thread.CurrentThread.ManagedThreadId)
        //    {
        //        return;
        //    }

        //    // 通知を行う
        //    OnProgress(e);
        //}

        /// <summary>
        /// ExecuteFunction および ExecuteAction を実行したスレッドで
        /// LogManager.Write が発生したときの処理です。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void LogMonitor_Reported(object sender, LogEventArgs e)
        //{
        //    // シングルトンインスタンスの場合、または、
        //    // 現在のスレッド以外で発生したイベントについては何もしない
        //    if (executeThreadId != Thread.CurrentThread.ManagedThreadId)
        //    {
        //        return;
        //    }

        //    // 通知を行う
        //    OnLogReport(e);
        //}

        #endregion

    }

    /// <summary>
    /// アクション開始前イベントのEventArgsです。
    /// </summary>
    public class ActionBeginingEventArgs : EventArgs
    {
        public string ProgressName { get; set; }
        public string Message { get; set; }

        public ActionBeginingEventArgs(string progressName, string message)
        {
            this.ProgressName = progressName;
            this.Message = message;
        }
    }

    /// <summary>
    /// アクション終了イベントのEventArgsです。
    /// </summary>
    public class ActionFinishedEventArgs : EventArgs
    {
    }
}

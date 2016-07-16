using ClipBoardReplacer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClipBoardReplacer
{
    public partial class NotifyIconWrapper : Component
    {
        ClipboardWatcher clipboardWatcher = null;

        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        public NotifyIconWrapper()
        {

            // コンポーネントの初期化
            this.InitializeComponent();

            // コンテキストメニューのイベントを設定
            this.toolStripMenuItem_Open.Click += this.toolStripMenuItem_Open_Click;
            this.toolStripMenuItem_Exit.Click += this.toolStripMenuItem_Exit_Click;
        }

        /// <summary>
        /// コンテナ を指定して NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        /// <param name="container">コンテナ</param>
        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            this.InitializeComponent();
        }
        /// <summary>
        /// コンテキストメニュー "表示" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            // MainWindow を生成、設定画面
            var wnd = new ConfigWindow();
            wnd.Show();

            Console.WriteLine(wnd);

            //クリップボード監視
            this.clipboardWatcher = new ClipboardWatcher(new System.Windows.Interop.WindowInteropHelper(wnd).Handle);
            this.clipboardWatcher.DrawClipboard += clipboardWatcher_DrawClipboard;
        }

        /// <summary>
        /// コンテキストメニュー "終了" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            // 現在のアプリケーションを終了
            Application.Current.Shutdown();
        }

        void clipboardWatcher_DrawClipboard(object sender, System.EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                Console.WriteLine(Clipboard.GetText());
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.clipboardWatcher.Dispose();
        }
    }
}

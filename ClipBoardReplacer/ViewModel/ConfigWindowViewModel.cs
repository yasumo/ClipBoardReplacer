using ClipBoardReplacer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClipBoardReplacer.ViewModel
{
    class ConfigWindowViewModel:BindableBase
    {

        //window
        Window parent = null;

        //クリップボード監視
        ClipboardWatcher clipboardWatcher = null;

        public ConfigWindowViewModel(Window parent)
        {
            this.parent = parent;
            PropertyChanged += chageProperty;
        }

        private void chageProperty(object sender, PropertyChangedEventArgs e)
        {
            var s = (ConfigWindowViewModel)sender;
            if (e.PropertyName == "ClipText")
            {
                changePropertyClip(s,e);
            }
            else if (e.PropertyName == "RegexText")
            {
                changePropertyRegex(s, e);
            }
            else if (e.PropertyName == "ReplaceText")
            {
                changePropertyReplace(s, e);
            }
            else if (e.PropertyName == "TestText")
            {
                changePropertyTest(s, e);
            }
        }


        private string replace(string regex, string replace,string target)
        {
            if (regex == null || target == null || regex.Length==0||target.Length==0)
            {
                return null;
            }
            try
            {
                System.Text.RegularExpressions.Regex r =
                new System.Text.RegularExpressions.Regex(regex);
                return r.Replace(target, replace);
            }
            catch {
                return null;
            }

        }

        private void changePropertyReplace(ConfigWindowViewModel s, PropertyChangedEventArgs e)
        {
            ResultText = this.replace(RegexText, ReplaceText, TestText);
        }

        private void changePropertyTest(ConfigWindowViewModel s, PropertyChangedEventArgs e)
        {
            ResultText = this.replace(RegexText, ReplaceText, TestText);
        }

        private void changePropertyRegex(ConfigWindowViewModel s, PropertyChangedEventArgs e)
        {
            ResultText = this.replace(RegexText, ReplaceText, TestText);
        }

        private void changePropertyClip(ConfigWindowViewModel s, PropertyChangedEventArgs e)
        {
            Clipboard.SetDataObject(this.replace(RegexText, ReplaceText, ClipText), true);
        }

        private void startWatch()
        {
            if (this.clipboardWatcher == null)
            {
                //クリップボード監視
                this.clipboardWatcher = new ClipboardWatcher(new System.Windows.Interop.WindowInteropHelper(parent).Handle);
                this.clipboardWatcher.DrawClipboard += clipboardWatcher_DrawClipboard;
            }
        }

        private void stopWatch()
        {
            if (this.clipboardWatcher != null)
            { 
                this.clipboardWatcher.Dispose();
            }
        }

        void clipboardWatcher_DrawClipboard(object sender, System.EventArgs e)
        {
            if (Clipboard.ContainsText() && !string.Equals(Clipboard.GetText(),this.clipText))
            {
                Console.WriteLine(Clipboard.GetText());
                ClipText = Clipboard.GetText();
            }
        }

        //binding
        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get { return startCommand = startCommand ?? new RelayCommand(startWatch); }
        }

        private RelayCommand stopCommand;
        public RelayCommand StopCommand
        {
            get { return stopCommand = stopCommand ?? new RelayCommand(stopWatch); }
        }

        private string regexText;
        public string RegexText
        {
            get { return regexText; }
            set { this.SetProperty(ref this.regexText, value); }
        }
        private string replaceText;
        public string ReplaceText
        {
            get { return replaceText; }
            set { this.SetProperty(ref this.replaceText, value); }
        }
        private string resultText;
        public string ResultText
        {
            get { return resultText; }
            set { this.SetProperty(ref this.resultText, value); }
        }
        private string testText;
        public string TestText
        {
            get { return testText; }
            set { this.SetProperty(ref this.testText, value); }
        }
        private string clipText;
        public string ClipText
        {
            get { return clipText; }
            set { this.SetProperty(ref this.clipText, value); }
        }

    }
}

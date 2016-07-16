using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipBoardReplacer.ViewModel
{
    class ConfigWindowViewModel:BindableBase
    {
        public ConfigWindowViewModel()
        {
            PropertyChanged += chageProperty;
        }

        private void chageProperty(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        private string regexText;
        public string RegexText
        {
            get { return regexText; }
            set { this.SetProperty(ref this.regexText, value); }
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

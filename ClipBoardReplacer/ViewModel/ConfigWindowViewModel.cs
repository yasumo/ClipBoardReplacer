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
            if (e.PropertyName == "Name")
            {
                Age = "aaa";
            }
            Console.WriteLine(e.PropertyName);
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetProperty(ref this.name, value); }
        }

        private string age;
        public string Age
        {
            get { return age; }
            set { this.SetProperty(ref this.age, value); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp_samples.Presentation
{
    public class MenuItem : NotifyPropertyChanged
    {
        private string _icon;
        private string _label;
        private Type _pageType;


        public string Icon
        {
            get { return _icon; }
            set { Set(ref _icon, value); }
        }

        public char SymbolAsChar {
            get; set;
        }

        public string Label
        {
            get { return _label; }
            set { Set(ref _label, value); }
        }

        public Type PageType
        {
            get { return _pageType; }
            set { Set(ref _pageType, value); }
        }
    }
}

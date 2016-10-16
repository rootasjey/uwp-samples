using uwpsamples.Views;
using uwpsamples.Presentation;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace uwpsamples {
    public sealed partial class Shell : UserControl
    {
        private static string _header;
        public string Header {
            get {
                if (_header == null) {
                    _header = "UWP Samples";
                }
                return _header;
            }
            set {
                if (_header != value) {
                    _header = value;
                }
            }
        }

        public Shell()
        {
            this.InitializeComponent();

            var vm = new ShellViewModel();

            vm.MenuItems.Add(new MenuItem { 
                Icon = "",
                SymbolAsChar = '\uE706',
                Label = "Home",
                PageType = typeof(WelcomePage)
            });
            vm.MenuItems.Add(new MenuItem {
                Icon = "",
                SymbolAsChar = '\uE00B',
                Label = "wincomposition",
                PageType = typeof(WincompositionPage)
            });
            vm.MenuItems.Add(new MenuItem {
                Icon = "",
                SymbolAsChar = '\uE11A',
                Label = "ListView",
                PageType = typeof(Page2)
            });
            vm.MenuItems.Add(new MenuItem {
                Icon = "",
                SymbolAsChar = '\uE2AF',
                Label = "Tasks",
                PageType = typeof(Page3)
            });

            // select the first menu item
            vm.SelectedMenuItem = vm.MenuItems.First();

            this.ViewModel = vm;

            // add entry animations
            var transitions = new TransitionCollection { };
            var transition = new NavigationThemeTransition { };
            transitions.Add(transition);
            this.Frame.ContentTransitions = transitions;
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                if (Frame.SourcePageType != null) {
                    SetPageHeaderTitle(Frame.SourcePageType.Name);
                }
                return Frame;
            }
        }

        /// <summary>
        /// Set the related header when loading to a page
        /// </summary>
        /// <param name="name">page name</param>
        private void SetPageHeaderTitle(string name) {
            switch (name) {
                case "WelcomePage":
                    VisualHeader.Text = "UWP Samples";
                    break;
                case "WincompositionPage":
                    VisualHeader.Text = "WinComposition Samples";
                    break;
                case "ListViewPage":
                    VisualHeader.Text = "ListView Samples";
                    break;
                case "TasksPage":
                    VisualHeader.Text = "Tasks Samples";
                    break;
                default:
                    VisualHeader.Text = name;
                    break;
            }
        }

        /// <summary>
        /// Set a custom header
        /// </summary>
        /// <param name="name">custom header's name</param>
        public void SetHeaderTitle(string name) {
            VisualHeader.Text = name;
        }

        //private async void VisualHeader_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
        //    var listView = App._shell.GetChildOfType<ListViewBase>();

        //    if (listView == null) {
        //        return;
        //    }

        //    await VisualTreeExtensions.ScrollToIndex(listView, 0);
        //}

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using uwp_samples.Views.wincomposition;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace uwp_samples.Views {
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class WincompositionPage : Page {
        public WincompositionPage() {
            this.InitializeComponent();
        }

        private void ParallaxPage_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            Frame.Navigate(typeof(ParallaxPage), null, new DrillInNavigationTransitionInfo());
        }

        private void BlurPage_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            Frame.Navigate(typeof(BlurPage), null, new DrillInNavigationTransitionInfo());
        }
    }
}

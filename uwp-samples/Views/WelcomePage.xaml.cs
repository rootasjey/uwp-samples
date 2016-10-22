using uwp_samples.Views.wincomposition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace uwp_samples.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            this.InitializeComponent();
        }

        private void ParallaxPage_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            Frame.Navigate(typeof(ParallaxPage), null, new DrillInNavigationTransitionInfo());
        }

        private void BlurPage_Click(System.Object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            Frame.Navigate(typeof(BlurPage), null, new DrillInNavigationTransitionInfo());
        }

        private void TwitterIcon_Tapped(System.Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
            Windows.System.Launcher.LaunchUriAsync(new System.Uri("http://www.twitter.com/jeremiecorpinot"));
        }

        private void MediumIcon_Tapped(System.Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
            Windows.System.Launcher.LaunchUriAsync(new System.Uri("https://medium.com/@jeremiecorpinot"));
        }
    }
}

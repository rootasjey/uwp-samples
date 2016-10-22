using System;
using uwp_samples.Controllers;
using uwp_samples.Helpers;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace uwp_samples.Views.wincomposition {
    public sealed partial class ParallaxPage : Page {

        private Visual _backgroundVisual;
        private Compositor _compositor;
        private ScrollViewer _peopleScrollViewer;
        private CompositionPropertySet _peopleScrollerPropertySet;

        private static ListViewController _peopleController;
        public static ListViewController PeopleController {
            get {
                if (_peopleController == null) {
                    _peopleController = new ListViewController();
                }
                return _peopleController;
            }
        }

        public ParallaxPage() {
            this.InitializeComponent();
            PopulatePage();
        }

        private void PopulatePage() {
            PeopleController.LoadData();
            BindCollectionToView();
        }

        private void BindCollectionToView() {
            ListPeople.ItemsSource = ListViewController.RandomPeople;
        }

        private void ListPeople_Loaded(object sender, RoutedEventArgs e) {
            _peopleScrollViewer = ListPeople.GetChildOfType<ScrollViewer>();
            _peopleScrollerPropertySet = ElementCompositionPreview.
                    GetScrollViewerManipulationPropertySet(_peopleScrollViewer);

        }

        private void BackgroundImage_Loaded(object sender, RoutedEventArgs e) {
            _backgroundVisual = ElementCompositionPreview.GetElementVisual(BackgroundImage);
            _compositor = _backgroundVisual.Compositor;

        }

        private void BackgroundImage_ImageOpened(object sender, RoutedEventArgs e) {
            AttachBackgroundParallax();

        }

        private void AttachBackgroundParallax() {
            double backgroundOffset = Math.Round(BackgroundImage.ActualHeight - ParallaxCanvas.ActualHeight);
            string maxOffsetBottomToUp = backgroundOffset.ToString();

            var expression = _compositor.CreateExpressionAnimation();
            expression.Expression = string.Format("Clamp(scroller.Translation.Y * parallaxFactor, -{0}, 999)", maxOffsetBottomToUp);
            expression.SetScalarParameter("parallaxFactor", 0.03f);
            expression.SetReferenceParameter("scroller", _peopleScrollerPropertySet);

            _backgroundVisual.StartAnimation("Offset.Y", expression);
        }
    }
}

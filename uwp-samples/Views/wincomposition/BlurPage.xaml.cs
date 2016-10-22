using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;
using uwp_samples.Controllers;
using uwp_samples.Helpers;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace uwp_samples.Views.wincomposition {
    public sealed partial class BlurPage : Page {
        // WinComposition variables
        private Visual _backgroundVisual;
        private Compositor _compositor;
        private ScrollViewer _peopleScrollViewer;
        private CompositionPropertySet _peopleScrollViewerPropertySet;

        private static ListViewController _peopleController;
        public static ListViewController PeopleController {
            get {
                if (_peopleController == null) {
                    _peopleController = new ListViewController();
                }
                return _peopleController;
            }
        }

        public BlurPage() {
            InitializeComponent();
            PopulatePage();
        }

        private void PopulatePage() {
            PeopleController.LoadData();
            BindCollectionToView();
        }

        private void BindCollectionToView() {
            ListPeople.ItemsSource = ListViewController.RandomPeople;
        }

        private void BackgroundImage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            _backgroundVisual = ElementCompositionPreview.GetElementVisual(BackgroundImage);
            _compositor = _backgroundVisual.Compositor;
        }

        private void BackgroundImage_ImageOpened(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            AttachBlurAnimation();
        }

        private void AttachBlurAnimation() {
            GaussianBlurEffect blurEffect = new GaussianBlurEffect() {
                Name = "Blur",
                BlurAmount = 0.0f,
                BorderMode = EffectBorderMode.Hard,
                Optimization = EffectOptimization.Speed,
                Source = new CompositionEffectSourceParameter("Backdrop")
            };

            var effectFactory = _compositor.CreateEffectFactory(blurEffect, new[] { "Blur.BlurAmount" });
            var effectBrush = effectFactory.CreateBrush();

            var destinationBrush = _compositor.CreateBackdropBrush();
            effectBrush.SetSourceParameter("Backdrop", destinationBrush);

            var blurSprite = _compositor.CreateSpriteVisual();
            blurSprite.Size = new Vector2((float)BackgroundImage.ActualWidth, (float)BackgroundImage.ActualHeight);
            blurSprite.Brush = effectBrush;
            ElementCompositionPreview.SetElementChildVisual(BackgroundImage, blurSprite);

            ExpressionAnimation backgroundBlurAnimation = _compositor.CreateExpressionAnimation(
                "Clamp(-scroller.Translation.Y / 10,0,100)");
            backgroundBlurAnimation.SetReferenceParameter("scroller", _peopleScrollViewerPropertySet);

            blurSprite.Brush.Properties.StartAnimation("Blur.BlurAmount", backgroundBlurAnimation);

        }

        private void ListPeople_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            _peopleScrollViewer = ListPeople.GetChildOfType<ScrollViewer>();
            _peopleScrollViewerPropertySet = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(_peopleScrollViewer);
        }
    }
}

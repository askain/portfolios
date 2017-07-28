using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HDIMSAPP.Controls
{
    public class OptionsPanel : ItemsControl
    {
        ScrollViewer scrollContent;
        public OptionsPanel()
        {
            base.DefaultStyleKey = typeof(OptionsPanel);
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Loaded += new RoutedEventHandler(CustomPanel_Loaded);
        }

        void CustomPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
            var rootElement = VisualTreeHelper.GetChild(this, 0) as Border;
            var winHandle = rootElement.FindName("optPanel") as Border;
            var btnState = rootElement.FindName("btnState") as ToggleButton;
            var btnClose = rootElement.FindName("btnClose") as ToggleButton;
            scrollContent = rootElement.FindName("scrollContent") as ScrollViewer;

            winHandle.MouseMove += new MouseEventHandler(winHandle_MouseMove);
            winHandle.MouseLeftButtonDown += new MouseButtonEventHandler(winHandle_MouseLeftButtonDown);
            winHandle.MouseLeftButtonUp += new MouseButtonEventHandler(winHandle_MouseLeftButtonUp);

            btnState.Click += new RoutedEventHandler(btnState_Click);
            btnClose.Click += new RoutedEventHandler(btnClose_Click);

            this.UpdateLayout();
            GeneralTransform objGeneralTransform = this.TransformToVisual(this.Parent as FrameworkElement);
            Point point = objGeneralTransform.Transform(new Point(0, 0));
            tTranslate.X = point.X;
            tTranslate.Y = point.Y;
            this.Margin = new Thickness(0);
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.RenderTransform = tTranslate;
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        void btnState_Click(object sender, RoutedEventArgs e)
        {
            var btnState = (ToggleButton)sender;
            scrollContent.Visibility = btnState.IsChecked.Value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            btnState.Content = btnState.IsChecked.Value ? "-" : "+";
        }

        #region Moving
        TranslateTransform tTranslate = new TranslateTransform();
        Point borderP;
        Point currentP;
        double maxMarginLeft;
        double maxMarginTop;
        bool dragOn = false;
        private void winHandle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var c = sender as System.Windows.FrameworkElement;
            dragOn = true;
            this.Opacity *= 0.5;
            borderP = e.GetPosition(sender as Border);
            c.CaptureMouse();

            maxMarginLeft = (this.Parent as FrameworkElement).ActualWidth - this.ActualWidth;
            maxMarginTop = (this.Parent as FrameworkElement).ActualHeight - this.ActualHeight;
        }

        private void winHandle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dragOn)
            {
                var c = sender as System.Windows.FrameworkElement;
                this.Opacity = 1;
                c.ReleaseMouseCapture();
                dragOn = false;
            }
        }

        private void winHandle_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragOn)
            {
                currentP = e.GetPosition(sender as Border);
                var x = tTranslate.X + currentP.X - borderP.X;
                var y = tTranslate.Y + currentP.Y - borderP.Y;

                if (x < 0)
                    x = 0;
                if (y < 0)
                    y = 0;
                if (x > maxMarginLeft)
                    x = maxMarginLeft;
                if (y > maxMarginTop)
                    y = maxMarginTop;
                tTranslate.X = x;
                tTranslate.Y = y;
            }
        }
        #endregion

        #region Dependency Properties
        public static DependencyProperty HeaderProperty = DependencyProperty.Register(
         "HeaderText", typeof(string), typeof(OptionsPanel), null);


        public string HeaderText
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

    }
}

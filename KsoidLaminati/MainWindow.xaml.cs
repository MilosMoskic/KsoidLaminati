using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KsoidLaminati
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(CanvasMain).Add(new ResizeAdorner(new Rectangle()));
        }

        UIElement dragObject = null;
        Point offset;

        private void UserCTRL_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = sender as UIElement;
            this.offset = e.GetPosition(this.CanvasMain);
            this.offset.Y -= Canvas.GetTop(this.dragObject);
            this.offset.X -= Canvas.GetLeft(this.dragObject);
            this.CanvasMain.CaptureMouse();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rectangle userCTRL = new Rectangle();
            userCTRL.Fill = Brushes.Blue;
            userCTRL.Width = 100;
            userCTRL.Height = 100;
            Canvas.SetTop(userCTRL, 20);
            Canvas.SetLeft(userCTRL, 20);
            userCTRL.PreviewMouseDown += UserCTRL_PreviewMouseDown;
            CanvasMain.Children.Add(userCTRL);
            AdornerLayer.GetAdornerLayer(CanvasMain).Add(new ResizeAdorner(userCTRL));
        }

        private void CanvasMain_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragObject == null)
                return;
            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetTop(this.dragObject, position.Y - this.offset.Y);
            Canvas.SetLeft(this.dragObject, position.X - this.offset.X);
        }

        private void CanvasMain_PreviewMouseUp(object sender, MouseEventArgs e)
        {
            this.dragObject = null;
            this.CanvasMain.ReleaseMouseCapture();
        }
    }
}

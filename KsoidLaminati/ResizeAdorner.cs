using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KsoidLaminati
{
    public class ResizeAdorner : Adorner
    {
        VisualCollection AdornerVisuals;
        Thumb thumb;
        Rectangle Rec;
        public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            AdornerVisuals = new VisualCollection(this);
            thumb = new Thumb() { Background = Brushes.Coral, Height = 10, Width = 10 };
            Rec = new Rectangle() { Stroke = Brushes.Coral, StrokeThickness = 2, StrokeDashArray = { 3, 2 } };

            thumb.DragDelta += Thumb_DragDelta;

            AdornerVisuals.Add(Rec);
            AdornerVisuals.Add(thumb);
        }
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var ele = (FrameworkElement)AdornedElement;
            ele.Height = ele.Height + e.VerticalChange < 40 ? 40 : ele.Height + e.VerticalChange;

            ele.Width = ele.Width + e.HorizontalChange < 40 ? 40 : ele.Width + e.HorizontalChange;
        }
        protected override Visual GetVisualChild(int index)
        {
            return AdornerVisuals[index];
        }
        protected override int VisualChildrenCount => AdornerVisuals.Count;
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rec.Arrange(new Rect(-2.5, -2.5, AdornedElement.DesiredSize.Width +5, AdornedElement.DesiredSize.Height +5));
            thumb.Arrange(new Rect(AdornedElement.DesiredSize.Width -5, AdornedElement.DesiredSize.Height -5, 10, 10));
            return base.ArrangeOverride(finalSize);
        }

    }
}

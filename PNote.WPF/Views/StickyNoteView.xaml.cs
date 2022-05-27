using PNote.Core;
using PNote.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PNote.Views
{
    public partial class StickyNoteView : UserControl
    {
        private bool _isDragging;
        private Point _lastPos;

        public StickyNoteView()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += StickyNoteView_MouseLeftButtonDown;
            this.MouseLeftButtonUp += StickyNoteView_MouseLeftButtonUp;
            this.MouseMove += StickyNoteView_MouseMove;

            this.CloseButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Canvas)?.Children.Remove(this);
            (this.DataContext as StickyNoteViewModel)?.UnstickNote().Wait();
        }

        private void StickyNoteView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this._isDragging)
                return;

            var parent = this.Parent as Canvas;
            if (parent == null)
                return;

            Point newPoint = Mouse.GetPosition(parent);

            double top = Canvas.GetTop(this);
            double left = Canvas.GetLeft(this);
            double bottom = top + this.ActualHeight;
            double right = left + this.ActualWidth;

            double newTop = top + (newPoint.Y - this._lastPos.Y);
            double newLeft = left + (newPoint.X - this._lastPos.X);
            double newBottom = bottom + (newPoint.Y - this._lastPos.Y);
            double newRight  = right + (newPoint.X - this._lastPos.X);

            if (newRight > parent.ActualWidth)
                newLeft = parent.ActualWidth - this.ActualWidth;

            if (newBottom > parent.ActualHeight)
                newTop = parent.ActualHeight - this.ActualHeight;

            if (newLeft < 0)
                newLeft = 0;

            if (newTop < 0)
                newTop = 0;

            Canvas.SetTop(this, newTop);
            Canvas.SetLeft(this, newLeft);

            this._lastPos = newPoint;
        }

        private void StickyNoteView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this._isDragging = false;
            this.ReleaseMouseCapture();
        }

        private void StickyNoteView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._isDragging = true;

            this._lastPos = Mouse.GetPosition(this.Parent as UIElement);
            this.CaptureMouse();
            foreach (UIElement child in (this.Parent as Canvas)?.Children)
                Canvas.SetZIndex(child, 0);

            Canvas.SetZIndex(this, 1);
        }
    }
}

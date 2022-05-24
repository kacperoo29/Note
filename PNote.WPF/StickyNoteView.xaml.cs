using PNote.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PNote
{
    public partial class StickyNoteView : UserControl
    {
        private Canvas _parent;
        private bool _isDragging;
        private Point _lastPos;

        public Note Note { get; set; }

        public StickyNoteView(Note note, Canvas parent)
        {
            InitializeComponent();

            this.Note = note;
            this._parent = parent;
            this.DataContext = this.Note;

            this.MouseLeftButtonDown += StickyNoteView_MouseLeftButtonDown;
            this.MouseLeftButtonUp += StickyNoteView_MouseLeftButtonUp;
            this.MouseMove += StickyNoteView_MouseMove;
        }

        private void StickyNoteView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this._isDragging)
                return;

            Point newPoint = Mouse.GetPosition(this._parent);

            double top = Canvas.GetTop(this);
            double left = Canvas.GetLeft(this);

            Canvas.SetTop(this, top + (newPoint.Y - this._lastPos.Y));
            Canvas.SetLeft(this, left + (newPoint.X - this._lastPos.X));

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

            this._lastPos = Mouse.GetPosition(this._parent);
            this.CaptureMouse();
        }
    }
}

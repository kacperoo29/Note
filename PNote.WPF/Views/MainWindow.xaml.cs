using PNote.Core;
using PNote.ViewModels;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PNote.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.NoteListView.MouseDoubleClick += NoteListView_MouseDoubleClick;
            var dataContext = this.DataContext as MainWindowViewModel;
            if (dataContext != null)
            {
                dataContext.StickedNotes.CollectionChanged += (s, e) => dataContext.RefreshNotes(this.StickyNoteCanvas);
                dataContext.RefreshNotes(this.StickyNoteCanvas);
            }

            this.StickyNoteCanvas.SizeChanged += StickyNoteCanvas_SizeChanged;
        }

        private void StickyNoteCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var canvas = this.StickyNoteCanvas as Canvas;
            foreach (var child in this.StickyNoteCanvas.Children)
            {
                var uiChild = (UserControl)child;
                if (uiChild == null)
                    continue;

                double top = Canvas.GetTop(uiChild);
                double left = Canvas.GetLeft(uiChild);
                double bottom = top + uiChild.ActualHeight;
                double right = left + uiChild.ActualWidth;

                double newTop = top;
                double newLeft = left;

                if (right > canvas.ActualWidth)
                    newLeft = canvas.ActualWidth - uiChild.ActualWidth;

                if (bottom > canvas.ActualHeight)
                    newTop = canvas.ActualHeight - uiChild.ActualHeight;

                if (newLeft < 0)
                    newLeft = 0;

                if (newTop < 0)
                    newTop = 0;

                Canvas.SetTop(uiChild, newTop);
                Canvas.SetLeft(uiChild, newLeft);
            }
        }

        private void NoteListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var note = this.NoteListView.SelectedItem as Note;
            if (note == null)
                return;

            var dataContext = this.DataContext as MainWindowViewModel;
            if (dataContext == null)
                return;

            dataContext.StickNote(this.StickyNoteCanvas, note).Wait();
        }
    }
}

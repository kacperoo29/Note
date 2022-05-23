using PNote.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class MainWindow : Window
    {
        private readonly INoteService _noteService;
        private bool _dragging = false;
        private StickyNoteView _stickyNote;

        public MainWindow()
        {
            InitializeComponent();

            _noteService = new NoteService();

            NoteListView.ItemsSource = _noteService.GetNotes();
            NoteListView.MouseLeftButtonDown += NoteListView_MouseLeftButtonDown;
            NoteListView.MouseLeftButtonUp += NoteListView_MouseLeftButtonUp;
            NoteListView.MouseMove += NoteListView_MouseMove;
        }

        private void NoteListView_MouseMove(object sender, MouseEventArgs e)
        {
            // https://stackoverflow.com/questions/34267260/wpf-marking-an-image-with-pushpins
        }

        private void NoteListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this._dragging = false;
            // TODO: If not on canvas delete sticky note.
        }

        private void NoteListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._dragging = true;
            this._stickyNote = new StickyNoteView(NoteListView.SelectedItem as Note ?? new Note("BAD", "BAD", DateTime.Now));
        }

    }
}

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

        public MainWindow()
        {
            InitializeComponent();

            this._noteService = new NoteService();

            this.NoteListView.ItemsSource = this._noteService.GetNotes();
            this.NoteListView.MouseDoubleClick += NoteListView_MouseDoubleClick;
        }

        private void NoteListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var stickyNote = new StickyNoteView(NoteListView.SelectedItem as Note ?? new Note("BAD", "BAD", DateTime.Now), this.StickyNoteCanvas);
            Canvas.SetTop(stickyNote, 200);
            Canvas.SetLeft(stickyNote, 200);
            stickyNote.Height = 200;
            stickyNote.Width = 200;

            StickyNoteCanvas.Children.Add(stickyNote);
        }

    }
}

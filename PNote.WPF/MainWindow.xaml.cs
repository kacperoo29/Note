using PNote.Core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PNote
{
    public partial class MainWindow : Window
    {
        private readonly INoteService _noteService;

        public MainWindow(INoteService noteService)
        {
            InitializeComponent();

            this._noteService = noteService;

            this.NoteListView.ItemsSource = this._noteService.GetNotesAsync().Result;
            this.NoteListView.MouseDoubleClick += NoteListView_MouseDoubleClick;

            this.AddButton.Click += AddButton_Click;
            this.EditButton.Click += EditButton_Click;
            this.RemoveButton.Click += RemoveButton_Click;

            foreach (var pinnedNote in this._noteService.GetPinnedNotes().Result)
                StickNote(pinnedNote);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var note = NoteListView.SelectedItem as Note;
            if (note == null)
                return;

            var result = MessageBox.Show($"Do you want to delete note: {note.Name}", "Delete note", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            var noteView = this.StickyNoteCanvas.Children.OfType<StickyNoteView>().FirstOrDefault(x => x.Note.Id == note.Id);
            if (noteView != null)
                this.StickyNoteCanvas.Children.Remove(noteView);

            this._noteService.RemoveNoteAsync(note).Wait();
            this.NoteListView.ItemsSource = this._noteService.GetNotesAsync().Result;
        }
          
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Note note = this._noteService.AddNoteAsync(new Note("Test note", "Test contents", DateTime.Now.AddDays(1))).Result;
            this.NoteListView.ItemsSource = this._noteService.GetNotesAsync().Result;
        }

        private void NoteListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var note = this.NoteListView.SelectedItem as Note;
            if (note == null)
                return;
            
            if (this.StickyNoteCanvas.Children.OfType<StickyNoteView>().Any(x => x.Note.Id == note.Id))
                return;

            StickNote(note);

            note.IsPinned = true;
            this._noteService.EditNoteAsync(note).Wait();
        }

        private void StickNote(Note note)
        {
            var stickyNote = new StickyNoteView(note, this.StickyNoteCanvas);
            Canvas.SetTop(stickyNote, 200);
            Canvas.SetLeft(stickyNote, 200);
            stickyNote.Height = 200;
            stickyNote.Width = 200;

            StickyNoteCanvas.Children.Add(stickyNote);

            stickyNote.Closed += StickyNote_Closed;
        }

        private void StickyNote_Closed(object? sender, EventArgs e)
        {
            var stickyNote = sender as StickyNoteView;
            if (stickyNote == null)
                return;

            stickyNote.Note.IsPinned = false;
            this._noteService.EditNoteAsync(stickyNote.Note).Wait();
        }
    }
}

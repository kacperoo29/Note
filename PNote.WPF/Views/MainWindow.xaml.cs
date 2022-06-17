using Microsoft.Extensions.DependencyInjection;
using PNote.Core;
using PNote.Styles;
using PNote.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System;

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

            this.ChangeThemeButton.Click += ChangeThemeButton_Click;
        }

        private void ChangeThemeButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            if (app != null)
                app.ChangeTheme(app.CurrentStyle == StyleType.Light ? StyleType.Dark : StyleType.Light);
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

                ((uiChild as StickyNoteView)?.DataContext as StickyNoteViewModel)?.SavePosition(newTop, newLeft);
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

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = this.DataContext as MainWindowViewModel;
            var textBox = sender as TextBox;
            if (vm == null || textBox == null)
                return;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                NoteListView.ItemsSource = vm.Notes;
            }
            else
            {
                vm.Search(textBox.Text).Wait();
                NoteListView.ItemsSource = vm.SearchNotes;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addNoteWindow = App.ServiceProvider?.GetService<AddNoteWindow>();
            if (addNoteWindow == null)
                return;

            addNoteWindow.Closing += (object? sender, CancelEventArgs e) =>
            {
                var viewModel = addNoteWindow.DataContext as AddNoteViewModel;

                if (viewModel == null)
                    return;

                if (!viewModel.Added)
                    return;

                (this.DataContext as MainWindowViewModel)?.Notes.Add(viewModel.Note!);
            };

            this.Closing += (object? sender, CancelEventArgs e) =>
            {
                addNoteWindow.Close();
            };

            addNoteWindow.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var editNoteWindow = App.ServiceProvider?.GetService<EditNoteWindow>();            
            if (editNoteWindow == null)
                return;

            (editNoteWindow.DataContext as EditNoteViewModel)!.Note = NoteListView.SelectedItem as Note;
            editNoteWindow.Closing += (object? sender, CancelEventArgs e) =>
            {
                var viewModel = editNoteWindow.DataContext as EditNoteViewModel;

                if (viewModel == null)
                    return;

                if (!viewModel.Edited)
                    return;

                var note = viewModel.Note!;
                var old = (this.DataContext as MainWindowViewModel)?.Notes.FirstOrDefault(n => n.Id == note.Id);
                var index = (this.DataContext as MainWindowViewModel)?.Notes.IndexOf(old);
                (this.DataContext as MainWindowViewModel)!.Notes.Remove(note);

                (this.DataContext as MainWindowViewModel)?.Notes.Insert(index.Value, note);
                (this.DataContext as MainWindowViewModel)?.RefreshNoteList();
                (this.DataContext as MainWindowViewModel)?.RefreshNotes(this.StickyNoteCanvas);                
            };

            this.Closing += (object? sender, CancelEventArgs e) =>
            {
                editNoteWindow.Close();
            };

            editNoteWindow.Show();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();

                if (printDialog.ShowDialog() != true)
                    return;

                printDialog.PrintVisual(this.StickyNoteCanvas, "Sticky notes");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

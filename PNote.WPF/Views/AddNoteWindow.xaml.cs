using PNote.ViewModels;
using System;
using System.Windows;

namespace PNote.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddMovieWindow.xaml
    /// </summary>
    public partial class AddNoteWindow : Window
    {
        public AddNoteWindow()
        {
            InitializeComponent();

            (this.DataContext as AddNoteViewModel).CloseAction = new Action(this.Close);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AddNoteViewModel).Added = false;
            this.Close();
        }
    }
}

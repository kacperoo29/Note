using PNote.ViewModels;
using System;
using System.Windows;

namespace PNote.Views
{
    public partial class EditNoteWindow : Window
    {
        public EditNoteWindow()
        {
            InitializeComponent();

            (this.DataContext as EditNoteViewModel).CloseAction = new Action(this.Close);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as EditNoteViewModel).Edited = false;
            this.Close();
        }
    }
}

using PNote.ViewModels;
using System;
using System.Windows;

namespace PNote.Views
{
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();

            (this.DataContext as AddUserWindowViewModel).CloseAction = new Action(this.Close);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AddUserWindowViewModel).Created = false;
            this.Close();
        }
    }
}

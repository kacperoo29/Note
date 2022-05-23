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
        public Note Note { get; set; }

        public StickyNoteView(Note note)
        {
            InitializeComponent();

            this.Note = note;
        }
    }
}

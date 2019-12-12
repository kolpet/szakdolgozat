using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.ViewModel.Events
{
    public class FileDialogEventArgs
    {
        public string Title { get; set; }

        public string FileName { get; set; }

        public string Filter { get; set; }

        public bool RestoreDirectory { get; set; }

        public string InitialDirectory { get; set; }
    }
}

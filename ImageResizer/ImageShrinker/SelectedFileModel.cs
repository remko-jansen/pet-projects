using System.Collections.Generic;
using System.Windows.Documents;

namespace ImageShrinker
{
    public class SelectedFileModel : NotificationModel
    {
        private string _selectedFile;
        private int _requestedSize;
        private bool _overwriteOriginal;
        private List<string> _droppedFiles;
        private List<int> _predefinedSizes;

        private bool _busy;
        private bool _idle;

        public SelectedFileModel()
        {
            _predefinedSizes = new List<int> { 1024, 1280, 1600, 2048 };
            _droppedFiles = new List<string>();
            _requestedSize = 2048;
            Busy = false;
        }

        public string SelectedFile
        {
            get { return _selectedFile; }
            set { Set(() => SelectedFile, ref _selectedFile, value); }
        }

        public List<string> DroppedFiles
        {
            get { return _droppedFiles; }
            set { Set(() => DroppedFiles, ref _droppedFiles, value); }
        }

        public int RequestedSize
        {
            get { return _requestedSize; }
            set { Set(() => RequestedSize, ref _requestedSize, value); }
        }

        public bool OverwriteOriginal
        {
            get { return _overwriteOriginal; }
            set { Set(() => OverwriteOriginal, ref _overwriteOriginal, value); }
        }

        public List<int> PredefinedSizes {
            get { return _predefinedSizes; }
            set { Set(() => PredefinedSizes, ref _predefinedSizes, value); }
        }

        public bool Busy
        {
            get { return _busy; }
            set
            {
                Set(() => Busy, ref _busy, value);
                Idle = !value;
            }
        }

        public bool Idle
        {
            get { return !_busy; }
            set { Set(() => Idle, ref _idle, value); }
        }
    }
}
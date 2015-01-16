using System;
using System.Collections.Generic;

namespace ImageShrinker.ViewModel
{
    public class SelectedFileViewModel : BaseNotificationModel
    {
        private string _selectedFile;
        private int _requestedSize;
        private bool _overwriteOriginal;
        private List<string> _droppedFiles;
        private List<int> _predefinedSizes;

        private ProgressViewModel _progress;

        private bool _busy;
        private bool _idle;

        public SelectedFileViewModel()
        {
            _predefinedSizes = new List<int> { 1024, 1280, 1600, 2048, 2560, 3000 };
            _droppedFiles = new List<string>();
            _requestedSize = 2048;
            Busy = false;
            Progress = new ProgressViewModel();
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
            get { return _idle; }
            set { Set(() => Idle, ref _idle, value); }
        }

        public ProgressViewModel Progress
        {
            get { return _progress; }
            set { Set(() => Progress, ref _progress, value); }
        }
    }
}
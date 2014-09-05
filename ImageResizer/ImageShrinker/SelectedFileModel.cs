using System.Collections.Generic;
using System.Windows.Documents;

namespace ImageShrinker
{
    public class SelectedFileModel : NotificationModel
    {
        private string _filePath;
        private int _requestedSize;
        private bool _overwriteOriginal;
        private List<int> _predefinedSizes;

        public SelectedFileModel()
        {
            _predefinedSizes = new List<int> { 1024, 1280, 1600, 2048 };
        }

        public string FilePath
        {
            get { return _filePath; }
            set { Set(() => FilePath, ref _filePath, value); }
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
    }
}
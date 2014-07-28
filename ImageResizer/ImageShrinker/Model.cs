using System.Collections.Generic;
using System.Windows.Documents;

namespace ImageShrinker
{
    public class Model : NotificationModel
    {
        private string _filePath;
        private int _requestedSize;
        private List<int> _predefinedSizes;

        public Model()
        {
            _predefinedSizes = new List<int> { 1024, 1280, 1600, 2048 };
        }

        public string FilePath
        {
            get { return _filePath; }
            set { Set("FilePath", ref _filePath, value); }
        }

        public int RequestedSize
        {
            get { return _requestedSize; }
            set { Set("RequestedSize", ref _requestedSize, value); }
        }

        public List<int> PredefinedSizes {
            get { return _predefinedSizes; }
            set { Set("PredefinedSizes", ref _predefinedSizes, value); }
        }
    }
}
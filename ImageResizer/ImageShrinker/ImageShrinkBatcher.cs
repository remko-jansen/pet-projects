using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ImageShrinker.ViewModel;

namespace ImageShrinker
{
    public class ImageShrinkBatcher
    {
        private SelectedFileViewModel _model;

        public ImageShrinkBatcher(SelectedFileViewModel model)
        {
            _model = model;
        }

        public void DoShrink()
        {
            if (!string.IsNullOrWhiteSpace(_model.SelectedFile))
            {
                ShrinkFiles(new List<string> { _model.SelectedFile });
            }
            else if (_model.DroppedFiles != null && _model.DroppedFiles.Count > 0)
            {
                ShrinkFiles(_model.DroppedFiles);
            }
        }

        private void ShrinkFiles(List<string> files)
        {
            Debug.Assert(files != null);
            Debug.Assert(files.Count > 0);

            var renamer = new RenamingService("{0} ({1}px)", _model.OverwriteOriginal, _model.RequestedSize);
            var shrinker = new ShrinkingService(renamer, _model.RequestedSize);

            _model.Busy = true;
            _model.Progress.MaximumSteps = files.Count;
            _model.Progress.CurrentStep = 0;

            foreach (var file in files)
            {
                try
                {
                    shrinker.Shrink(file);
                }
                catch (Exception ex)
                {
                    _model.Progress.AddMessage(ex.Message);
                }

                _model.Progress.CurrentStep++;
            }

            _model.Busy = false;
            _model.Progress.Reset();
        }
    }
}
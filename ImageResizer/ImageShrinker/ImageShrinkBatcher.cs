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

        public async Task DoShrinkAsync()
        {
            List<string> filesToShrink = null;

            if (!string.IsNullOrWhiteSpace(_model.SelectedFile))
            {
                filesToShrink = new List<string> { _model.SelectedFile };
            }
            else if (_model.DroppedFiles != null && _model.DroppedFiles.Count > 0)
            {
                filesToShrink = _model.DroppedFiles;
            }

            if (filesToShrink == null || filesToShrink.Count == 0)
                return;

            await Task.Run(() => ShrinkFiles(filesToShrink));
        }

        private void ShrinkFiles(IReadOnlyCollection<string> files)
        {
            Debug.Assert(files != null);
            Debug.Assert(files.Count > 0);

            var renamer = new RenamingService("{0} ({1}px)", _model.OverwriteOriginal, _model.RequestedSize);
            var shrinker = new ShrinkingService(renamer, _model.RequestedSize);

            _model.Busy = true;
            _model.Progress.Reset();
            _model.Progress.MaximumSteps = files.Count;

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
        }
    }
}
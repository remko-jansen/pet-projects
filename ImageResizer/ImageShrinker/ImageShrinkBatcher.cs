using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using ImageShrinker.ViewModel;

namespace ImageShrinker
{
    public class ImageShrinkBatcher
    {
        private SelectedFileViewModel _model;

        private readonly object _detailsSync = new Object();
        private readonly object _progressSync = new Object();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly string[] _args;

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

            _model.Busy = true;
            _model.Progress.MaximumSteps = files.Count;
            _model.Progress.CurrentStep = 0;

            ResizeAsync(files);

            _model.Progress.CurrentStep++;

            _model.Busy = false;
            _model.Progress.Reset();
        }

        private async void ResizeAsync(List<string> files)
        {
            var renamer = new RenamingService("{0} ({1}px)", _model.OverwriteOriginal, _model.RequestedSize);
            var shrinker = new ShrinkingService(renamer, _model.RequestedSize);

            try
            {
                await Task.Run(() =>
                               Parallel.ForEach(
                                                files,
                                                new ParallelOptions
                                                    {
                                                        CancellationToken = _cancellationTokenSource.Token,
                                                        MaxDegreeOfParallelism = 1, //Environment.ProcessorCount
                                                    },
                                                image =>
                                                {
                                                    try
                                                    {
                                                        shrinker.Shrink(image);
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }

                                                    lock (_progressSync)
                                                    {
                                                        _model.Progress.CurrentStep++;
                                                    }
                                                }
                                   )
                    );
            }
            catch (OperationCanceledException)
            {
            }
        }


    }
}
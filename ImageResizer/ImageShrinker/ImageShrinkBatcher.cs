using System;
using System.Drawing;
using System.IO;

namespace ImageShrinker
{
    public class ImageShrinkBatcher
    {
        private SelectedFileModel _model;

        public ImageShrinkBatcher(SelectedFileModel model)
        {
            _model = model;
        }

        public void DoShrink()
        {
            var renamer = new RenamingService("{0} ({1}px)", _model.OverwriteOriginal, _model.RequestedSize);
            var service = new ShrinkingService(renamer);

            service.Shrink(_model.SelectedFile, _model.RequestedSize);
        }

    }
}
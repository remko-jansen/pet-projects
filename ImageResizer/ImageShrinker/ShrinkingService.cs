// This class is based on code from "Image Resizer for windows".
// Image Resizer for Windows is a utility that lets you resize one or more selected image files directly from Windows Explorer by right-clicking.
// http://imageresizer.codeplex.com/

//------------------------------------------------------------------------------
// <copyright file="ResizingService.cs" company="Brice Lambson">
//     Copyright (c) 2011-2013 Brice Lambson. All rights reserved.
//
//     The use of this software is governed by the Microsoft Public License
//     which is included with this distribution.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageShrinker
{
    public class ShrinkingService
    {
        private const string DefaultEncoderExtension = ".jpg";
        private static readonly Type DefaultEncoderType = typeof(JpegBitmapEncoder);

        private readonly int _encoderQualityLevel;
        private readonly RenamingService _renamer;

        public ShrinkingService(RenamingService renamer, int encoderQualityLevel = 85)
        {
            Debug.Assert(encoderQualityLevel >= 1 && encoderQualityLevel <= 100);

            _encoderQualityLevel = encoderQualityLevel;
            _renamer = renamer;
        }

        public string Shrink(string sourcePath, int targetSize)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(sourcePath));
            Debug.Assert(targetSize > 0);

            BitmapDecoder decoder;
            BitmapEncoder encoder;
            GetBitmapDecoderEncoder(sourcePath, out decoder, out encoder);
            if (decoder == null || encoder == null)
                throw new ArgumentException("Image type is not supported.");

            // NOTE: grab its first (and usually only) frame. Only TIFF and GIF images support multiple frames
            var sourceFrame = decoder.Frames[0];

            // Apply the transform
            var steps = new StepCalculator().GetSteps(new Size(sourceFrame.PixelWidth, sourceFrame.PixelHeight), targetSize);
            if (steps == null || steps.Count == 1)
                return sourcePath;

            BitmapFrame destinationFrame = null;
            for (var i = 1; i < steps.Count; i++)
            {
                var transform = GetTransform(sourceFrame, steps[i]);
                var transformedBitmap = new TransformedBitmap(sourceFrame, transform);

                // Create the destination frame
                var thumbnail = sourceFrame.Thumbnail;
                var metadata = sourceFrame.Metadata as BitmapMetadata;
                var colorContexts = sourceFrame.ColorContexts;
                destinationFrame = BitmapFrame.Create(transformedBitmap, thumbnail, metadata, colorContexts);

                sourceFrame = destinationFrame;
            }

            encoder.Frames.Add(destinationFrame);

            return SaveResultToFile(sourcePath, encoder);
        }

        private string SaveResultToFile(string sourcePath, BitmapEncoder encoder)
        {
            string destinationPath = _renamer.Rename(sourcePath);

            if (encoder.GetType() == DefaultEncoderType)
            {
                destinationPath = Path.ChangeExtension(destinationPath, DefaultEncoderExtension);
            }

            using (var destinationStream = File.Open(destinationPath, FileMode.Create))
            {
                // Save the final image
                encoder.Save(destinationStream);
            }

            return destinationPath;
        }

        private void GetBitmapDecoderEncoder(string sourcePath, out BitmapDecoder decoder, out BitmapEncoder encoder)
        {
            using (var sourceStream = File.OpenRead(sourcePath))
            {
                // NOTE: Using BitmapCacheOption.OnLoad here will read the entire file into
                //       memory which allows us to dispose of the file stream immediately
                decoder = BitmapDecoder.Create(sourceStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }

            encoder = BitmapEncoder.Create(decoder.CodecInfo.ContainerFormat);

            try
            {
                // NOTE: This will throw if the codec does not support encoding
                var dummy = encoder.CodecInfo;
            }
            catch (NotSupportedException)
            {
                // Fallback to the default (JPEG) encoder
                encoder = (BitmapEncoder)Activator.CreateInstance(DefaultEncoderType);
            }

            // Set encoder quality if it is a jpeg encoder
            var jpegEncoder = encoder as JpegBitmapEncoder;
            if (jpegEncoder != null)
            {
                jpegEncoder.QualityLevel = _encoderQualityLevel;
            }
        }

        private Transform GetTransform(BitmapSource source, Size targetSize)
        {
            Debug.Assert(source != null);
            Debug.Assert(targetSize != null);

            var scaleX = targetSize.Width / ((double)source.PixelWidth);
            var scaleY = targetSize.Height / ((double)source.PixelHeight);

            return new ScaleTransform(scaleX, scaleY);
        }  
    }
}
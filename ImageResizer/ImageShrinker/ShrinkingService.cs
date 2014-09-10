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
        private readonly int _targetSize;
        private readonly RenamingService _renamer;

        public ShrinkingService(RenamingService renamer, int targetSize, int encoderQualityLevel = 85)
        {
            Debug.Assert(encoderQualityLevel >= 1 && encoderQualityLevel <= 100);
            Debug.Assert(targetSize > 0);

            _encoderQualityLevel = encoderQualityLevel;
            _targetSize = targetSize;
            _renamer = renamer;
        }

        public void Shrink(string sourcePath)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(sourcePath));

            BitmapDecoder decoder;
            BitmapEncoder encoder;
            GetBitmapDecoderEncoder(sourcePath, out decoder, out encoder);
            if (decoder == null || encoder == null)
                throw new ArgumentException("Image type is not supported.");

            // NOTE: grab its first (and usually only) frame. Only TIFF and GIF images support multiple frames
            var sourceFrame = decoder.Frames[0];

            // Apply the transform
            var transform = GetTransform(sourceFrame);

            if (transform == null)
                return;

            var transformedBitmap = new TransformedBitmap(sourceFrame, transform);

            // Create the destination frame
            var thumbnail = sourceFrame.Thumbnail;
            var metadata = sourceFrame.Metadata as BitmapMetadata;
            var colorContexts = sourceFrame.ColorContexts;
            var destinationFrame = BitmapFrame.Create(transformedBitmap, thumbnail, metadata, colorContexts);

            encoder.Frames.Add(destinationFrame);

            SaveResultToFile(sourcePath, encoder);
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

        private Transform GetTransform(BitmapSource source)
        {
            Debug.Assert(source != null);

            // Use the longest side of the image to calculate how much it must be scaled to reach the target size. 
            double scale;
            if (source.PixelWidth > source.PixelHeight)
            {
                scale = _targetSize/((double) source.PixelWidth);
            }
            else
            {
                scale = _targetSize/((double) source.PixelHeight);
            }

            if (scale > 1.0)
                return null;

            return new ScaleTransform(scale, scale);
        }  
    }
}
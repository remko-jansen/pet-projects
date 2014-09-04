//------------------------------------------------------------------------------
// Code copied from "Image Resizer for windows".
// Image Resizer for Windows is a utility that lets you resize one or more selected image files directly from Windows Explorer by right-clicking.
// http://imageresizer.codeplex.com/
//
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
using System.Globalization;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageShrinker
{
    public class ShrinkingService
    {
        private const string DefaultEncoderExtension = ".jpg";
        private static readonly Type DefaultEncoderType = typeof(JpegBitmapEncoder);

        private readonly int _encoderQualityLevel;
        private readonly Size _targetSize;

        public ShrinkingService(Size targetSize, int encoderQualityLevel = 85)
        {
            Debug.Assert(encoderQualityLevel >= 1 && encoderQualityLevel <= 100);
            Debug.Assert(targetSize != null);

            _encoderQualityLevel = encoderQualityLevel;
            _targetSize = targetSize;
        }

        public string Resize(string sourcePath)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(sourcePath));

            var encoderDefaulted = false;
            BitmapDecoder decoder;

            using (var sourceStream = File.OpenRead(sourcePath))
            {
                // NOTE: Using BitmapCacheOption.OnLoad here will read the entire file into
                //       memory which allows us to dispose of the file stream immediately
                decoder = BitmapDecoder.Create(sourceStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }

            var encoder = BitmapEncoder.Create(decoder.CodecInfo.ContainerFormat);

            try
            {
                // NOTE: This will throw if the codec dose not support encoding
                var _ = encoder.CodecInfo;
            }
            catch (NotSupportedException)
            {
                // Fallback to JPEG encoder
                encoder = (BitmapEncoder)Activator.CreateInstance(DefaultEncoderType);
                encoderDefaulted = true;
            }

            // TODO: Copy container-level metadata if codec supports it
            SetEncoderSettings(encoder);

            string destinationPath = null;

            // NOTE: grab its first (and usually only) frame. Only TIFF and GIF images support multiple frames
            var sourceFrame = decoder.Frames[0];

            // Apply the transform
            var transform = GetTransform(sourceFrame);
            var transformedBitmap = new TransformedBitmap(sourceFrame, transform);

            // Create the destination frame
            var thumbnail = sourceFrame.Thumbnail;
            var metadata = sourceFrame.Metadata as BitmapMetadata;
            var colorContexts = sourceFrame.ColorContexts;
            var destinationFrame = BitmapFrame.Create(transformedBitmap, thumbnail, metadata, colorContexts);

            encoder.Frames.Add(destinationFrame);

            // Set the destination path using the first frame
            if (destinationPath == null)
            {
                if (encoderDefaulted)
                {
                    sourcePath = Path.ChangeExtension(sourcePath, DefaultEncoderExtension);
                }

                destinationPath = sourcePath;
            }

            using (var destinationStream = File.OpenWrite(destinationPath))
            {
                // Save the final image
                encoder.Save(destinationStream);
            }

            return destinationPath;
        }

        private void SetEncoderSettings(BitmapEncoder encoder)
        {
            Debug.Assert(encoder != null);

            var jpegEncoder = encoder as JpegBitmapEncoder;

            if (jpegEncoder != null)
            {
                jpegEncoder.QualityLevel = _encoderQualityLevel;
            }
        }

        private Transform GetTransform(BitmapSource source)
        {
            Debug.Assert(source != null);

            var width = _targetSize.Width;
            var height = _targetSize.Height;

            if ((width > height) != (source.PixelWidth > source.PixelHeight))
            {
                var temp = width;
                width = height;
                height = temp;
            }

            var scaleX = width / ((double)source.PixelWidth);
            var scaleY = height / ((double)source.PixelHeight);

            var minScale = Math.Min(scaleX, scaleY);

            scaleX = minScale;
            scaleY = minScale;

            if (scaleX > 1.0)
            {
                scaleX = 1.0;
                scaleY = 1.0;
            }

            return new ScaleTransform(scaleX, scaleY);
        }  
    }
}
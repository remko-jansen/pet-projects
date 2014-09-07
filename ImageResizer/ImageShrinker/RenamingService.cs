// This class is based on code from "Image Resizer for windows".
// Image Resizer for Windows is a utility that lets you resize one or more selected image files directly from Windows Explorer by right-clicking.
// http://imageresizer.codeplex.com/
//

//------------------------------------------------------------------------------
// <copyright file="RenamingService.cs" company="Brice Lambson">
//     Copyright (c) 2011-2013 Brice Lambson. All rights reserved.
//
//     The use of this software is governed by the Microsoft Public License
//     which is included with this distribution.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace ImageShrinker
{
    public class RenamingService
{
        private const string UniqueFileNameFormat = "{0} ({1})";

        private readonly string _fileNameFormat;
        private readonly bool _overwriteOriginals;
        private readonly int _targetSize;

        public RenamingService(string fileNameFormat, bool overwriteOriginals, int targetSize)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(fileNameFormat));
            Debug.Assert(fileNameFormat.Contains("{0}"));
            
            _fileNameFormat = fileNameFormat;
            _overwriteOriginals = overwriteOriginals;
            _targetSize = targetSize;
        }

        public string Rename(string sourcePath)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(sourcePath));

            if (_overwriteOriginals)
            {
                return sourcePath;
            }

            var directoryName = Path.GetDirectoryName(sourcePath);
            var fileName = Path.GetFileNameWithoutExtension(sourcePath);
            var extension = Path.GetExtension(sourcePath);

            // TODO: Define more replacement variables
            //        * Selected size width
            //        * Selected size height
            //        * Selected size units
            //        * Selected size pixel width
            //        * Selected size pixel height
            //        * Actual width
            //        * Actual height
            //        * Actual pixel width
            //        * Actual pixel height
            var replacementItems = new object[]
            {
                // {0} = Original file name
                fileName,

                // {1} = target image size
                _targetSize,
            };

            var destinationFileName = String.Format(CultureInfo.CurrentCulture, _fileNameFormat, replacementItems);
            var destinationPath = Path.Combine(directoryName, destinationFileName + extension);
            var i = 1;

            // Ensure the file name is unique
            while (File.Exists(destinationPath))
            {
                var uniqueFileName = String.Format(CultureInfo.CurrentCulture, UniqueFileNameFormat, destinationFileName, ++i);
                destinationPath = Path.Combine(directoryName, uniqueFileName + extension);
            }

            Debug.Assert(!String.IsNullOrWhiteSpace(destinationPath));

            return destinationPath;
        }
    }
}

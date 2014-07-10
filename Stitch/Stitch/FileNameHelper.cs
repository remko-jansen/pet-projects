using System;
using System.IO;

namespace Stitch
{
    public class FileNameHelper
    {
        private const string DefaultExtension = ".jpg";

        public string GetOutputFileName(string filePath1, string filePath2)
        {
            if (string.IsNullOrWhiteSpace(filePath1) && string.IsNullOrWhiteSpace(filePath2))
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(filePath1) && string.IsNullOrWhiteSpace(filePath2))
            {
                return GetFileNameBasedOn(filePath1);
            }

            if (string.IsNullOrWhiteSpace(filePath1) && !string.IsNullOrWhiteSpace(filePath2))
            {
                return GetFileNameBasedOn(filePath2);
            }

            return GetFileNameBasedOn(filePath1, filePath2);
        }

        private string GetFileNameBasedOn(string filePath)
        {
            string dir;

            try
            {
                dir = Path.GetDirectoryName(filePath) ?? "";
            }
            catch (Exception e)
            {
                return "";
            }

            var filename = Path.GetFileNameWithoutExtension(filePath);

            var newFilePath = Path.Combine(dir, filename + "_Stitched") + DefaultExtension;

            return newFilePath;
        }

        private string GetFileNameBasedOn(string filePath1, string filePath2)
        {
            string dir;

            try
            {
                dir = Path.GetDirectoryName(filePath1) ?? "";
            }
            catch (Exception e)
            {
                return "";
            }

            var filename1 = Path.GetFileNameWithoutExtension(filePath1);
            var filename2 = Path.GetFileNameWithoutExtension(filePath2);

            var newFileName = GetCommonPart(filename1, filename2);
            if (string.IsNullOrWhiteSpace(newFileName))
                return GetFileNameBasedOn(filePath1);

            var newFilePath = Path.Combine(dir, newFileName) + DefaultExtension;

            if (newFilePath == filePath1 || newFilePath == filePath2)
                return GetFileNameBasedOn(filePath1);

            return newFilePath;
        }

        private string GetCommonPart(string fileName1, string fileName2)
        {
            var commonPart = "";
            var minLenght = Math.Min(fileName1.Length, fileName2.Length);

            for (int i = 0; i < minLenght; i++)
            {
                if (fileName1[i] == fileName2[i])
                {
                    commonPart += fileName1[i];
                }
                else
                {
                    break;
                }
            }
            return commonPart.TrimEnd();
        }

    }
}
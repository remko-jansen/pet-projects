using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stitch
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void textBoxImage1_TextChanged(object sender, EventArgs e)
        {
            textBoxOutPutImage.Text = MakeOutputPath();
        }

        private void textBoxImage2_TextChanged(object sender, EventArgs e)
        {
            textBoxOutPutImage.Text = MakeOutputPath();
        }

        private string MakeOutputPath()
        {
            var outputFile = new FileNameHelper().GetOutputFileName(textBoxImage1.Text, textBoxImage2.Text);
            return outputFile;
        }

        private void buttonBrowseImage1_Click(object sender, EventArgs e)
        {
            var newPath = AskUserForNewFilePath(textBoxImage1.Text);

            if (!string.IsNullOrWhiteSpace(newPath))
            {
                textBoxImage1.Text = newPath;
            }
        }

        private void buttonBrowseImage2_Click(object sender, EventArgs e)
        {
            var newPath = AskUserForNewFilePath(textBoxImage2.Text);

            if (!string.IsNullOrWhiteSpace(newPath))
            {
                textBoxImage2.Text = newPath;
            }
        }

        private string AskUserForNewFilePath(string initialFile)
        {
            var filePath = "";

            var initialFilePath = "";
            try
            {
                initialFilePath = Path.GetDirectoryName(initialFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg files (*.jpg)|*.jpg;*.jpeg;*.jpe;*.jfif|All files (*.*)|*.*";
            openFileDialog.CheckFileExists = true;
            if (!string.IsNullOrEmpty(initialFilePath))
            {
                openFileDialog.InitialDirectory = initialFilePath;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }

            return filePath;
        }

        private void buttonStitch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxImage1.Text) || string.IsNullOrWhiteSpace(textBoxImage2.Text) || string.IsNullOrWhiteSpace(textBoxOutPutImage.Text))
                return;

            var imageFile1 = new FileInfo(textBoxImage1.Text);
            var imageFile2 = new FileInfo(textBoxImage2.Text);
            var outputFile = new FileInfo(textBoxOutPutImage.Text);

            if (imageFile1.Exists && imageFile2.Exists)
            {
                var stitcher = new ImageStitcher(imageFile1, imageFile2);
                stitcher.Border = new SolidBorder(2, Color.Purple)
                                      {
                                          Inside = false
                                      };

                var outputImage = stitcher.DoStitch();

                var jpegSaver = new JpegSaver {ImageQuality = 85};
                jpegSaver.Save(outputImage, outputFile);
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            textBoxImage1.Text = "";
            textBoxImage2.Text = "";

            if (files.Length > 0)
            {
                textBoxImage1.Text = files[0];
            }

            if (files.Length > 1)
            {
                textBoxImage2.Text = files[1];
            }

            if (!string.IsNullOrWhiteSpace(textBoxImage1.Text) && !string.IsNullOrWhiteSpace(textBoxImage2.Text))
            {
                buttonStitch.PerformClick();
            }

        }
    }
}

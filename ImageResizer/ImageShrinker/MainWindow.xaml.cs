using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ImageShrinker.ViewModel;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageShrinker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SelectedFileViewModel _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new SelectedFileViewModel();

            DataContext = _model;
        }

        private async void Window_Drop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                _model.DroppedFiles = new List<string>(files);
                _model.SelectedFile = "";

                var shrinker = new ImageShrinkBatcher(_model);
                await shrinker.DoShrinkAsync();
            }
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void SelectedFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;

            ImageSource image = null;
            if (tb != null && !string.IsNullOrWhiteSpace(tb.Text))
            {
                image = GetImageFromFile(tb.Text);
            }

            if (image == null)
            {
                image = ResourceHelper.LoadBitmapFromResource("Images/DragHere.png");
            }

            ImagePreview.Source = image;
        }

        private ImageSource GetImageFromFile(string path)
        {
            ImageSource result = null;

            try
            {
                BitmapDecoder decoder;
                using (var sourceStream = File.OpenRead(path))
                {
                    // NOTE: Using BitmapCacheOption.OnLoad here will read the entire file into
                    //       memory which allows us to dispose of the file stream immediately
                    decoder = BitmapDecoder.Create(sourceStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }

                result = decoder.Frames[0];
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        private async void ShrinkIt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_model.SelectedFile) || _model.RequestedSize <= 0)
                return;

            _model.DroppedFiles.Clear();

            var shrinker = new ImageShrinkBatcher(_model);
            await shrinker.DoShrinkAsync();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            var dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Images|*.jpg;*.jpeg;*.png|All Files|*.*";
            dlg.CheckFileExists = true;

            var result = dlg.ShowDialog();
            if (result == true)
            {
                _model.SelectedFile = dlg.FileName;
            }
        }
    }
}

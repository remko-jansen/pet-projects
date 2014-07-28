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
        private readonly Model _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new Model {RequestedSize = 1024};

            DataContext = _model;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                _model.FilePath = files[0];
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

        private void InputImageFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;

            ImagePreview.Source = null;
            if (tb != null)
            {
                var image = GetImageFromFile(tb.Text);
                if (image != null)
                {
                    ImagePreview.Source = image;
                }
            }
        }

        private BitmapImage GetImageFromFile(string path)
        {
            BitmapImage result = null;
            try
            {
                if (File.Exists(path))
                {
                    result = new BitmapImage(new Uri(path));
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        private void ShrinkIt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_model.FilePath) || _model.RequestedSize <= 0)
                return;

            var shrinker = new ImageShrinkHelper(_model.FilePath, _model.RequestedSize);
            shrinker.DoShrink();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            var dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Images|*.jpg;*.jpeg|All Files|*.*";
            dlg.CheckFileExists = true;

            var result = dlg.ShowDialog();
            if (result == true)
            {
                _model.FilePath = dlg.FileName;
            }

        }
    }
}

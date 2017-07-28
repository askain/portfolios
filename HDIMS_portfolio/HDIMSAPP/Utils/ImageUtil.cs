using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageTools;
using ImageTools.IO.Png;

namespace HDIMSAPP.Utils
{
    public class ImageUtil
    {
        public static void ExportImage(FrameworkElement target)
        {
            try
            {
                WriteableBitmap bmp = new WriteableBitmap((int)target.ActualWidth, (int)target.ActualHeight);
                bmp.Render(target, new TranslateTransform());
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "png";
                saveDialog.Filter = "이미지파일 (.png)|*.png";
                if (saveDialog.ShowDialog().Value)
                {
                    using (Stream _stream = saveDialog.OpenFile())
                    {
                        PngEncoder encoder = new PngEncoder();
                        encoder.Encode(bmp.ToImage(), _stream);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

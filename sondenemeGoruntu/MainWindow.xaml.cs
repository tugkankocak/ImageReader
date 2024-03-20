using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Tesseract;

namespace sondenemeGoruntu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Dosya seçme iletişim kutusu aç
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Dosyayı yükle
                string imagePath = openFileDialog.FileName;

                // Görüntüyü önizleme olarak yükle
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                previewImage.Source = bitmap;
            }
        }

        private void RecognizeTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (previewImage.Source == null)
            {
                MessageBox.Show("Önce bir ekran görüntüsü seçin.");
                return;
            }

            // Tesseract OCR motorunu başlatma
            //using (var engine = new TesseractEngine(@"C:\Users\tugkan\gorselislemeveri\tesseract\tessdata", "eng", EngineMode.Default))
            using (var engine = new TesseractEngine(@"../../tessdata", "eng", EngineMode.Default))
            {
                // Görüntüyü yükleme
                using (var img = Pix.LoadFromFile(((BitmapImage)previewImage.Source).UriSource.LocalPath))
                {
                    // OCR işlemi
                    using (var page = engine.Process(img))
                    {
                        // Tanınan metni al
                        var text = page.GetText();

                        // Metni TextBox'a yerleştir
                        txtResult.Text = text;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ImpartireImagine
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

        private void ButonIncarcaImagine_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Fotografii|*.jpg|Toate fișierele|*.*", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) };
            if (dialog.ShowDialog() == true)
            {
                var imagine = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
                TextBlockDetaliiImagine.Text = string.Format("{0}: {1} / {2}", System.IO.Path.GetFileNameWithoutExtension(dialog.FileName), imagine.Width, imagine.Height);
                Imagine.Source = imagine;
                AplicaImpartire();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsInitialized)
                return;
            AplicaImpartire();
        }

        private void AplicaImpartire()
        {
            GridImpartire.Children.Clear();
            GridImpartire.ColumnDefinitions.Clear();
            GridImpartire.RowDefinitions.Clear();
            int cx, cy;
            if (int.TryParse(TextBoxX.Text, out cx) && cx >= 1 && cx <= 100)
            {
                if (int.TryParse(TextBoxY.Text, out cy) && (cy > 1 || (cy == 1 && cx > 1)) && cy <= 100)
                {
                    for (int i = 0; i < cx; i++)
                        GridImpartire.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    for (int j = 0; j < cy; j++)
                        GridImpartire.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    for (int i = 0; i < cx; i++)
                    {
                        for (int j = 0; j < cy; j++)
                        {
                            var borderExterior = new Border { BorderBrush = BorderImpartireExterior.BorderBrush, BorderThickness = BorderImpartireExterior.BorderThickness };
                            Grid.SetColumn(borderExterior, i);
                            Grid.SetRow(borderExterior, j);
                            GridImpartire.Children.Add(borderExterior);
                            var borderInterior = new Border { BorderBrush = BorderImpartireInterior.BorderBrush, BorderThickness = BorderImpartireInterior.BorderThickness };
                            borderExterior.Child = borderInterior;
                        }
                    }
                }
            }
        }
    }
}

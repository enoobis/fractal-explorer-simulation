using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fractal_explorer_simulation
{
    public partial class MainWindow : Window
    {
        private const int ImageWidth = 800;
        private const int ImageHeight = 600;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RenderButton_Click(object sender, RoutedEventArgs e)
        {
            int iterations;
            if (!int.TryParse(IterationsTextBox.Text, out iterations))
            {
                MessageBox.Show("Invalid input for iterations.");
                return;
            }

            RenderFractal(iterations);
        }
        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            // Increase the zoom level and recalculate the fractal
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            // Decrease the zoom level and recalculate the fractal
        }

        private void PanUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Pan the fractal image upwards and recalculate the fractal
        }

        private void PanDownButton_Click(object sender, RoutedEventArgs e)
        {
            // Pan the fractal image downwards and recalculate the fractal
        }

        private void PanLeftButton_Click(object sender, RoutedEventArgs e)
        {
            // Pan the fractal image to the left and recalculate the fractal
        }

        private void PanRightButton_Click(object sender, RoutedEventArgs e)
        {
            // Pan the fractal image to the right and recalculate the fractal
        }

        private void RenderFractal(int iterations)
        {
            BitmapSource fractalImage = GenerateFractal(iterations);
            FractalImage.Source = fractalImage;
        }

        private BitmapSource GenerateFractal(int iterations)
        {
            RenderTargetBitmap fractal = new RenderTargetBitmap(ImageWidth, ImageHeight, 96, 96, PixelFormats.Default);

            double xScale = 3.5 / ImageWidth;
            double yScale = 2.0 / ImageHeight;

            for (int x = 0; x < ImageWidth; x++)
            {
                for (int y = 0; y < ImageHeight; y++)
                {
                    double zx = 0;
                    double zy = 0;
                    double cx = x * xScale - 2.5;
                    double cy = y * yScale - 1.0;

                    int i;
                    for (i = 0; i < iterations; i++)
                    {
                        double temp = zx * zx - zy * zy + cx;
                        zy = 2.0 * zx * zy + cy;
                        zx = temp;

                        if (zx * zx + zy * zy > 4)
                            break;
                    }

                    byte fractalR = (byte)(255 - i * 255 / iterations);
                    byte fractalG = 0;
                    byte fractalB = 0;

                    byte calculationR = (byte)(255 - (i % 16) * 16);
                    byte calculationG = (byte)(i % 256);
                    byte calculationB = 0;

                    Color fractalColor = Color.FromRgb(fractalR, fractalG, fractalB);
                    Color calculationColor = Color.FromRgb(calculationR, calculationG, calculationB);

                    SolidColorBrush fractalBrush = new SolidColorBrush(fractalColor);
                    SolidColorBrush calculationBrush = new SolidColorBrush(calculationColor);

                    Rectangle fractalRect = new Rectangle
                    {
                        Width = 1,
                        Height = 1,
                        Fill = fractalBrush,
                        SnapsToDevicePixels = true
                    };

                    Rectangle calculationRect = new Rectangle
                    {
                        Width = 1,
                        Height = 1,
                        Fill = calculationBrush,
                        SnapsToDevicePixels = true
                    };

                    fractalRect.Arrange(new Rect(x, y, 1, 1));
                    calculationRect.Arrange(new Rect(x, y, 1, 1));

                    if (i == iterations)
                    {
                        fractal.Render(fractalRect);
                    }
                    else
                    {
                        fractal.Render(calculationRect);
                    }
                }
            }

            return fractal;
        }
    }
}

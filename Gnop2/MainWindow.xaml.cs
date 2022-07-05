using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Gnop2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _animate = new DispatcherTimer();
        private double ballVelocity = 5;

        public MainWindow()
        {
            InitializeComponent();
            _animate.Interval = TimeSpan.FromMilliseconds(16);
            _animate.Tick += _animateBall;
        }

        private void _animateBall(object? sender, EventArgs e)
        {
            // get current x position of ball
            double x = Canvas.GetLeft(Ball);

            Canvas.SetLeft(Ball, x + ballVelocity);
        }

        private void btn_start_click(object sender, RoutedEventArgs e)
        {
            // toggle game loop
            if (_animate.IsEnabled) _animate.Stop();
            else _animate.Start();
        }
    }
}

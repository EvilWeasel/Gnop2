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
        // TODO: Refactor to use negative velocity for reverse movement
        private double ballVelocity = 5;
        private bool directionRight = true;

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

            // move ball on x
            if (directionRight) Canvas.SetLeft(Ball, x + ballVelocity);
            else Canvas.SetLeft(Ball, x - ballVelocity);

            // check if ball is outside boundary area
            if (x >= GameArea.ActualWidth - Ball.ActualWidth) directionRight = false;
            else if (x <= 0) directionRight = true;
        }

        private void btn_start_click(object sender, RoutedEventArgs e)
        {
            // toggle game loop
            if (_animate.IsEnabled)
            {
                _animate.Stop();
                Canvas.SetLeft(Ball, (GameArea.ActualWidth - Ball.ActualWidth) / 2);
                Canvas.SetTop(Ball, (GameArea.ActualHeight - Ball.ActualHeight) / 2);
            }
            else _animate.Start();
        }
    }
}

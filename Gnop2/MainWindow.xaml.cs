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
        private bool directionBottom = true;

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
            double y = Canvas.GetTop(Ball);

            #region directionX
            // move ball on x
            if (directionRight) Canvas.SetLeft(Ball, x + ballVelocity);
            else Canvas.SetLeft(Ball, x - ballVelocity);

            // check if ball is outside x boundary area
            if (x >= GameArea.ActualWidth - Ball.ActualWidth) directionRight = false;
            else if (x <= 0) directionRight = true;
            #endregion

            #region directionY
            // move ball on y
            if (directionBottom) Canvas.SetTop(Ball, y + ballVelocity);
            else Canvas.SetTop(Ball, y - ballVelocity);

            // check if ball is outside y boundary area
            if (y >= GameArea.ActualHeight - Ball.ActualHeight) directionBottom = false;
            else if (y <= 0) directionBottom = true;
            #endregion
        }

        private void btn_start_click(object sender, RoutedEventArgs e)
        {
            // toggle game loop
            if (_animate.IsEnabled)
            {
                _animate.Stop();
                Init();
            }
            else _animate.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            // set position of right paddle
            Canvas.SetLeft(RightPaddle, GameArea.ActualWidth - RightPaddle.ActualWidth);
            Canvas.SetTop(RightPaddle, (GameArea.ActualHeight - RightPaddle.ActualHeight) / 2);
            // set position of left paddle
            Canvas.SetLeft(LeftPaddle, 0);
            Canvas.SetTop(LeftPaddle, (GameArea.ActualHeight - LeftPaddle.ActualHeight) / 2);

            // set position of ball
            Canvas.SetLeft(Ball, (GameArea.ActualWidth - Ball.ActualWidth) / 2);
            Canvas.SetTop(Ball, (GameArea.ActualHeight - Ball.ActualHeight) / 2);
        }
    }
}

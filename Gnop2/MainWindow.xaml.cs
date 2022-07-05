using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private double paddleVelocity = 10;
        private bool directionRight = true;
        private bool directionBottom = true;
        private bool PLUp = false;
        private bool PLDown = false;
        private bool PRUp = false;
        private bool PRDown = false;

        public MainWindow()
        {
            InitializeComponent();
            _animate.Interval = TimeSpan.FromMilliseconds(16);
            _animate.Tick += _animateBall;
            _animate.Tick += _animatePaddles;
        }

        private void _animatePaddles(object? sender, EventArgs e)
        {
            //double plX = Canvas.GetLeft(LeftPaddle);
            //double prX = Canvas.GetLeft(RightPaddle);
            double plY = Canvas.GetTop(LeftPaddle); 
            double prY = Canvas.GetTop(RightPaddle);
            // Left Player Movement
            if (PLUp)
                if (plY > 10)
                    Canvas.SetTop(LeftPaddle, plY - 10);
                else Canvas.SetTop(LeftPaddle, 0);
            else if (PLDown)
                if (plY + LeftPaddle.ActualHeight < GameArea.ActualHeight)
                    Canvas.SetTop(LeftPaddle, plY + 10); 
            // Right Player Movement
            if (PRUp)
                if (prY > 10)
                    Canvas.SetTop(RightPaddle, prY - 10);
                else Canvas.SetTop(RightPaddle, 0);
            else if (PRDown)
                if (prY + RightPaddle.ActualHeight < GameArea.ActualHeight)
                    Canvas.SetTop(RightPaddle, prY + 10);
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

        #region KeyboardEvents for Movement
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    PLUp = true;
                    PLDown = false;
                    break;
                case Key.S:
                    PLDown = true;
                    PLUp = false;
                    break;
                case Key.Up:
                    PRUp = true;
                    PRDown = false;
                    break;
                case Key.Down:
                    PRDown = true;
                    PRUp = false;
                    break;
            }
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    PLUp = false;
                    PLDown = false;
                    break;
                case Key.S:
                    PLDown = false;
                    PLUp = false;
                    break;
                case Key.Up:
                    PRUp = false;
                    PRDown = false;
                    break;
                case Key.Down:
                    PRDown = false;
                    PRUp = false;
                    break;
            }
        }
        #endregion
    }
}

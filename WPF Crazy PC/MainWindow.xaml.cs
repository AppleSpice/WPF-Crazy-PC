using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPF_Crazy_PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int score;
        int x;
        int y;
        
        public Random r = new Random();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(xButton.Content) == "Start")
            {
                StartGame();
            }
            else
            {
                Score();
            }
        }

        public void StartGame()
        {
            //start timer
            Thread timerThreadStart = new Thread(new ThreadStart(Timer));
            timerThreadStart.Start();
            //change name 
            xButton.Content = "Click Me!";

        }

        public void Timer()
        {
            int time = 0;
            while (time >= 60)
            {
                xTimer.Text = Convert.ToString(60 - time);
                Thread.Sleep(1000);
                time++;
            }
        }

        public void Score()
        {
            //adds score and prints
            score++;
            xScore.Text = Convert.ToString($"Score: {score}");
            //move button
            y = r.Next(1,400);
            x = r.Next(1,800);
            xButton.Margin = new Thickness(x,y,0,0);
        }
    }
}

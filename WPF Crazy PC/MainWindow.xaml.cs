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
using System.Windows.Threading;
using System.IO;

namespace WPF_Crazy_PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //timer
        DispatcherTimer dt = new DispatcherTimer();
        //int
        int score;
        int x;
        int y;
        int xMouseLocation;
        int yMouseLocation;
        int moveX = 0;
        int moveY = 0;
        //random
        public Random r = new Random();
        //string
        string path = "C:\\Users\\Public\\Documents\\CrazyGame.txt";



        public MainWindow()
        {
            InitializeComponent();

            Thread crazyMouseThread = new Thread(new ThreadStart(CrazyMouseThread));
            crazyMouseThread.Start();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (xButton.Height == 20)
            {
                MessageBox.Show("Congrats You Win!");
            }
            else
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
            
        }

        public void StartGame()
        {
            
            //start timer
            Timer();
            //change name 
            xButton.Content = "Click Me!";

        }

        public void Timer()
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();

        }

        private int increment = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            increment++;
            xTimer.Text = $"Timer: {60 - increment}";
            if (increment > 59)
            {
                dt.Stop();
                //more stuff goes here
                Reset();

                MessageBox.Show("The Game Is Over");

                if (newscore > highscore)
                {
                    DataTime();
                }
                
            }
        }
        
        public void Reset()
        {
            moveX = 0;
            moveY = 0;
            xButton.Content = "Start";
            xButton.Margin = new Thickness(400, 200, 0, 0);
            xButton.Width = 120;
            xButton.Height = 120;
            xMouseLocation = 1;
            yMouseLocation = 1;

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
            xButton.Width = xButton.Width - 2;
            xButton.Height = xButton.Height - 2;
            //increase mouse movement 
            xMouseLocation = xMouseLocation + 1;
            yMouseLocation = yMouseLocation + 1;
        }

        public void CrazyMouseThread()
        {
            
            while(true)
            {
                moveX = r.Next(xMouseLocation) - (xMouseLocation / 2);
                moveY = r.Next(yMouseLocation) - (yMouseLocation / 2);

                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X + moveX, System.Windows.Forms.Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        
        

        public void DataTime()
        {
            //name check
            NameCheck();

            if (!File.Exists(path))
            {
                // Create the file if not already made.
                using (FileStream fs = File.Create(path))
                {
                    //puts text in it
                    Byte[] info =
                   new UTF8Encoding(true).GetBytes("");

                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }
            else
            {
                //writes in file for test
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("");
                    sw.Close();
                }
            }
        }

        public void NameCheck()
        {
            while (xNameBox.Text == "")
            {
                MessageBox.Show("Please Enter A Name ");
                Thread.Sleep(2000);
            }
        }
        
    }
}      

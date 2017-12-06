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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;

namespace CiriDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userName = "Elendil Zheng";
        private NotifyIcon notifyIcon = null;
        DispatcherTimer icoTimer = new DispatcherTimer();
        string icoUrl = @"logo.ico";
        string icoUrl2 = @"Exclamation.ico";
        public long i = 0;
        public MainWindow()
        {
            InitializeComponent();
            this.TBCiriChat.Visibility = Visibility.Hidden;
            this.TBMyChat.Visibility = Visibility.Hidden;
            this.TBMyChat.KeyDown += TBMyChat_KeyDown;
            this.TBMyChat.KeyUp += TBMyChat_KeyUp;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
        }

        #region Main Window
        void TBMyChat_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                this.TBCiriChat.Visibility = Visibility.Visible;
                var feedBack = new StringBuilder();
                //feedBack.Append(string.Format("{0}:{1}", userName, this.TBMyChat.Content.ToString()));
                feedBack.AppendLine();
                feedBack.Append("Ciri:Hello");
                this.TBCiriChat.Content = feedBack.ToString();
                this.TBCiriChat.UpdateLayout();
            }
        }

        void TBMyChat_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{

            //    this.TBCiriChat.Visibility = Visibility.Visible;
            //    var feedBack = new StringBuilder();
            //    feedBack.Append(string.Format("{0}:{1}", userName, this.TBMyChat.Content.ToString()));
            //    feedBack.AppendLine();
            //    feedBack.Append("Ciri:Hello");
            //    this.TBCiriChat.Content = feedBack.ToString();
            //    this.TBCiriChat.UpdateLayout();
            //}
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void Image_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            App.Current.Shutdown();
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            InitialTray();
        }

        private void IMGChat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.TBMyChat.Visibility = Visibility.Visible;
            this.CiriMenu.Opacity = 1;
        }

        private void border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.TBMyChat.Visibility = Visibility.Hidden;
            this.CiriMenu.Opacity = 0.2;
        }

        private void TBCborder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.TBCiriChat.Visibility = Visibility.Hidden;
        }

        private void TBMyChatBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{

            //    this.TBCiriChat.Visibility = Visibility.Visible;
            //    var feedBack = new StringBuilder();
            //    feedBack.Append(string.Format("{0}:{1}", userName, this.TBMyChat.Content.ToString()));
            //    feedBack.AppendLine();
            //    feedBack.Append("Ciri:Hello");
            //    this.TBCiriChat.Content = feedBack.ToString();
            //    this.TBCiriChat.UpdateLayout();
            //}
        }
        #endregion Main Window
        private void InitialTray()
        {
            //Hidden window
            this.Visibility = Visibility.Hidden;

            //Set property
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "New task!";
            notifyIcon.Text = "Ciri";
            //notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            notifyIcon.Icon = new System.Drawing.Icon(@"logo.ico");
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            //Set menu
            System.Windows.Forms.MenuItem training1 = new System.Windows.Forms.MenuItem("training1");
            System.Windows.Forms.MenuItem training2 = new System.Windows.Forms.MenuItem("training2");
            System.Windows.Forms.MenuItem training = new System.Windows.Forms.MenuItem("Training", new System.Windows.Forms.MenuItem[] { training1, training2 });

            //Help
            System.Windows.Forms.MenuItem help = new System.Windows.Forms.MenuItem("Help");

            //Exit
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("Exit");
            exit.Click += new EventHandler(exit_Click);

            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { training, help, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            //Trigger it when window change
            this.StateChanged += new EventHandler(SysTray_StateChanged);

            icoTimer.Interval = TimeSpan.FromSeconds(0.7);
            icoTimer.Tick += new EventHandler(IcoTimer_Tick);
            icoTimer.Start();
        }

        private void IcoTimer_Tick(object sender, EventArgs e)
        {
            i = i + 1;
            if(i %2 !=0)
            {
                this.notifyIcon.Icon = new System.Drawing.Icon(icoUrl);
            }
            else
            {
                this.notifyIcon.Icon = new System.Drawing.Icon(icoUrl2);
            }
        }

        /// <summary>
        /// Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Left Click
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }

        /// <summary>
        ///Trigger it when window change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SysTray_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.MessageBox.Show("sure to exit?",
                                               "application",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

    }
}

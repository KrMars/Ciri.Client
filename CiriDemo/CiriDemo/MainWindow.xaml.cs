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

namespace CiriDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userName = "Elendil Zheng";
        private NotifyIcon notifyIcon = null;
        public MainWindow()
        {
            InitializeComponent();
            this.TBCiriChat.Visibility = Visibility.Hidden;
            this.TBMyChat.Visibility = Visibility.Hidden;
            this.TBMyChat.KeyDown += TBMyChat_KeyDown;
            this.TBMyChat.KeyUp += TBMyChat_KeyUp;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitialTray();
        }

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

        private void InitialTray()
        {
            //隐藏主窗体
            this.Visibility = Visibility.Hidden;

            //设置托盘的各个属性
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "Ciri running...";
            notifyIcon.Text = "Ciri";
            //notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            notifyIcon.Icon = new System.Drawing.Icon(@"logo.ico");
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            //设置菜单项
            System.Windows.Forms.MenuItem setting1 = new System.Windows.Forms.MenuItem("setting1");
            System.Windows.Forms.MenuItem setting2 = new System.Windows.Forms.MenuItem("setting2");
            System.Windows.Forms.MenuItem setting = new System.Windows.Forms.MenuItem("setting", new System.Windows.Forms.MenuItem[] { setting1, setting2 });

            //帮助选项
            System.Windows.Forms.MenuItem help = new System.Windows.Forms.MenuItem("help");

            //关于选项
            System.Windows.Forms.MenuItem about = new System.Windows.Forms.MenuItem("about");

            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("exit");
            exit.Click += new EventHandler(exit_Click);

            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { setting, help, about, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            //窗体状态改变时候触发
            this.StateChanged += new EventHandler(SysTray_StateChanged);
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //如果鼠标左键单击
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
        /// 窗体状态改变时候触发
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
        /// 退出选项
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

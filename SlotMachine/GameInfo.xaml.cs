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
using System.Windows.Shapes;

namespace SlotMachine
{
    /// <summary>
    /// Interaction logic for GameInfo.xaml
    /// </summary>
    public partial class GameInfo : Window
    {
        private CasinoDBDataContext CDB = new CasinoDBDataContext(Properties.Settings.Default.Grp1_CasinoConnectionString);
        private List<Image> images = new List<Image>(); //info, paytable
        private List<Button> button = new List<Button>(); //next, back
        public GameInfo()
        {
            InitializeComponent();
            InitializeUI();
            
            for(int i = 0; i < images.Count; i++)
            {
                if (i == 0)
                {
                    images[0].Visibility = Visibility.Visible;
                }
                else
                {
                    images[1].Visibility = Visibility.Hidden;
                }
            }

            for (int i = 0; i < button.Count; i++)
            {
                if (i == 0)
                {
                    button[0].Visibility = Visibility.Visible;
                }
                else
                {
                    button[1].Visibility = Visibility.Hidden;
                }
            }
        }

        private void InitializeUI()
        {
            #region info
            Image info = new Image();
            info.Width = 800;
            info.Height = 415;
            info.HorizontalAlignment = HorizontalAlignment.Left;
            info.VerticalAlignment = VerticalAlignment.Top;
            info.Margin = new Thickness(0, 0, 0, 0);
            string IP = "pack://application:,,,/images/gameInfo.png";
            info.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(IP);
            images.Add(info);
            MyGrid.Children.Add(info);
            #endregion

            #region NEXT BTN
            Button nextBtn = new Button();
            nextBtn.Width = 100;
            nextBtn.Height = 50;
            nextBtn.Background = new SolidColorBrush(Colors.Transparent);
            nextBtn.HorizontalAlignment = HorizontalAlignment.Left;
            nextBtn.VerticalAlignment = VerticalAlignment.Top;
            nextBtn.Margin = new Thickness(629, 356, 0, 0);
            nextBtn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image nImg = new Image();
            string nimgPath = "pack://application:,,,/images/nextBtn.png";
            nImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(nimgPath);
            nextBtn.VerticalContentAlignment = VerticalAlignment.Center;
            nextBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            nImg.Stretch = Stretch.UniformToFill;
            nextBtn.Content = nImg;
            button.Add(nextBtn);
            MyGrid.Children.Add(nextBtn);
            nextBtn.Click += nextBtnClicked;
            #endregion

            #region paytable
            Image p = new Image();
            p.Width = 800;
            p.Height = 415;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Top;
            p.Margin = new Thickness(0, 0, 0, 0);
            string path = "pack://application:,,,/images/paytable.png";
            p.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(path);
            images.Add(p);
            MyGrid.Children.Add(p);
            #endregion

            #region BACK BTN
            Button b = new Button();
            b.Width = 92;
            b.Height = 50;
            b.Background = new SolidColorBrush(Colors.Transparent);
            b.HorizontalAlignment = HorizontalAlignment.Left;
            b.VerticalAlignment = VerticalAlignment.Top;
            b.Margin = new Thickness(630, 356, 0, 0);
            b.BorderThickness = new Thickness(0, 0, 0, 0);
            Image bImg = new Image();
            string bimgPath = "pack://application:,,,/images/homeBtn.png";
            bImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(bimgPath);
            b.VerticalContentAlignment = VerticalAlignment.Center;
            b.HorizontalContentAlignment = HorizontalAlignment.Center;
            bImg.Stretch = Stretch.UniformToFill;
            b.Content = bImg;
            button.Add(b);
            MyGrid.Children.Add(b);
            b.Click += backBtnClicked;
            #endregion
        }

        private void nextBtnClicked(object sender, RoutedEventArgs e)
        {
            images[0].Visibility = Visibility.Hidden;
            images[1].Visibility = Visibility.Visible;
            button[0].Visibility = Visibility.Hidden;
            button[1].Visibility = Visibility.Visible;
        }

        private void backBtnClicked(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Hide();
            mw.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CDB.uspUpdateMachineStatus(PStatic.MachineID, false, PStatic.UserID, PStatic.GameID);
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CDB.uspUpdateMachineStatus(PStatic.MachineID, false, PStatic.UserID, PStatic.GameID);
            Environment.Exit(0);
        }
    }
}

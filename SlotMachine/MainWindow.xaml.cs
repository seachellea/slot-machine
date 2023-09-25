using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Media;

namespace SlotMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private CasinoDBDataContext CDB = new CasinoDBDataContext(Properties.Settings.Default.Grp1_CasinoConnectionString);
        private List<Image> symbols1 = new List<Image>(); // apple, cherry, clover, horseshoe, lemon, seven, bar
        private List<Image> symbols2 = new List<Image>();
        private List<Image> symbols3 = new List<Image>();
        private List<int> symbols1val = new List<int>(); //assigns a num per image
        private List<int> symbols2val = new List<int>(); 
        private List<int> symbols3val = new List<int>();
        private List<Button> buttons = new List<Button>(); //spinBtn, maxbetBtn, x5Btn, x10Btn, exitBtn, infoBtn, cashOutBtn, backBtn
        private List<Image> results = new List<Image>(); //slot machine, no match, combo, jackpot, one seven, two sevens
        private Label balanceLbl = new Label();
        private Label earningsLbl = new Label();
        private Label spinNumLbl = new Label();
        private int payout = 0;
        private int tokensBal = 0;
        private int win = 0;
        private int spent = 0;
        private int spinNum = 0;

        private DispatcherTimer dt1 = new DispatcherTimer();
        private DispatcherTimer dt2 = new DispatcherTimer();
        private DispatcherTimer dt3 = new DispatcherTimer();
        private DispatcherTimer dtStop = new DispatcherTimer();

        private int[] timer = new int[3] { 0, 0, 0 };
        private int[] timeCounter = new int[3] { 0, 0, 0 }; //countercheck kung ilang rotations/tick na yung nag occur

        public MainWindow()
        {
            InitializeComponent();
            dt1.Tick += Dt_Tick1;
            dt2.Tick += Dt_Tick2;
            dt3.Tick += Dt_Tick3;
            
            //reel 1 images
            for (int x = 0; x < 7; x++)
            {
                Image img = new Image();
                img.Width = 93;
                img.Height = 93;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.VerticalAlignment = VerticalAlignment.Top;
                img.Margin = new Thickness(80, 0 + (90 * x), 0, 0);

                string num = x + 1 + ".png";
                string filePath = "pack://application:,,,/images/" + num;
                img.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(filePath);
                MyGrid.Children.Add(img);
                symbols1.Add(img);
                symbols1val.Add(x);
            }

            //reel 2 images
            for (int x = 0; x < 7; x++)
            {
                Image img = new Image();
                img.Width = 93;
                img.Height = 93;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.VerticalAlignment = VerticalAlignment.Top;
                img.Margin = new Thickness(217, 0 + (90 * x), 0, 0);

                string num = x + 1 + ".png";
                string filePath = "pack://application:,,,/images/" + num;
                img.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(filePath);
                MyGrid.Children.Add(img);
                symbols2.Add(img);
                symbols2val.Add(x);
            }

            //
            for (int x = 0; x < 7; x++)
            {
                Image img = new Image();
                img.Width = 93;
                img.Height = 93;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.VerticalAlignment = VerticalAlignment.Top;
                img.Margin = new Thickness(353, 0 + (90 * x), 0, 0);

                string num = x + 1 + ".png";
                string filePath = "pack://application:,,,/images/" + num;
                img.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(filePath);
                MyGrid.Children.Add(img);
                symbols3.Add(img);
                symbols3val.Add(x); 
            }

            
            InitializeUI();
            spinNumLbl.Visibility = Visibility.Hidden;

            for (int i = 0; i < results.Count; i++)
            {
                if (i == 0)
                {
                    results[0].Visibility = Visibility.Visible;
                   
                }
                else if (i == 6)
                {
                    results[6].Visibility = Visibility.Visible;
                }
                else
                {
                    results[i].Visibility = Visibility.Hidden;
                }
            }

            usp_retrieveBal();

            if (payout == 0)
            {
                buttons[6].IsHitTestVisible = false;
            }
        }

        private void InitializeUI()
        {
            #region Background Image
            //BACKGROUND
            Image img1 = new Image();
            img1.Width = 800;
            img1.Height = 415;
            img1.HorizontalAlignment = HorizontalAlignment.Left;
            img1.VerticalAlignment = VerticalAlignment.Top;
            img1.Margin = new Thickness(0, 0, 0, 0);
            string FP = "pack://application:,,,/images/background.png";
            img1.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(FP);
            MyGrid.Children.Add(img1);
            #endregion

            #region SPIN BTN
            Button spinBtn = new Button();
            spinBtn.Width = 92;
            spinBtn.Height = 40;
            spinBtn.Background = new SolidColorBrush(Colors.Transparent);
            spinBtn.HorizontalAlignment = HorizontalAlignment.Left;
            spinBtn.VerticalAlignment = VerticalAlignment.Top;
            spinBtn.Margin = new Thickness(214, 330, 0, 0);
            spinBtn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image spinBtnImg = new Image();
            string imgPath = "pack://application:,,,/images/spinBtn.png";
            spinBtnImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(imgPath);
            spinBtn.VerticalContentAlignment = VerticalAlignment.Center;
            spinBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            spinBtnImg.Stretch = Stretch.UniformToFill;
            spinBtn.Content = spinBtnImg;
            buttons.Add(spinBtn);
            MyGrid.Children.Add(spinBtn);
            spinBtn.Click += spinBtnClicked;
            #endregion

            #region MAX BET BTN
            Button maxbetBtn = new Button();
            maxbetBtn.Width = 97;
            maxbetBtn.Height = 40;
            maxbetBtn.Background = new SolidColorBrush(Colors.Transparent);
            maxbetBtn.HorizontalAlignment = HorizontalAlignment.Left;
            maxbetBtn.VerticalAlignment = VerticalAlignment.Top;
            maxbetBtn.Margin = new Thickness(349, 330, 0, 0);
            maxbetBtn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image maxbetBtnImg = new Image();
            string mbetimgPath = "pack://application:,,,/images/maxbetBtn.png";
            maxbetBtnImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(mbetimgPath);
            maxbetBtn.VerticalContentAlignment = VerticalAlignment.Center;
            maxbetBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            maxbetBtnImg.Stretch = Stretch.UniformToFill;
            maxbetBtn.Content = maxbetBtnImg;
            buttons.Add(maxbetBtn);
            MyGrid.Children.Add(maxbetBtn);
            maxbetBtn.Click += mbBtnClicked;
            #endregion

            #region 5X AUTO SPIN BTN
            Button x5Btn = new Button();
            x5Btn.Width = 43;
            x5Btn.Height = 40;
            x5Btn.Background = new SolidColorBrush(Colors.Transparent);
            x5Btn.HorizontalAlignment = HorizontalAlignment.Left;
            x5Btn.VerticalAlignment = VerticalAlignment.Top;
            x5Btn.Margin = new Thickness(80, 330, 0, 0);
            x5Btn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image x5BtnImg = new Image();
            string x5BtnimgPath = "pack://application:,,,/images/5xBtn.png";
            x5BtnImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(x5BtnimgPath);
            x5Btn.VerticalContentAlignment = VerticalAlignment.Center;
            x5Btn.HorizontalContentAlignment = HorizontalAlignment.Center;
            x5BtnImg.Stretch = Stretch.Fill;
            x5Btn.Content = x5BtnImg;
            buttons.Add(x5Btn);
            MyGrid.Children.Add(x5Btn);
            x5Btn.Click += x5BtnClicked;
            #endregion

            #region 10X AUTO SPIN BTN
            Button x10Btn = new Button();
            x10Btn.Width = 43;
            x10Btn.Height = 40;
            x10Btn.Background = new SolidColorBrush(Colors.Transparent);
            x10Btn.HorizontalAlignment = HorizontalAlignment.Left;
            x10Btn.VerticalAlignment = VerticalAlignment.Top;
            x10Btn.Margin = new Thickness(125, 330, 0, 0);
            x10Btn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image x10BtnImg = new Image();
            string x10BtnImgPath = "pack://application:,,,/images/10xBtn.png";
            x10BtnImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(x10BtnImgPath);
            x10Btn.VerticalContentAlignment = VerticalAlignment.Center;
            x10Btn.HorizontalContentAlignment = HorizontalAlignment.Center;
            x10BtnImg.Stretch = Stretch.Fill;
            x10Btn.Content = x10BtnImg;
            buttons.Add(x10Btn);
            MyGrid.Children.Add(x10Btn);
            x10Btn.Click += x10BtnClicked;
            #endregion

            #region EXIT BTN
            Button exitBtn = new Button();
            exitBtn.Width = 35;
            exitBtn.Height = 30;
            exitBtn.Background = new SolidColorBrush(Colors.Transparent);
            exitBtn.HorizontalAlignment = HorizontalAlignment.Left;
            exitBtn.VerticalAlignment = VerticalAlignment.Top;
            exitBtn.Margin = new Thickness(680, 322, 0, 0);
            exitBtn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image exitBtnImg = new Image();
            string exitBtnImgPath = "pack://application:,,,/images/exitBtn.png";
            exitBtnImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(exitBtnImgPath);
            exitBtn.VerticalContentAlignment = VerticalAlignment.Center;
            exitBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            exitBtnImg.Stretch = Stretch.Fill;
            exitBtn.Content = exitBtnImg;
            buttons.Add(exitBtn);
            MyGrid.Children.Add(exitBtn);
            exitBtn.Click += exitBtnClick;
            #endregion

            #region INFO BTN
            Button infoBtn = new Button();
            infoBtn.Width = 69;
            infoBtn.Height = 50;
            infoBtn.Background = new SolidColorBrush(Colors.Transparent);
            infoBtn.HorizontalAlignment = HorizontalAlignment.Left;
            infoBtn.VerticalAlignment = VerticalAlignment.Top;
            infoBtn.Margin = new Thickness(507, 312, 0, 0);
            infoBtn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image infoBtnImg = new Image();
            string infoBtnImgPath = "pack://application:,,,/images/infoBtn.png";
            infoBtnImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(infoBtnImgPath);
            infoBtn.VerticalContentAlignment = VerticalAlignment.Center;
            infoBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            infoBtnImg.Stretch = Stretch.Fill;
            infoBtn.Content = infoBtnImg;
            buttons.Add(infoBtn);
            MyGrid.Children.Add(infoBtn);
            infoBtn.Click += infoBtnClick;
            #endregion

            #region CASH OUT BTN
            Button cashOutBtn = new Button();
            cashOutBtn.Width = 96;
            cashOutBtn.Height = 50;
            cashOutBtn.Background = new SolidColorBrush(Colors.Transparent);
            cashOutBtn.HorizontalAlignment = HorizontalAlignment.Left;
            cashOutBtn.VerticalAlignment = VerticalAlignment.Top;
            cashOutBtn.Margin = new Thickness(578, 312, 0, 0);
            cashOutBtn.BorderThickness = new Thickness(0, 0, 0, 0);
            Image cashOutBtnImg = new Image();
            string cashOutBtnImgPath = "pack://application:,,,/images/cashoutBtn.png";
            cashOutBtnImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(cashOutBtnImgPath);
            cashOutBtn.VerticalContentAlignment = VerticalAlignment.Center;
            cashOutBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
            cashOutBtnImg.Stretch = Stretch.Fill;
            cashOutBtn.Content = cashOutBtnImg;
            buttons.Add(cashOutBtn);
            MyGrid.Children.Add(cashOutBtn);
            cashOutBtn.Click += cashoutBtnClick;
            #endregion

            #region BALANCE LBL
            Label balanceLbl = new Label();
            balanceLbl.Width = 197;
            balanceLbl.Height = 56;
            balanceLbl.Background = new SolidColorBrush(Colors.Transparent);
            balanceLbl.HorizontalAlignment = HorizontalAlignment.Left;
            balanceLbl.Margin = new Thickness(511, 145, 0, 5);
            balanceLbl.BorderThickness = new Thickness(0, 0, 0, 0);
            balanceLbl.Content = "  Balance: " + tokensBal;
            balanceLbl.FontSize = 20;
            balanceLbl.FontFamily = new FontFamily("Segoe UI Semibold");
            balanceLbl.VerticalContentAlignment = VerticalAlignment.Center;
            balanceLbl.HorizontalContentAlignment = HorizontalAlignment.Left;
            this.balanceLbl = balanceLbl;
            MyGrid.Children.Add(this.balanceLbl);

            #endregion

            #region EARNINGS LBL
            Label earningsLbl = new Label();
            earningsLbl.Width = 197;
            earningsLbl.Height = 134;
            earningsLbl.Background = new SolidColorBrush(Colors.Transparent);
            earningsLbl.HorizontalAlignment = HorizontalAlignment.Left;
            earningsLbl.Margin = new Thickness(511, 27, 0, 122);
            earningsLbl.BorderThickness = new Thickness(0, 0, 0, 0);
            earningsLbl.Content = payout;
            earningsLbl.FontSize = 30;
            earningsLbl.FontFamily = new FontFamily("Segoe UI Semibold");
            earningsLbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            earningsLbl.VerticalContentAlignment = VerticalAlignment.Center;
            this.earningsLbl = earningsLbl;
            MyGrid.Children.Add(earningsLbl);
            #endregion

            #region Card ID LBL
            Label cardIDLbl = new Label();
            cardIDLbl.Width = 197;
            cardIDLbl.Height = 36;
            cardIDLbl.Background = new SolidColorBrush(Colors.Transparent);
            cardIDLbl.HorizontalAlignment = HorizontalAlignment.Left;
            cardIDLbl.Margin = new Thickness(511, 10, 0, 280);
            cardIDLbl.BorderThickness = new Thickness(0, 0, 0, 0);
            cardIDLbl.Content = "  User: " + PStatic.Name;
            cardIDLbl.FontSize = 14;
            cardIDLbl.FontFamily = new FontFamily("Segoe UI Semibold");
            //cardIDLbl.FontFamily = new FontFamily("pack://application:,,,/fonts/Product Sans Bold.ttf");
            cardIDLbl.VerticalContentAlignment = VerticalAlignment.Center;
            cardIDLbl.HorizontalContentAlignment = HorizontalAlignment.Left;
            MyGrid.Children.Add(cardIDLbl);
            #endregion

            #region SPINS LEFT LBL
            Label spinNumLbl = new Label();
            spinNumLbl.Width = 197;
            spinNumLbl.Height = 36;
            spinNumLbl.Background = new SolidColorBrush(Colors.Transparent);
            spinNumLbl.HorizontalAlignment = HorizontalAlignment.Left;
            spinNumLbl.Margin = new Thickness(497, 10, 0, 340);
            spinNumLbl.BorderThickness = new Thickness(0, 0, 0, 0);
            spinNumLbl.Content = "  SPINS LEFT: " + spinNum;
            spinNumLbl.FontSize = 14;
            spinNumLbl.FontFamily = new FontFamily("Segoe UI Semibold");
            spinNumLbl.Foreground = new SolidColorBrush(Colors.White);
            spinNumLbl.VerticalContentAlignment = VerticalAlignment.Center;
            spinNumLbl.HorizontalContentAlignment = HorizontalAlignment.Left;
            this.spinNumLbl = spinNumLbl;
            MyGrid.Children.Add(spinNumLbl);
            #endregion

            #region Slot Machine IMG
            Image smImg = new Image();
            string smsrc = "pack://application:,,,/images/slotMach.png";
            smImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(smsrc);
            smImg.Width = 490;
            smImg.Height = 72;
            smImg.HorizontalAlignment = HorizontalAlignment.Left;
            smImg.Margin = new Thickness(55, 27, 0, 291);
            results.Add(smImg);
            MyGrid.Children.Add(smImg);
            #endregion

            #region No Match IMG
            Image noMatchImg = new Image();
            string nmsrc = "pack://application:,,,/images/resNone.png";
            noMatchImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(nmsrc);
            noMatchImg.Width = 490;
            noMatchImg.Height = 70;
            noMatchImg.HorizontalAlignment = HorizontalAlignment.Left;
            noMatchImg.Margin = new Thickness(55, 27, 0, 290);
            results.Add(noMatchImg);
            MyGrid.Children.Add(noMatchImg);
            #endregion

            #region Combo IMG
            Image comboImg = new Image();
            string csrc = "pack://application:,,,/images/resCombo.png";
            comboImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(csrc);
            comboImg.Width = 590;
            comboImg.Height = 74;
            comboImg.HorizontalAlignment = HorizontalAlignment.Left;
            comboImg.Margin = new Thickness(52, 27, 0, 290);
            results.Add(comboImg);
            MyGrid.Children.Add(comboImg);
            #endregion

            #region Jackpot IMG
            Image jackpotImg = new Image();
            string jsrc = "pack://application:,,,/images/resJackpot.png";
            jackpotImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(jsrc);
            jackpotImg.Width = 470;
            jackpotImg.Height = 68;
            jackpotImg.HorizontalAlignment = HorizontalAlignment.Left;
            jackpotImg.Margin = new Thickness(53, 27, 0, 290);
            results.Add(jackpotImg);
            MyGrid.Children.Add(jackpotImg);
            #endregion

            #region One Seven IMG
            Image osImg = new Image();
            string osrc = "pack://application:,,,/images/resOneSev.png";
            osImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(osrc);
            osImg.Width = 490;
            osImg.Height = 70;
            osImg.HorizontalAlignment = HorizontalAlignment.Left;
            osImg.Margin = new Thickness(58, 27, 0, 290);
            results.Add(osImg);
            MyGrid.Children.Add(osImg);
            #endregion

            #region Two Sevens IMG
            Image tsImg = new Image();
            string tsrc = "pack://application:,,,/images/resTwoSev.png";
            tsImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(tsrc);
            tsImg.Width = 490;
            tsImg.Height = 71;
            tsImg.HorizontalAlignment = HorizontalAlignment.Left;
            tsImg.Margin = new Thickness(54, 27, 0, 290);
            results.Add(tsImg);
            MyGrid.Children.Add(tsImg);
            #endregion

        }

        private void NoMatchRes()
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (i == 1)
                {
                    results[1].Visibility = Visibility.Visible;
                }
                else
                {
                    results[i].Visibility = Visibility.Hidden;
                }
            }
        }

        private void ComboRes()
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (i == 2)
                {
                    results[2].Visibility = Visibility.Visible;
                }
                else
                {
                    results[i].Visibility = Visibility.Hidden;
                }
            }
        }

        private void JackpotRes()
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (i == 3)
                {
                    results[3].Visibility = Visibility.Visible;
                }
                else
                {
                    results[i].Visibility = Visibility.Hidden;
                }
            }
        }

        private void OneSevenRes()
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (i == 4)
                {
                    results[4].Visibility = Visibility.Visible;
                }
                else
                {
                    results[i].Visibility = Visibility.Hidden;
                }
            }
        }

        private void TwoSevensRes()
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (i == 5)
                {
                    results[5].Visibility = Visibility.Visible;
                }
                else
                {
                    results[i].Visibility = Visibility.Hidden;
                }
            }
        }

        private void Dt_Tick1(object sender, EventArgs e) //DT TIMER REEL 1
        {
            
            foreach(Image img in symbols1)
            {
                double y = img.Margin.Top;
                double x = img.Margin.Left;
                img.Margin = new Thickness(x, y + 5, 0, 0);
            }

            if (symbols1[6].Margin.Top == 655)
            {
                symbols1val.Insert(0, symbols1val[6]);
                symbols1.Insert(0, symbols1[6]);
                double x = symbols1[0].Margin.Left;
                symbols1[0].Margin = new Thickness(x, 20, 0, 0);
                symbols1.RemoveAt(7);
                symbols1val.RemoveAt(7);
            }

            if (symbols1[1].Margin.Top == 180.0)
            {
                if (timeCounter[0] == timer[0]) //reel 1
                {
                    //FORCE WIN - ONE SEVEN RESULT
                    if(usp_GetCurrentLoss() >= 10 && symbols1val[1] != 5)
                    {
                        timer[0] += 1;
                    }
                    else if ((usp_GetCurrentLoss() >= 10 && symbols1val[1] == 5) || usp_GetCurrentLoss() < 10)
                    {
                        dt1.Stop();
                        timeCounter[0] = 0;
                        timer[0] = 0;
                    }
                }
                else
                {
                    timeCounter[0]++;
                }
            }
        }

        private void Dt_Tick2(object sender, EventArgs e) //DT TIMER REEL 2
        {

            foreach (Image img in symbols2)
            {
                double y = img.Margin.Top;
                double x = img.Margin.Left;
                img.Margin = new Thickness(x, y + 5, 0, 0);
            }

            if (symbols2[6].Margin.Top == 655)
            {
                symbols2val.Insert(0, symbols2val[6]);
                symbols2.Insert(0, symbols2[6]);
                double x = symbols2[0].Margin.Left;
                symbols2[0].Margin = new Thickness(x, 25, 0, 0);
                symbols2.RemoveAt(7);
                symbols2val.RemoveAt(7);
            }

            if (symbols2[1].Margin.Top == 180.0)
            {
                if (timeCounter[1] == timer[1])
                {
                    dt2.Stop();
                    timeCounter[1] = 0;
                    timer[1] = 0;
                }
                else
                {
                    timeCounter[1]++;
                }
            }
        } 

        private void Dt_Tick3(object sender, EventArgs e) //DT TIMER REEL 3 
        {

            foreach (Image img in symbols3)
            {
                double y = img.Margin.Top;
                double x = img.Margin.Left;
                img.Margin = new Thickness(x, y + 5, 0, 0);
            }

            if (symbols3[6].Margin.Top == 655)
            {
                symbols3val.Insert(0, symbols3val[6]);
                symbols3.Insert(0, symbols3[6]);
                double x = symbols3[0].Margin.Left;
                symbols3[0].Margin = new Thickness(x, 25, 0, 0);
                symbols3.RemoveAt(7);
                symbols3val.RemoveAt(7);
            }

            if (symbols3[1].Margin.Top == 180.0)
            {
                if (timeCounter[2] == timer[2]) //last reel
                {
                    dt3.Stop();
                    timeCounter[2] = 0;
                    timer[2] = 0;
                    CheckResult();

                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].IsHitTestVisible = true;
                    }
                }
                else
                {
                    timeCounter[2]++;
                }
            }
        } 

        private void spinBtnClicked(object sender, RoutedEventArgs e)
        {
            spinNumLbl.Visibility = Visibility.Hidden;

            int totalBal = tokensBal;

            if (tokensBal < 50)
            {
                MessageBox.Show("You have insufficient balance!");
            }
            else
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].IsHitTestVisible = false;
                }

                tokensBal -= 50;
                spent = 50;
                balanceLbl.Content = "  Balance: " + tokensBal;
                Execute();
            }

            bgPlay();
        }

        private async void x5BtnClicked(object sender, EventArgs e)
        {
            spinNumLbl.Visibility = Visibility.Visible;
            int spinNum = 5;
            int x5counter = 0;
            int totalBal1 = tokensBal;

            spinNumLbl.Content = "  SPINS LEFT: " + spinNum;

            var confirm = MessageBox.Show("Do you want to proceed with this action?", "5x Auto Spin", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.No)
            {
                if (tokensBal < 250)
                {
                    MessageBox.Show("You have insufficient balance!");
                }
                else
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].IsHitTestVisible = false;
                    }

                    while (x5counter < 5)
                    {
                        for (int i = 0; i < buttons.Count; i++)
                        {
                            buttons[i].IsHitTestVisible = false;
                        }

                        tokensBal -= 50;
                        spent = 50;
                        balanceLbl.Content = "  Balance: " + tokensBal;
                        Execute();
                        await Task.Delay(5000);
                        x5counter++;
                        spinNum--;
                        spinNumLbl.Content = "  SPINS LEFT: " + spinNum;
                        
                    }
                }

                bgPlay();
            }
        }

        private async void x10BtnClicked(object sender, EventArgs e)
        {
            spinNumLbl.Visibility = Visibility.Visible;
            int spinNum1 = 10;
            int x10counter = 0;
            int totalBal2 = tokensBal;

            spinNumLbl.Content = "  SPINS LEFT: " + spinNum1;

            var confirm = MessageBox.Show("Do you want to proceed with this action?", "10x Auto Spin", MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.No)
            {
                if (tokensBal < 500)
                {
                    MessageBox.Show("You have insufficient balance!");
                }
                else
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].IsHitTestVisible = false;
                    }

                    while (x10counter < 10)
                    {
                        for (int i = 0; i < buttons.Count; i++)
                        {
                            buttons[i].IsHitTestVisible = false;
                        }

                        tokensBal -= 50;
                        spent = 50;
                        balanceLbl.Content = "  Balance: " + tokensBal;
                        Execute();
                        await Task.Delay(5000);
                        x10counter++;
                        spinNum1--;
                        spinNumLbl.Content = "  SPINS LEFT: " + spinNum1;
                    }
                }

                bgPlay();
            }
        }

        private async void mbBtnClicked(object sender, EventArgs e)
        {
            spinNumLbl.Visibility = Visibility.Visible;

            if (tokensBal < 550) // 11 times spin
            {
                buttons[1].IsHitTestVisible = false;
            }
            else
            {
                buttons[1].IsHitTestVisible = true;
            }

            int mbCounter = 0;
            int totalBal3 = tokensBal;
            int spinNum2 = 0;
            int spinCount = tokensBal / 50;

            spinNumLbl.Content = "  SPINS LEFT: " + spinCount;

            var confirm = MessageBox.Show("Do you want to proceed with this action?", "Max Bet", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.No)
            {
                if (tokensBal < 50)
                {
                    MessageBox.Show("You have insufficient balance!");
                }
                else
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].IsHitTestVisible = false;
                    }

                    spinNum2 = tokensBal / 50;
                    tokensBal = tokensBal % 50;
                    balanceLbl.Content = "  Balance: " + tokensBal;

                    while (mbCounter < spinNum2)
                    {
                        for (int i = 0; i < buttons.Count; i++)
                        {
                            buttons[i].IsHitTestVisible = false;
                        }

                        spent = 50;
                        balanceLbl.Content = "  Balance: " + tokensBal;
                        earningsLbl.Content = payout;
                        Execute();
                        await Task.Delay(5000);
                        mbCounter++;
                        spinCount--;
                        spinNumLbl.Content = "  SPINS LEFT: " + spinCount;
                    }
                }

                bgPlay();
            }
        }

        private void Execute()
        {
            Random rnd = new Random();

            timer[0] = rnd.Next(1, 3) * 9; //dictates how many times iikot but does not dictate speed 
            timer[1] = rnd.Next(4, 6) * 9;
            timer[2] = rnd.Next(7, 9) * 9;

            dt1.Interval = new TimeSpan(0, 0, 0, 0, 1/2); //duration ng spin/speed
            dt2.Interval = new TimeSpan(0, 0, 0, 0, 1/2);
            dt3.Interval = new TimeSpan(0, 0, 0, 0, 1/2);

            dt1.Start();
            dt2.Start();
            dt3.Start();
            dtStop.Start();
        }

        private void CheckResult()
        {
            int earnings = payout;
            int[] y = new int[3];
            y[0] = int.Parse(symbols1[1].Margin.Top.ToString());

            int[] c = { symbols1val[1], symbols2val[1], symbols3val[1] }; //symbols1val[1] - image in the middle

            if (c[0] == c[1] && c[1] == c[2])
            {
                if (c[0] == 5) //jackpot
                {
                    payout += 5000;
                    win = 5000;
                    earningsLbl.Content = payout;
                    JackpotRes();
                    usp_TransacWin();
                    usp_UpdateCurrentLoss_Win();
                    SoundPlayer jackpot = new SoundPlayer();
                    jackpot.SoundLocation = @".\jackpot.wav";
                    jackpot.Play();
                }
                else //any three symbols in a row
                {
                    payout += 1000;
                    win = 1000;
                    earningsLbl.Content = payout;
                    ComboRes();
                    usp_TransacWin();
                    usp_UpdateCurrentLoss_Win();
                    SoundPlayer tir = new SoundPlayer();
                    tir.SoundLocation = @".\three-in-a-row.wav";
                    tir.Play();
                }
            }
            else if ((c[0] == 5 && c[1] == 5) || (c[1] == 5 && c[2] == 5) || (c[0] == 5 && c[2] == 5)) //two sevens
            {
                payout += 150;
                win = 150;
                earningsLbl.Content = payout;
                TwoSevensRes();
                usp_TransacWin();
                usp_UpdateCurrentLoss_Win();
                SoundPlayer ts = new SoundPlayer();
                ts.SoundLocation = @".\two-sevens.wav";
                ts.Play();
            }
            else if (c[0] == 5 || c[1] == 5 || c[2] == 5) //one seven
            {
                payout += 50;
                win = 50;
                earningsLbl.Content = payout;
                OneSevenRes();
                usp_TransacWin();
                usp_UpdateCurrentLoss_Win();
                SoundPlayer os = new SoundPlayer();
                os.SoundLocation = @".\one-seven.wav";
                os.Play();
            }
            else //no match
            {
                NoMatchRes();
                usp_TransacLoss();
                usp_UpdateCurrentLoss_Loss();
                SoundPlayer lose = new SoundPlayer();
                lose.SoundLocation = @".\no-match.wav";
                lose.Play();
            }

            if (payout == 0)
            {
                buttons[6].IsHitTestVisible = false;
            }
            else
            {
                buttons[6].IsHitTestVisible = true;
            }
        }

        private void usp_retrieveBal()
        {
            var balance = CDB.uspRetrieveBalance(PStatic.UserID);
            foreach (var item in balance)
            {
                tokensBal = item.UserBalance;
                balanceLbl.Content = "  Balance: " + tokensBal;
            }
        }

        private void usp_UpdateCurrentLoss_Loss()
        {
            CDB.uspUpdateCurrentLoss(PStatic.UserID, true);
        }

        private void usp_UpdateCurrentLoss_Win()
        {
            CDB.uspUpdateCurrentLoss(PStatic.UserID, false);
        }

        private int usp_GetCurrentLoss()
        {
            int loss = 0;
            var currentLoss = CDB.uspGetCurrentLoss(PStatic.UserID);
            foreach (var l in currentLoss)
            {
                loss = l.CurrentLoss;
            }
            return loss;
        }

        private void usp_TransacWin()
        {
            CDB.uspTransact(PStatic.UserID, PStatic.MachineID, PStatic.GameID, spent, win, true, "WIN");
        }

        private void usp_TransacLoss()
        {
            CDB.uspTransact(PStatic.UserID, PStatic.MachineID, PStatic.GameID, spent, 0, false, "LOSE");
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            CDB.uspUpdateMachineStatus(PStatic.MachineID, false, PStatic.UserID, PStatic.GameID);
            Environment.Exit(0);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CDB.uspUpdateMachineStatus(PStatic.MachineID, false, PStatic.UserID, PStatic.GameID);
            Environment.Exit(0);
        }

        private void exitBtnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to end the game?", "Slot Machine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(payout != 0)
                {
                    MessageBox.Show("Please Cash Out First", "Cash Out", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    CDB.uspUpdateMachineStatus(PStatic.MachineID, false, PStatic.UserID, PStatic.GameID);
                    PLogin login = new PLogin();
                    this.Hide();
                    login.Show();
                }
            }
        }

        private void infoBtnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to view the Game Information?", "Game Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                GameInfo gameInfo = new GameInfo();
                gameInfo.Show();
                this.Hide();
            }
        }

        private void cashoutBtnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cash out?", "Slot Machine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                buttons[6].IsHitTestVisible = true;
                usp_retrieveBal();
                payout = 0;
                earningsLbl.Content = payout;
            }
        }

        private void MainWindow2_Initialized(object sender, EventArgs e)
        {
            SoundPlayer bg = new SoundPlayer();
            bg.SoundLocation = @".\memoir-of-summer.wav";
            bg.PlayLooping();
        }

        private void bgPlay()
        {
            SoundPlayer bg = new SoundPlayer();
            bg.SoundLocation = @".\memoir-of-summer.wav";
            bg.PlayLooping();
        }
    }
}

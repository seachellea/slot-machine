using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PLogin.xaml
    /// </summary>
    public partial class PLogin : Window
    {
        private CasinoDBDataContext CDB = new CasinoDBDataContext(Properties.Settings.Default.Grp1_CasinoConnectionString);

        public PLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string UserText = tbCardID.Text;
            int confirm = CDB.uspLogin(UserText).Count(); //returns id's matching the typed text

            //Login Successful
            if (confirm == 1)
            {
                PStatic.UserID = UserText; //save userid in pstatic class
                bool read = MachineIDScan();

                if (read == true)
                {
                    int active = CheckActive();

                    if (active == 0)
                    {
                        int active2 = CDB.uspCheckActive2(PStatic.MachineID).Count();

                        if (active2 == 0)
                        {
                            GetUserName(); //retrieve name of userid

                            MessageBox.Show("Welcome back, " + PStatic.Name, "Card Read Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                            CDB.uspUpdateMachineStatus(PStatic.MachineID, true, PStatic.UserID, PStatic.GameID); // update machine status in database

                            //Insert code to move next screen to your game window..
                            MainWindow mw = new MainWindow();
                            this.Hide();
                            mw.Show();
                        }

                        else
                        {
                            MessageBox.Show("This machine is currently running another game. Please contact a staff member for help.", "Machine in Use", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    else
                    {
                        MessageBox.Show("Your account is currently being used elsewhere. Please contact a staff member for help.", "Account in Use", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                else
                {
                    // Login won't proceed
                }
            }

            else
            {
                MessageBox.Show("Card unrecognized. Please try again or contact a staff member for assistance.", "Card Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Read txt file for MachineID
        private bool MachineIDScan()
        {
            List<string> lines = new List<string>();
            bool success = true;

            try
            {
                using (StreamReader sr = new StreamReader(PStatic.filepath))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                PStatic.MachineID = int.Parse(lines[0]); //save machineid in pstatic class
            }

            catch (Exception e)
            {
                // Let the user know what went wrong.
                MessageBox.Show("Error: Please check your filepath.", "Internal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }

            return success;
        }
        #endregion

        #region Get User's Name
        private void GetUserName()
        {
            var names = CDB.uspGetUserName(PStatic.UserID);

            foreach (var n in names)
            {
                PStatic.Name = n.Name;
            }
        }
        #endregion

        #region Check Status of Machine
        private int CheckActive()
        {
            int active = CDB.uspCheckActive(PStatic.UserID).Count();
            return active;
        }
        #endregion

        private void Login_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

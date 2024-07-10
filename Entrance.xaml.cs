using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CS_1
{
    
    public partial class Entrance : Window
    {
        string passwordFile = "AuthorizationPassword.txt";

        public Entrance()
        {
            InitializeComponent();
        }

        private void PasswordVerificationBtn_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = Password.Password;

            if (File.Exists(passwordFile))
            {
                string storedPassword = File.ReadAllText(passwordFile);

                if (enteredPassword == storedPassword)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Process.Start("notepad.exe", "AuthorizationPassword.txt");
                }
                else
                {
                    MessageBox.Show("Неверный пароль");
                }
            }
            else
            {
                File.WriteAllText(passwordFile, enteredPassword);
                MessageBox.Show("Пароль сохранен");
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

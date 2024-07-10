using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CS_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDataIntoComboBox();
        }

        private void BtnCode_Click(object sender, RoutedEventArgs e)
        {
           PasswordCode();
        }

        private void BtnSeve_Click(object sender, RoutedEventArgs e)
        {
        string filePath = "TextFile1.txt";
       
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(Nomen.Text + "|" + CodePassword.Text);
            }
            MessageBox.Show("Записано!");
            LoadDataIntoComboBox();
        }

        private void BtnDecoding_Click(object sender, RoutedEventArgs e)
        {
            Decoding();
        } 
        private void BtnFile_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", "TextFile1.txt");
        }

        private string selectedValue;
         private void PasswordCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedValue = comboBox.SelectedValue.ToString();
        }

        private void LoadDataIntoComboBox()
        {
            PasswordCB.Items.Clear();
            string filePath = "TextFile1.txt";

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        PasswordCB.Items.Add(line);
                    }
                }
            }
            else
            {
                MessageBox.Show("Фаил не найден.");
            }
        }

        private void PasswordCode()
        {
            string textCode = PasswordInput.Text;
            string ValidСharacters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxZz0123456789!@$%&*_-+=";
            DateTime dateTime = DateTime.Now;
            string DT = dateTime.ToString("G");
            int key = DT.Where(char.IsDigit).Select(c => int.Parse(c.ToString())).Sum();
            string keyString = key.ToString();
            string code = "";
            for (int i = 0; i < textCode.Length; i++)
            {
                char currentChar = textCode[i];

                if (ValidСharacters.Contains(currentChar))
                {
                    int newIndex = (ValidСharacters.IndexOf(currentChar) + key) % ValidСharacters.Length;
                    code += ValidСharacters[newIndex];
                }
                else
                {
                    code += currentChar;
                }
            }
            char[] charArray = code.ToCharArray();
            Array.Reverse(charArray);
            string revCode = new string(charArray);
            CodePassword.Text = keyString + "/" + revCode;
        }

       private void Decoding()
        {
            string afterSpace = "TextFile1.txt";
            int index = selectedValue.IndexOf("|");
            if (index != -1)
            {
                 afterSpace = selectedValue.Substring(index + 1);
            }
            string[] parts = afterSpace.Split('/');
            string beforeSlash = parts[0];
            string afterSlash = parts[1];
            int key = Convert.ToInt32(beforeSlash);

            string ValidСharacters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxZz0123456789!@$%&*_-+=";
            string code = "";
            char[] charArray = afterSlash.ToCharArray();
            Array.Reverse(charArray);
            string textCode = new string(charArray);

            for (int i = 0; i < textCode.Length; i++)
            {
                char currentChar = textCode[i];

                if (ValidСharacters.Contains(currentChar))
                {
                    int newIndex = (ValidСharacters.IndexOf(currentChar) - key + ValidСharacters.Length) % ValidСharacters.Length;
                    code += ValidСharacters[newIndex];
                }
                else
                {
                    code += currentChar;
                }
            }
            OutputPassword.Text = code.ToString();
            MessageBox.Show("Пароль готов!");
        }

       
    }
}


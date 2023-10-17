using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private String? currentFilePath = null;


        private bool isDarkTheme = true;

        private void ToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            if (isDarkTheme)
            {
                textBox1.Foreground = (SolidColorBrush)Application.Current.Resources["ForegroundColorBrush"];
                textBox1.Background = (SolidColorBrush)Application.Current.Resources["BackgroundColorBrush"];
                this.Background = (SolidColorBrush)Application.Current.Resources["WindowColorBrush"];

            }
            else
            {
                textBox1.Foreground = (SolidColorBrush)Application.Current.Resources["DarkForegroundColorBrush"];
                textBox1.Background = (SolidColorBrush)Application.Current.Resources["DarkBackgroundColorBrush"];
                this.Background = (SolidColorBrush)Application.Current.Resources["DarkWindowColorBrush"];
            }
            isDarkTheme = !isDarkTheme;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    textBox1.Text = reader.ReadToEnd();
                }
                currentFilePath = openFileDialog.FileName;

                string filename = System.IO.Path.GetFileName(currentFilePath);
                this.Title = $"Dodgy notepad - {filename}";

            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            currentFilePath = null;
            this.Title = "Dodgy notepad";
        }

        private void Save_Click( object sender, RoutedEventArgs e) 
        {
        if (currentFilePath == null)
            {
                SaveAs_Click(sender, e );
            }
        else
            {
                SaveFile(currentFilePath);
            }

        }

        private void SaveFile(string filePath) 
        {
        using (StreamWriter writer = new(filePath))
            {
                writer.Write(textBox1.Text);
            }
            currentFilePath=filePath;
            string filename = System.IO.Path.GetFileName(currentFilePath);
            this.Title = $"Dodgy notepad - {filename}";

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new(saveFileDialog.FileName))
                {
                    writer.Write(textBox1.Text);
                }
                currentFilePath= saveFileDialog.FileName;
                string filename = System.IO.Path.GetFileName(currentFilePath);
                this.Title = $"Dodgy notepad - {filename}";
            }
        }
    }
}

using System;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для PersonalSettings.xaml
    /// </summary>
    public partial class PersonalSettings : Page
    {
        public PersonalSettings()
        {
            InitializeComponent();
            avatar.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(Globals.currentUser.Avatar);
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
                OpenFileDialog ofdPicture = new OpenFileDialog();
                ofdPicture.Filter =
                    "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
                ofdPicture.FilterIndex = 1;
                if (ofdPicture.ShowDialog() == true)
                    avatar.Source = new BitmapImage(new Uri(ofdPicture.FileName));
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            using (UserContext context = new UserContext()) {
                if (!Globals.IsUsernameValid(usernameTextBox.Text) && usernameTextBox.Text != "")
                    informLabel.Content = "Incorrect username!";
                else
                    context.Users.Find();
            }
        }
    }
}

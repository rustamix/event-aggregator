using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для registrationPage.xaml
    /// </summary>
    public partial class registrationPage : Page
    {
        public registrationPage()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDataCorrect(usernameTextBox.Text))
                informLabel.Content = "Incorrect username!";
            else if (IsUsernameUsed(usernameTextBox.Text))
                informLabel.Content = "Username is already used!";
            else if(!IsEmailValid(emailTextBox.Text))
                informLabel.Content = "Email is not valid!";
            else if (passwordBox.Password.Length < 8)
                informLabel.Content = "Password is too short!";
            else if (!IsDataCorrect(passwordBox.Password))
                informLabel.Content = "Incorrect password!";
            else if (passwordBox.Password != repeatPasswordBox.Password)
                informLabel.Content = "Passwords are different!";
            else
            {
                using (UserContext context = new UserContext()) 
                {
                    System.Windows.Controls.Image avatar = new System.Windows.Controls.Image();
                    avatar.Source = new BitmapImage
                        (new Uri(@"C:\Users\Max\Documents\CourseWork\CourseWork\Images\avatar.png"));
                    var user = new User()
                    {
                        Username = usernameTextBox.Text,
                        Email = emailTextBox.Text,
                        Password = passwordBox.Password,
                        Avatar = BitmapSourceToByteArray((BitmapSource)avatar.Source),
                        SumRating = 5,
                        AmountOfVoters = 1,
                        SignedUpEventsID = "0 "
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                App.Current.MainWindow.Content = new LoginPage();
            }
        }
        byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                var encoder = new PngBitmapEncoder(); // or some other encoder
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsDataCorrect(string someString)
        {
            if(someString  == "")
            {
                return false;
            }
            foreach(char ch in someString.ToLower())
            {
                if(!((ch >= 'a' && ch <= 'z') || (int.TryParse(ch.ToString(), out int i))))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsUsernameUsed(string username)
        {
            using (var context = new UserContext())
            {
                foreach (var user in context.Users)
                {
                    if (user.Username == username)
                        return true;
                }
            }
            return false;
        }

        private void toLoginButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.entryFrame.Navigate(new LoginPage());
        }
    }
}

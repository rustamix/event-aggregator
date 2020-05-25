using System.Windows;
using System.Windows.Controls;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        
        public LoginPage()
        {
            InitializeComponent();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataCorrect())
            {
                var lastWin = App.Current.MainWindow;
                var win = new MainWindow();
                App.Current.MainWindow = win;
                App.Current.MainWindow.Show();
                lastWin.Close();
                App.Current.MainWindow.Content = new MainWindow().Content;
            }
            else
                informLabel.Content = "Incorrect username or password";
        }
        bool IsDataCorrect()
        {
            using (UserContext context = new UserContext())
            {
                var user = new User()
                {
                    Username = usernameTextBox.Text,
                    Password = passwordBox.Password,
                    Avatar = new byte[] { },
                    SumRating = 0,
                    AmountOfVoters = 0,
                    SignedUpEventsID = ""
                };
                foreach (var us in context.Users)
                {
                    if (us.Password == user.Password && us.Username == user.Username)
                    {
                        Globals.currentUser = us;
                        return true;
                    }
                }
                return false;
            }
        }

        private void toRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.entryFrame.Navigate(new registrationPage());
        }
    }
}
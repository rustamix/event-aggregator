using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
            Globals.mainFrame = frame;
        }

        private void addEventLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid.SetRow(selectorGrid, Grid.GetRow(addEventLabel));
            frame.Navigate(new AddEventPage());
        }

        private void upcomingEventsLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid.SetRow(selectorGrid, Grid.GetRow(upcomingEventsLabel));
            frame.Navigate(new MainPage());
        }

        private void logOutLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var lastWin = App.Current.MainWindow;
            var win = new EntryWindow();
            App.Current.MainWindow = win;
            App.Current.MainWindow.Show();
            lastWin.Close();
            App.Current.MainWindow.Content = new EntryWindow().Content;
        }

        private void requestsLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid.SetRow(selectorGrid, Grid.GetRow(requestsLabel));
            frame.Navigate(new ReceivedRequests());
        }
    }
}

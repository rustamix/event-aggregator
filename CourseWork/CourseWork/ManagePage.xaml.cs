using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        public ManagePage()
        {
            InitializeComponent();
        }

        private void personalSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid.SetRow(selectorGrid, Grid.GetRow(personalSettings));
            personalSettingsLabel.Foreground = new SolidColorBrush(Color.FromRgb(118, 169, 250));
            personalSettingsMiniLabel.Foreground = new SolidColorBrush(Color.FromRgb(118, 169, 250));
            myEventsLabel.Foreground = Brushes.Black;
            MyEventsMiniLabel.Foreground = Brushes.Gray;
            manageFrame.Navigate(new PersonalSettings());
        }

        private void myEventsGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid.SetRow(selectorGrid, Grid.GetRow(myEventsGrid));
            personalSettingsLabel.Foreground = Brushes.Black;
            personalSettingsMiniLabel.Foreground = Brushes.Gray;
            myEventsLabel.Foreground = new SolidColorBrush(Color.FromRgb(118, 169, 250));
            MyEventsMiniLabel.Foreground = new SolidColorBrush(Color.FromRgb(118, 169, 250));
            manageFrame.Navigate(new MyEvents());
        }

        private void deleteAccountGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid.SetRow(selectorGrid, Grid.GetRow(deleteAccountGrid));
        }
    }
}

using Microsoft.Win32;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Page
    {
        public AddEventPage()
        {
            InitializeComponent();
            eventDatePicker.DisplayDateStart = DateTime.Now;
            eventDatePicker.DisplayDate = DateTime.Now;
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;
            if (ofdPicture.ShowDialog() == true)
                eventImage.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!StringIsCorrect(titleTextBox.Text))
                informLabel.Content = "Incorrect title!";
            else if(IsTitleUsed(titleTextBox.Text))
                informLabel.Content = "Title is already used!";
            else if(!LogoIsCorrect())
                informLabel.Content = "Impossible to use image!";
            else if (!TypeIsCorrect())
                informLabel.Content = "Incorrect type!";
            else if(!DateIsCorrect())
                informLabel.Content = "Incorrect date!";
            else if(!AgeLimitIsCorrect())
                informLabel.Content = "Incorrect age limit!";
            else
            {
                try
                {
                    using (EventContext eventContext = new EventContext())
                    {
                        CourseWork.Event someEvent = new CourseWork.Event()
                        {
                            Title = titleTextBox.Text,
                            Logo = BitmapSourceToByteArray((BitmapSource)eventImage.Source),
                            Type = typeComboBox.Text,
                            Date = (DateTime)eventDatePicker.SelectedDate,
                            EighteenYearsRequired = eighteenYearsRequiredComboBox.Text == "None" ? false : true,
                            Description = descriptionTextBox.Text,
                            CreatorID = Globals.currentUser.UserID,
                            SignedUpUsersID = "0 "
                        };
                        eventContext.Events.Add(someEvent);
                        eventContext.SaveChanges();
                    }
                    informLabel.Content = "";
                    Globals.mainFrame.Navigate(new AddEventPage());
                }
                catch(Exception)
                {
                    informLabel.Content = "Something went wrong!";
                }
            }
        }
        public static bool IsTitleUsed(string title)
        {
            using (var context = new EventContext())
            {
                foreach (var ev in context.Events)
                {
                    if (ev.Title == title)
                        return true;
                }
            }
            return false;
        }
        public static bool StringIsCorrect(string someString)
        {
            if (someString == "")
            {
                return false;
            }
            foreach (char ch in someString.ToLower())
            {
                if (!((ch >= 'a' && ch <= 'z') || (int.TryParse(ch.ToString(), out int i)) || (ch == ' ')))
                {
                    return false;
                }
            }
            return true;
        }
        bool LogoIsCorrect()
        {
            try
            {
                byte[] temp = BitmapSourceToByteArray((BitmapSource)eventImage.Source);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        bool TypeIsCorrect()
        {
            if (!StringIsCorrect(typeComboBox.Text) || typeComboBox.SelectedIndex == -1)
                return false;
            return true;
        }
        bool DateIsCorrect()
        {
            if (eventDatePicker.SelectedDate == null  || !DateTime.TryParse(eventDatePicker.SelectedDate.ToString(), out DateTime temp))
                return false;
            return true;
        }
        bool AgeLimitIsCorrect()
        {
            if (eighteenYearsRequiredComboBox.SelectedIndex == -1)
                return false;
            return true;
        }
        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                var encoder = new PngBitmapEncoder(); // or some other encoder
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
        void typeComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            typeComboBox.IsDropDownOpen = true;
        }

        void typeComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            typeComboBox.IsDropDownOpen = false;
        }
    }
}

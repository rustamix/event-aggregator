using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для EventOverviewPage.xaml
    /// </summary>
    public partial class EventOverviewPage : Page
    {
        Event theEvent;
        public EventOverviewPage(Event theEvent)
        {
            this.theEvent = theEvent;
            InitializeComponent();
            if (Globals.ListOfApprovedUsers(theEvent).Contains(Globals.currentUser) ||
                Array.ConvertAll(theEvent.SignedUpUsersID.Trim().Split(' '), int.Parse).Contains(Globals.currentUser.UserID) ||
                theEvent.CreatorID == Globals.currentUser.UserID)
                SendRequestBorder.Visibility = Visibility.Hidden;

            titleLabel.Content = $"{theEvent.Title}, {theEvent.Date:dd MMMM yyyy}";
            descriptionTextBlock.Text = theEvent.Description;
            eventImage.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(theEvent.Logo);
            FillGuestList(theEvent);
        }
        private void FillGuestList(Event theEvent)
        {
            int currentRow = 1;
            using (UserContext context = new UserContext())
            {
                foreach (User user in Globals.ListOfApprovedUsers(theEvent))
                {
                    var row = new RowDefinition()
                    {
                        Height = GridLength.Auto
                    };
                    var row1 = new RowDefinition()
                    {
                        Height = GridLength.Auto
                    };
                    guestListGrid.RowDefinitions.Add(row);
                    guestListGrid.RowDefinitions.Add(row1);
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image
                    {
                        Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(user.Avatar),
                        Height = 40,
                        Width = 40,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(0, 0, 0, 0)
                    };
                    Label label = new Label
                    {
                        Content = user.Username,
                        Foreground = Brushes.Black,
                        FontFamily = new FontFamily("Leelawadee"),
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(0, 0, 0, 3),
                    };
                    label.MouseLeftButtonUp += Globals.Username_MouseLeftButtonUp;
                    Grid rating = new Grid();
                    rating.VerticalAlignment = VerticalAlignment.Top;
                    Grid.SetRow(rating, currentRow + 1);
                    Grid.SetColumn(rating, 1);
                    guestListGrid.Children.Add(rating);
                    for (int i = 0; i < ((double)user.SumRating / (double)user.AmountOfVoters - user.SumRating / user.AmountOfVoters >= 0.5 ?
                        (user.SumRating / user.AmountOfVoters) + 1 : user.SumRating / user.AmountOfVoters); i++)
                    {
                        rating.ColumnDefinitions.Add(new ColumnDefinition());
                        Image star = new Image
                        {
                            Source = new BitmapImage(new Uri(@"C:\Users\Max\Documents\CourseWork\CourseWork\Images\coinStar.png")),
                            Width = 12,
                            Height = 12,
                            Margin = new Thickness(3, 3, 3, 3)
                        };
                        Grid.SetRowSpan(star, 1);
                        Grid.SetColumnSpan(star, 1);
                        Grid.SetRow(star, 0);
                        Grid.SetColumn(star, i);
                        rating.Children.Add(star);
                    }
                    Grid.SetRow(image, currentRow);
                    Grid.SetColumn(image, 0);
                    Grid.SetRowSpan(image, 2);
                    Grid.SetRow(label, currentRow);
                    Grid.SetColumn(label, 1);
                    guestListGrid.Children.Add(image);
                    guestListGrid.Children.Add(label);
                    currentRow += 2;
                }
            }
        }
        private void sendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            using (EventContext context = new EventContext())
            {
                context.Events.Find(theEvent.EventID).SignedUpUsersID += Globals.currentUser.UserID.ToString() + " ";
                context.SaveChanges();
            }
            informLabel.Foreground = Brushes.Green;
            informLabel.Content = "Request has been sent!";
        }
    }
}

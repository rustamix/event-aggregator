using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        User theUser;
        public ProfilePage(User user)
        {
            theUser = user;
            InitializeComponent();
            avatarImage.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(theUser.Avatar);
            usernameLabel.Content = theUser.Username;
            eventListLabel.Content = $"{theUser.Username} will visit";
            DisplayRating();
            amountOfVotersLabel.Content = $"({theUser.AmountOfVoters})";
            FillEventList();
        }
        void DisplayRating()
        {
            Grid rating = new Grid();
            rating.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(rating, 1);
            Grid.SetColumn(rating, 1);
            profileGrid.Children.Add(rating);
            for (int i = 0; i < ((double)theUser.SumRating / (double)theUser.AmountOfVoters - theUser.SumRating / theUser.AmountOfVoters >= 0.5 ?
                (theUser.SumRating / theUser.AmountOfVoters) + 1 : theUser.SumRating / theUser.AmountOfVoters); i++)
            {
                rating.ColumnDefinitions.Add(new ColumnDefinition());
                Image star = new Image
                {
                    Source = new BitmapImage(new Uri(@"C:\Users\Max\Documents\CourseWork\CourseWork\Images\coinStar.png")),
                    Width = 24,
                    Height = 25,
                    Margin = new Thickness(3, 3, 3, 3)
                };
                Grid.SetRowSpan(star, 1);
                Grid.SetColumnSpan(star, 1);
                Grid.SetRow(star, 0);
                Grid.SetColumn(star, i);
                rating.Children.Add(star);
            }
        }
        void FillEventList()
        {
            using (var context = new EventContext())
            {
                int currentRowIndex = 1;
                foreach (var ev in context.Events)
                {
                    if (theUser.SignedUpEventsID != "" && Array.ConvertAll(theUser.SignedUpEventsID.Trim().Split(' '), int.Parse).Contains(ev.EventID))
                    {
                        var row = new RowDefinition
                        {
                            Height = GridLength.Auto
                        };
                        eventListGrid.RowDefinitions.Add(row);
                        System.Windows.Controls.Image image = new System.Windows.Controls.Image
                        {
                            Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(ev.Logo),
                            Height = 100,
                            Width = 100,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0, 0, 0, 0)
                        };
                        Label label = new Label
                        {
                            Content = $"{ev.Title}, {ev.Date:dd MMMM yyyy}",
                            Foreground = System.Windows.Media.Brushes.Black,
                            FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                            FontSize = 15,
                            FontWeight = FontWeights.Bold,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0, 0, 0, 5)
                        };
                        label.MouseLeftButtonUp += MainPage.EventTitle_MouseLeftButtonUp;
                        TextBlock textBlock = new TextBlock
                        {
                            Text = ev.Description.Length < 200 ? ev.Description :
                            ev.Description.Substring(0, 200) + "...",
                            VerticalAlignment = VerticalAlignment.Center,
                            Foreground = System.Windows.Media.Brushes.Black,
                            FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                            Margin = new Thickness(0, 0, 0, 0),
                            TextWrapping = TextWrapping.Wrap
                        };
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Children.Add(label);
                        stackPanel.Children.Add(textBlock);
                        Grid.SetRow(image, currentRowIndex);
                        Grid.SetColumn(image, 0);
                        Grid.SetRow(stackPanel, currentRowIndex);
                        Grid.SetColumn(stackPanel, 1);
                        eventListGrid.Children.Add(image);
                        eventListGrid.Children.Add(stackPanel);
                        currentRowIndex++;
                    }
                }
            }
        }
    }
}

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
    /// Логика взаимодействия для ReceivedRequests.xaml
    /// </summary>
    public partial class ReceivedRequests : Page
    {
        public ReceivedRequests()
        {
            InitializeComponent();
            FillRequestsGrid();
        }
        void FillRequestsGrid()
        {
            using (var eventContext = new EventContext())
            {
                int currentRowIndex = 1;
                foreach (var ev in eventContext.Events)
                {
                    if (ev.CreatorID == Globals.currentUser.UserID)
                    {
                        using (var userContext = new UserContext()) 
                        {
                            foreach (int id in Array.ConvertAll(ev.SignedUpUsersID.Trim().Split(' '), int.Parse))
                            {
                                if (id != 0) 
                                {
                                    User user = userContext.Users.Find(id);
                                    RowDefinition row = new RowDefinition
                                    {
                                        Height = GridLength.Auto
                                    };
                                    requestsGrid.RowDefinitions.Add(row);
                                    System.Windows.Controls.Image image = new System.Windows.Controls.Image
                                    {
                                        Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(user.Avatar),
                                        Height = 64,
                                        Width = 64,
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                        Margin = new Thickness(0, 10, 0, 0)
                                    };
                                    Grid.SetRow(image, currentRowIndex);
                                    Grid.SetColumn(image, 0);
                                    requestsGrid.Children.Add(image);
                                    Label label = new Label
                                    {
                                        Content = user.Username,
                                        Foreground = System.Windows.Media.Brushes.Black,
                                        FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                                        FontSize = 15,
                                        FontWeight = FontWeights.Bold,
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                    };
                                    label.MouseLeftButtonUp += Globals.Username_MouseLeftButtonUp;
                                    Grid.SetRow(label, currentRowIndex);
                                    Grid.SetColumn(label, 1);
                                    requestsGrid.Children.Add(label);
                                    Label label1 = new Label
                                    {
                                        Content = $"{ev.Title}, {ev.Date:dd MMMM yyyy}",
                                        Foreground = System.Windows.Media.Brushes.Black,
                                        FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                                        FontSize = 15,
                                        FontWeight = FontWeights.Bold,
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                    };
                                    Grid.SetRow(label1, currentRowIndex);
                                    Grid.SetColumn(label1, 2);
                                    requestsGrid.Children.Add(label1);
                                    Border border = new Border
                                    {
                                        CornerRadius = new CornerRadius(5),
                                        Background = new SolidColorBrush(Color.FromRgb(118, 169, 250)),
                                        Height = 25,
                                        Width = 150
                                    };
                                    Grid.SetRow(border, currentRowIndex);
                                    Grid.SetColumn(border, 3);
                                    requestsGrid.Children.Add(border);
                                    Button acceptButton = new Button
                                    {
                                        Content = "Accept",
                                        Background = new SolidColorBrush(),
                                        Foreground = System.Windows.Media.Brushes.White,
                                        FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                                        Tag = $"{ev.EventID} {user.UserID}"
                                    };
                                    border.Child = acceptButton;
                                    Border border1 = new Border
                                    {
                                        CornerRadius = new CornerRadius(5),
                                        Background = new SolidColorBrush(Color.FromRgb(118, 169, 250)),
                                        Height = 25,
                                        Width = 150
                                    };
                                    Grid.SetRow(border1, currentRowIndex);
                                    Grid.SetColumn(border1, 4);
                                    requestsGrid.Children.Add(border1);
                                    Button declineButton = new Button
                                    {
                                        Content = "Decline",
                                        Background = new SolidColorBrush(),
                                        Foreground = System.Windows.Media.Brushes.White,
                                        FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                                        Tag = $"{ev.EventID} {user.UserID}"
                                    };
                                    border1.Child = declineButton;
                                    acceptButton.Click += AcceptButton_Click;
                                    declineButton.Click += DeclineButton_Click;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            using (EventContext eventContex = new EventContext())
            {
                using (UserContext userContex = new UserContext())
                {
                    int[] ids = Array.ConvertAll(((Button)sender).Tag.ToString().Split(' '), int.Parse);
                    eventContex.Events.Find(ids[0]).SignedUpUsersID = eventContex.Events.Find(ids[0]).SignedUpUsersID.Replace($"{ids[1]} ", "");
                    userContex.Users.Find(ids[1]).SignedUpEventsID += $"{ids[0]} ";
                    eventContex.SaveChanges();
                    userContex.SaveChanges();
                }
            }
            FillRequestsGrid();
        }
        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            using (EventContext eventContex = new EventContext())
            {
                eventContex.Events.Find(((Event)((Button)sender).Tag).EventID).SignedUpUsersID.Replace($"{((Button)sender).Name} ", "");
                eventContex.SaveChanges();
            }
            FillRequestsGrid();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для MyEvents.xaml
    /// </summary>
    public partial class MyEvents : Page
    {
        public MyEvents()
        {
            InitializeComponent();
            FillCreatedByMeGrid();
            FillSignedUpByMeGrid();
        }
        void FillCreatedByMeGrid()
        {
            using (var context = new EventContext())
            {
                int currentRowIndex = 1;
                foreach (var ev in context.Events)
                {
                    if (ev.CreatorID == Globals.currentUser.UserID)
                    {
                        var row = new RowDefinition
                        {
                            Height = GridLength.Auto
                        };
                        createdByMeGrid.RowDefinitions.Add(row);
                        System.Windows.Controls.Image image = new System.Windows.Controls.Image
                        {
                            Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(ev.Logo),
                            Height = 50,
                            Width = 50,
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
                            Margin = new Thickness(0, 0, 0, 5),
                            Tag = ev
                        };
                        label.MouseLeftButtonUp += EventTitle_MouseLeftButtonUp;
                        Border border = new Border
                        {
                            CornerRadius = new CornerRadius(5),
                            Background = new SolidColorBrush(Color.FromRgb(118, 169, 250)),
                            Height = 25,
                            Width = 150
                        };
                        Grid.SetRow(border, currentRowIndex);
                        Grid.SetColumn(border, 2);
                        createdByMeGrid.Children.Add(border);
                        Button button = new Button
                        {
                            Content = "Remove",
                            Background = new SolidColorBrush(),
                            Foreground = System.Windows.Media.Brushes.White,
                            FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                            Tag = ev
                        };
                        button.Click += RemoveButton_Clicked;
                        border.Child = button;
                        Grid.SetRow(image, currentRowIndex);
                        Grid.SetColumn(image, 0);
                        createdByMeGrid.Children.Add(image);
                        Grid.SetRow(label, currentRowIndex);
                        Grid.SetColumn(label, 1);
                        createdByMeGrid.Children.Add(label);
                        currentRowIndex++;
                    }
                }
            }
        }
        void FillSignedUpByMeGrid()
        {
            using(EventContext eventContext = new EventContext())
            {
                int currentRowIndex = 1;
                foreach (int id in Array.ConvertAll(Globals.currentUser.SignedUpEventsID.Trim().Split(' '), int.Parse))
                    if(id != 0)
                    {
                        var ev = eventContext.Events.Find(id);
                        var row = new RowDefinition
                        {
                            Height = GridLength.Auto
                        };
                        createdByMeGrid.RowDefinitions.Add(row);
                        System.Windows.Controls.Image image = new System.Windows.Controls.Image
                        {
                            Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(ev.Logo),
                            Height = 50,
                            Width = 50,
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
                            Margin = new Thickness(0, 0, 0, 5),
                            Tag = ev
                        };
                        label.MouseLeftButtonUp += EventTitle_MouseLeftButtonUp;
                        Border border = new Border
                        {
                            CornerRadius = new CornerRadius(5),
                            Background = new SolidColorBrush(Color.FromRgb(118, 169, 250)),
                            Height = 25,
                            Width = 150
                        };
                        Grid.SetRow(border, currentRowIndex);
                        Grid.SetColumn(border, 2);
                        createdByMeGrid.Children.Add(border);
                        Button button = new Button
                        {
                            Content = "Leave",
                            Background = new SolidColorBrush(),
                            Foreground = System.Windows.Media.Brushes.White,
                            FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                            Tag = ev
                        };
                        button.Click += LeaveButton_Clicked;
                        border.Child = button;
                        Grid.SetRow(image, currentRowIndex);
                        Grid.SetColumn(image, 0);
                        createdByMeGrid.Children.Add(image);
                        Grid.SetRow(label, currentRowIndex);
                        Grid.SetColumn(label, 1);
                        createdByMeGrid.Children.Add(label);
                        currentRowIndex++;

                    }
            }
        }
        private void EventTitle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Globals.mainFrame.Navigate(new EventOverviewPage((Event)((Label)sender).Tag));
        }
        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
        {
            using (EventContext eventContext = new EventContext())
            {
                using (UserContext userContext = new UserContext())
                {
                    foreach(User us in Globals.ListOfApprovedUsers((Event)((Button)sender).Tag))
                    {
                        userContext.Users.Find(us.UserID).SignedUpEventsID = us.SignedUpEventsID.Replace($"{((Event)((Button)sender).Tag).EventID} ", "");
                    }
                    eventContext.Entry((Event)((Button)sender).Tag).State = System.Data.Entity.EntityState.Deleted;
                    eventContext.SaveChanges();
                    userContext.SaveChanges();
                }
            }
        }
        private void LeaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.Users.Find(Globals.currentUser.UserID).SignedUpEventsID = Globals.currentUser.SignedUpEventsID.Replace($"{((Event)((Button)sender).Tag).EventID} ", "");
            }
        }
    }
}

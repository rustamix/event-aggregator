using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            FillDataGrid();
        }
        void FillDataGrid()
        {
            using (EventContext eventContext = new EventContext())
            {
                int currentRowIndex = 0;
                foreach (Event someEvent in eventContext.Events)
                {
                    var row = new RowDefinition
                    {
                        Height = GridLength.Auto
                    };
                    eventsGrid.RowDefinitions.Add(row);
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image
                    {
                        Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(someEvent.Logo),
                        Height = 100,
                        Width = 100,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(0, 0, 0, 10)
                    };
                    Label label = new Label
                    {
                        Content = someEvent.Title,
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    label.MouseLeftButtonUp += EventTitle_MouseLeftButtonUp;
                    TextBlock textBlock = new TextBlock
                    {
                        Text = someEvent.Description.Length < 200 ? someEvent.Description :
                        someEvent.Description.Substring(0, 200) + "...",
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Top,
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontFamily = new System.Windows.Media.FontFamily("Leelawadee"),
                        Margin = new Thickness(0, 0, 0, 10),
                        TextWrapping = TextWrapping.Wrap
                    };
                    StackPanel stackPanel = new StackPanel();
                    StackPanel.SetZIndex(label, 0);
                    StackPanel.SetZIndex(textBlock, 1);
                    stackPanel.Children.Add(label);
                    stackPanel.Children.Add(textBlock);
                    Grid.SetRow(image, currentRowIndex);
                    Grid.SetColumn(image, 0);
                    Grid.SetRow(stackPanel, currentRowIndex);
                    Grid.SetColumn(stackPanel, 1);
                    eventsGrid.Children.Add(image);
                    eventsGrid.Children.Add(stackPanel);
                    currentRowIndex++;
                }
            }
        }
        public static void EventTitle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (EventContext context = new EventContext())
            {
                foreach (var ev in context.Events)
                {
                    if(ev.Title == ((Label)sender).Content.ToString())
                    {
                        Globals.mainFrame.Navigate(new EventOverviewPage(ev));
                    }
                }  
            }
        }
        private System.Drawing.Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new System.IO.MemoryStream(byteArray))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }
        BitmapSource LoadImage(Byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
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

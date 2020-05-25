using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseWork
{
    public class Globals
    {
        public static Frame mainFrame;
        public static Frame entryFrame;
        public static User currentUser;
        public static List<User> ListOfApprovedUsers(Event someEvent)
        {
            List<User> users = new List<User>();
            using (var context = new UserContext())
            {
                foreach (var user in context.Users)
                {
                    if (Array.ConvertAll(user.SignedUpEventsID.Trim().Split(' '), int.Parse).Contains(someEvent.EventID))
                        users.Add(user);
                }
            }
            return users;
        }
        public static void Username_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (UserContext context = new UserContext())
            {
                foreach (var us in context.Users)
                {
                    if (us.Username == ((Label)sender).Content.ToString())
                    {
                        mainFrame.Navigate(new ProfilePage(us));
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CourseWork
{
    public partial class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public byte[] Logo { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public bool EighteenYearsRequired { get; set; }
        public string Description { get; set; }
        public int CreatorID { get; set; }
        public string SignedUpUsersID { get; set; }
    }
}

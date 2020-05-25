using System;
using System.Collections.Generic;

namespace CourseWork
{
    public partial class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }
        public int SumRating { get; set; }
        public int AmountOfVoters { get; set; }
        public string SignedUpEventsID { get; set; }
    }
}

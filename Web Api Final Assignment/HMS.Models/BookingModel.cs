using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<int> RoomId { get; set; }
        public string BookingStatus { get; set; }

    }
}

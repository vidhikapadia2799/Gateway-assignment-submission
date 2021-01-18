using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class HotelRoomModel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomCategory { get; set; }
        public Nullable<decimal> RoomPrice { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class RoomModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public Nullable<decimal> RoomPrice { get; set; }
        public Nullable<bool> RoomIsActive { get; set; }
        public Nullable<System.DateTime> RoomCreatedDate { get; set; }
        public string RoomCreatedBy { get; set; }
        public Nullable<System.DateTime> RoomUpdatedDate { get; set; }
        public string RoomUpdatedBy { get; set; }
        public Nullable<int> HotelId { get; set; }
        public string RoomCategory { get; set; }
    }
}

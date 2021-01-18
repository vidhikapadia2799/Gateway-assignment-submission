using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL.Interface
{
    public interface IHotelManager
    {
        HotelModel GetHotel(int id);
        List<HotelModel> GetAllHotel();
        string CreateHotel(HotelModel hotelModel);
        string UpdateHotel(HotelModel hotelModel);
       
        string CreateRoom(RoomModel roomModel);
        List<HotelRoomModel> GetRoomByPara();
        string BookRoom(BookingModel bookingModel);

        List<BookingModel> CheckBooking(BookingModel bookingModel);

        string UpdateBookingDate(BookingModel bookingModel);
        string UpdateBookingStatus(BookingModel bookingModel);
        string DeleteBooking(int id);

    }
}

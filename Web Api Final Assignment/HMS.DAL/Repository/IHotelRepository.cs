using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public interface IHotelRepository
    {
        HotelModel GetHotel(int id);
        List<HotelModel> GetAllHotel();
        string CreateHotel(HotelModel model);
        string CreateRoom(RoomModel model);
        string UpdateHotel(HotelModel model);
        List<HotelRoomModel> GetRoomByPara();
        string BookRoom(BookingModel bookingModel);
        List<BookingModel> CheckBooking(BookingModel bookingModel);
        string UpdateBookingDate(BookingModel bookingModel);
        string UpdateBookingStatus(BookingModel bookingModel);
       string DeleteBooking(int id);
    }
}

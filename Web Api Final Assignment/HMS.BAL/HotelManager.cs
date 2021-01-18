using HMS.BAL.Interface;
using HMS.DAL.Repository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL
{
    public class HotelManager : IHotelManager
    {
        private readonly IHotelRepository _HotelRepository;

        public HotelManager(IHotelRepository HotelRepository)
        {
            _HotelRepository = HotelRepository; 
        }
        public string CreateHotel(HotelModel hotelModel)
        {
            return _HotelRepository.CreateHotel(hotelModel);
        }

        public string CreateRoom(RoomModel roomModel)
        {
            return _HotelRepository.CreateRoom(roomModel);
        }

        
        public List<HotelModel> GetAllHotel()
        {
            return _HotelRepository.GetAllHotel();
        }

        public HotelModel GetHotel(int id)
        {
            return _HotelRepository.GetHotel(id);
        }

        public string UpdateHotel(HotelModel hotelModel)
        {
            return _HotelRepository.UpdateHotel(hotelModel);
        }

       
        public List<HotelRoomModel> GetRoomByPara()
        {
            return _HotelRepository.GetRoomByPara();
        }

        public string BookRoom(BookingModel bookingModel)
        {
            return _HotelRepository.BookRoom(bookingModel);
        }

        public List<BookingModel> CheckBooking(BookingModel bookingModel)
        {
            return _HotelRepository.CheckBooking(bookingModel);
        }

        public string UpdateBookingDate(BookingModel bookingModel)
        {
            return _HotelRepository.UpdateBookingDate(bookingModel);
        }

        public string UpdateBookingStatus(BookingModel bookingModel)
        {
            return _HotelRepository.UpdateBookingStatus(bookingModel);
        }
        public string DeleteBooking(int id)
        {
            return _HotelRepository.DeleteBooking(id);
        }
    }
}

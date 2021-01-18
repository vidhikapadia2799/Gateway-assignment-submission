using HMS.BAL.Interface;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HMS.WebApi.Controllers
{
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;

        public HotelController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }

        // GET: api/Hotel
        public List<HotelModel> Get()
        {
            return _hotelManager.GetAllHotel();
        }

        // GET: api/Hotel/5
        public HotelModel Get(int id)
        {
            return _hotelManager.GetHotel(id);
        }
        [Route("api/room")]
        public List<HotelRoomModel> GetRoomByPara()
        {
            return _hotelManager.GetRoomByPara();
        }

        [Route("api/checkbooking")]
        //Check Room availability
        public List<BookingModel> CheckBooking([FromBody]BookingModel model)
        {
            return _hotelManager.CheckBooking(model);
        }

        // POST: api/Hotel
        public string Post([FromBody]HotelModel model)
        {
            return _hotelManager.CreateHotel(model);
        }

        // POST: api/room
        [Route("api/room")]
        public string Post([FromBody] RoomModel model)
        {
            return _hotelManager.CreateRoom(model);
        }

        [Route("api/bookroom")]
        public string Post([FromBody] BookingModel model)
        {
            return _hotelManager.BookRoom(model);
        }

        // PUT: api/Hotel/5
        public string Put([FromBody]HotelModel model)
        {
            return _hotelManager.UpdateHotel(model);
        }

        [Route("api/booking")]
        public string PutUpdateBookingDate([FromBody] BookingModel model)
        {
            return _hotelManager.UpdateBookingDate(model);
        }
        [Route("api/bookingstatus")]
        public string PutUpdateBookingStatus([FromBody] BookingModel model)
        {
            return _hotelManager.UpdateBookingStatus(model);
        }

        
        public string DeleteBooking(int id)
        {
            return _hotelManager.DeleteBooking(id);
        }
    }
}

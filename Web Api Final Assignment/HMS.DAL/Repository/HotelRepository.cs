using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly Database.Hotel_Management_SystemEntities _dbContext;

        public HotelRepository()
        {
            _dbContext = new Database.Hotel_Management_SystemEntities();
        }

        public string BookRoom(BookingModel model)
        {
            try
            {
                if (model != null)
                {
                    var entity = _dbContext.Roomstbls.Find(model.RoomId);
                    Database.Roomstbl room = new Database.Roomstbl();
                    Database.Bookingstbl booking = new Database.Bookingstbl();

                    booking.BookingDate = model.BookingDate;
                    booking.BookingStatus = model.BookingStatus;
                    booking.RoomId = model.RoomId;

                    entity.RoomIsActive = true;

                    _dbContext.Bookingstbls.Add(booking);
                    _dbContext.SaveChanges();
                    return "Room Booked Successfully!";
                }
                return "No Data Found!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BookingModel> CheckBooking(BookingModel bookingModel)
        {
            List<BookingModel> booking = new List<BookingModel>();
            var room = _dbContext.Roomstbls.ToList();

            if (room != null)
            {
                foreach (var item in room)
                {
                    var entity = _dbContext.Bookingstbls.Where(x => x.RoomId == item.RoomId && x.BookingDate == bookingModel.BookingDate);
                    if (entity.Count() != 0)
                    {
                        foreach (var data in entity)
                        {
                            RoomModel rm = new RoomModel ();
                            if (data.BookingStatus == "Deleted")
                            {
                                rm.RoomIsActive = true;
                            }
                            else
                            {
                                rm.RoomIsActive = false;
                            }
                        }
                    }
                }
            }
            return booking;
        }

        public string CreateHotel(HotelModel model)
        {
            try
            { 
                if(model != null)
                {
                    Database.Hotelstbl entity = new Database.Hotelstbl();

                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.City = model.City;
                    entity.Pincode = model.Pincode;
                    entity.ContactNumber = model.ContactNumber;
                    entity.ContactPerson = model.ContactPerson;
                    entity.Website = model.Website;
                    entity.Facebook = model.Facebook;
                    entity.Twitter = model.Twitter;
                    entity.CreatedBy = model.CreatedBy;
                    entity.CreatedDate = DateTime.Now;
                    entity.UpdatedBy= model.UpdatedBy;
                    entity.UpdatedDate = model.UpdatedDate;
                    entity.IsActive = model.IsActive;

                    _dbContext.Hotelstbls.Add(entity);
                    _dbContext.SaveChanges();

                    return "Successfully Added!"; 
                }
                return "Model is null";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string CreateRoom(RoomModel model)
        {
            try
            {
                if (model != null)
                {
                    Database.Roomstbl entity = new Database.Roomstbl();

                    entity.RoomName = model.RoomName;
                    entity.RoomCategory = model.RoomCategory;
                    entity.RoomPrice = model.RoomPrice;
                    entity.RoomCreatedBy = model.RoomCreatedBy;
                    entity.RoomCreatedDate = DateTime.Now;
                    entity.RoomUpdatedBy = model.RoomUpdatedBy;
                    entity.RoomUpdatedDate = model.RoomUpdatedDate;
                    entity.RoomIsActive = model.RoomIsActive;
                    entity.HotelId = model.HotelId;
                   
                    _dbContext.Roomstbls.Add(entity);
                    _dbContext.SaveChanges();

                    return "Successfully Added!";
                }
                return "Model is null";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<HotelModel> GetAllHotel()
        {
            var entities = _dbContext.Hotelstbls.OrderBy(x=>x.Name).ToList();
            List<HotelModel> list = new List<HotelModel>();
            if(entities != null)
            {
                foreach (var item in entities)
                {
                    HotelModel model = new HotelModel();
                    model.HotelId = item.HotelId;
                    model.Name = item.Name;
                    model.Address = item.Address;
                    model.City = item.City;
                    model.Pincode = item.Pincode;
                    model.ContactNumber = item.ContactNumber;
                    model.ContactPerson = item.ContactPerson;
                    model.Website = item.Website;
                    model.Facebook = item.Facebook;
                    model.Twitter = item.Twitter;
                    model.CreatedBy = item.CreatedBy;
                    model.CreatedDate = item.CreatedDate;
                    model.UpdatedBy = item.UpdatedBy;
                    model.UpdatedDate = item.UpdatedDate;
                    model.IsActive = item.IsActive;

                    list.Add(model);
                }
            }
            return list;
        }

        public HotelModel GetHotel(int id)
        {
            var entity = _dbContext.Hotelstbls.Find(id);

            HotelModel model = new HotelModel();
            if(entity != null)
            {
                model.HotelId = entity.HotelId;
                model.Name = entity.Name;
                model.Address = entity.Address;
                model.City = entity.City;
                model.Pincode = entity.Pincode;
                model.ContactNumber = entity.ContactNumber;
                model.ContactPerson = entity.ContactPerson;
                model.Website = entity.Website;
                model.Facebook = entity.Facebook;
                model.Twitter = entity.Twitter;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                model.UpdatedBy = entity.UpdatedBy;
                model.UpdatedDate = entity.UpdatedDate;
                model.IsActive = entity.IsActive;
            }
           
            return model;
        }

        public List<HotelRoomModel> GetRoomByPara()
        {
            var HotelRoom = (from h in _dbContext.Hotelstbls
                             join r in _dbContext.Roomstbls
                             on h.HotelId equals r.HotelId 
                             select new HotelRoomModel
                             {
                                 HotelId = h.HotelId,
                                 Name = h.Name,
                                 RoomName = r.RoomName,
                                 RoomCategory = r.RoomCategory,
                                 City = h.City,
                                 Pincode = h.Pincode,
                                 RoomPrice = r.RoomPrice
                             }).OrderBy(r => r.RoomPrice).ToList();
            return HotelRoom;
        }

      

        public string UpdateHotel(HotelModel model)
        {
            try
            {
                var entity = _dbContext.Hotelstbls.Find(model.HotelId);
                if(entity != null)
                {
                    entity.HotelId = model.HotelId;
                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.City = model.City;
                    entity.Pincode = model.Pincode;
                    entity.ContactNumber = model.ContactNumber;
                    entity.ContactPerson = model.ContactPerson;
                    entity.Website = model.Website;
                    entity.Facebook = model.Facebook;
                    entity.Twitter = model.Twitter;
                    entity.CreatedBy = model.CreatedBy;
                    entity.CreatedDate = model.CreatedDate;
                    entity.UpdatedBy = model.UpdatedBy;
                    entity.UpdatedDate = DateTime.Now;
                    entity.IsActive = model.IsActive;

                    _dbContext.SaveChanges();

                    return "Updated Data!!";
                }
                return "No data found";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string UpdateBookingDate(BookingModel bookingModel)
        {
            var entity = _dbContext.Bookingstbls.Find(bookingModel.BookingId);

            if (entity != null)
            {
                entity.BookingDate = bookingModel.BookingDate;
                _dbContext.SaveChanges();
                return "Updated Booking Date Successfully!!";
            }

            return "Something went wrong!!Try Again...";

        }

        public string UpdateBookingStatus(BookingModel bookingModel)
        {
            var entity = _dbContext.Bookingstbls.Find(bookingModel.BookingId);

            if (entity != null)
            {
                entity.BookingStatus = bookingModel.BookingStatus;
                _dbContext.SaveChanges();
                return "Updated Booking Status Successfully!!";
            }

            return "Something went wrong!!Try Again...";

        }

        public string DeleteBooking(int id)
        {
            try
            {
                var booking = _dbContext.Bookingstbls.Find(id);
                var rooms = _dbContext.Roomstbls.Find(booking.RoomId);

                if (booking != null)
                {
                    booking.BookingStatus = "Deleted";
                    rooms.RoomIsActive = false;
                    _dbContext.SaveChanges();

                    return "Room Booking Deleted Successfully!";
                }
                return "No Data Found!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}

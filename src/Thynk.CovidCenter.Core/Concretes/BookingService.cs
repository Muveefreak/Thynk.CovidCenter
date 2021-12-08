using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.Constants;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.Helpers;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using Thynk.CovidCenter.Data.Enums;
using Thynk.CovidCenter.Data.Models;
using Thynk.CovidCenter.Repository.Cache;
using Thynk.CovidCenter.Repository.Commands.Interfaces;
using Thynk.CovidCenter.Repository.Queries.Interfaces;

namespace Thynk.CovidCenter.Core.Concretes
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IDBCommandRepository<Booking> _bookingCommandRepository;
        private readonly IDBQueryRepository<Booking> _bookingQueryRepository;
        private readonly IDBCommandRepository<AvailableDate> _availableDateCommandRepository;
        private readonly IDBQueryRepository<AvailableDate> _availableDateQueryRepository;
        private readonly IDBQueryRepository<ApplicationUser> _applicationUserQueryRepository;
        private readonly IConfiguration _configuration;
        private readonly IUtilities _utilities;
        private readonly ICache _cache;
        //private const string AllAvailDateCacheConstant = "COVID_CENTER_ALL_AVAIL_DATES";

        public BookingService(IMapper mapper,
            IDBCommandRepository<Booking> bookingCommandRepository,
            IDBQueryRepository<Booking> bookingQueryRepository,
            IDBCommandRepository<AvailableDate> availableDateCommandRepository,
            IDBQueryRepository<AvailableDate> availableDateQueryRepository,
            IDBQueryRepository<ApplicationUser> applicationUserQueryRepository,
            ICache cache,
            IConfiguration configuration,
            IUtilities utilities)
        {
            _configuration = configuration;
            _bookingCommandRepository = bookingCommandRepository;
            _bookingQueryRepository = bookingQueryRepository;
            _availableDateCommandRepository = availableDateCommandRepository;
            _availableDateQueryRepository = availableDateQueryRepository;
            _applicationUserQueryRepository = applicationUserQueryRepository;
            _cache = cache;
            _mapper = mapper;
            _utilities = utilities;
        }
        public async Task<BookingResponseModel> GetBookings(BookingStatus status)
        {
            List<Booking> bookings = (await _bookingQueryRepository.GetByAsync(x => x.AvailableDateSelected <= DateTime.UtcNow && x.BookingStatus == status)).ToList();

            if (!bookings.Any())
            {
                return new BookingResponseModel
                {
                    Message = ResponseMessages.NoBookingRecordFound,
                    Status = false
                };
            }

            List<BookingDTO> bookingsDTO = _mapper.Map<List<BookingDTO>>(bookings);

            return new BookingResponseModel
            {
                Message = ResponseMessages.Success,
                Status = true,
                Data = bookingsDTO
            };
        }

        public async Task<BaseResponse> CreateBooking(CreateBookingRequest request)
        {
            ApplicationUser user = await _utilities.GetUser(request.ApplicationUserId);
            if (user.UserRole != UserRole.Individual)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.OnlyIndividual,
                    Status = false
                };
            }

            Booking bookings = await _bookingQueryRepository.GetByDefaultAsync(x => x.AvailableDateId == request.AvailableDateId
            && x.ApplicationUserId == request.ApplicationUserId && x.LocationID == request.LocationID);
            if (bookings != null)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoDuplicateBookingsLocation,
                    Status = false
                };
            }

            AvailableDate availableDate = await _availableDateQueryRepository.GetByDefaultAsync(x => x.LocationId == request.LocationID && x.ID == request.AvailableDateId);

            if (availableDate == null)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoAvailableDatesRecordFound,
                    Status = false
                };
            }

            if (availableDate.AvailableSlots < 1)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoAvailableSlots,
                    Status = false
                };
            }
            // add transaction
            Booking bookingEntity = new()
            {
                DateCreated = DateTime.UtcNow,
                AvailableDateSelected = availableDate.DateAvailable.Date,
                LocationID = request.LocationID,
                ApplicationUserId = request.ApplicationUserId,
                IndividualName = user.UserName,
                BookingStatus = BookingStatus.Pending,
                BookingResult = BookingResultType.None,
                AvailableDateId = request.AvailableDateId
            };

            await _cache.RemoveKeyAsync($"{CacheConstants.AllAvailDateCacheConstant}");

            await _bookingCommandRepository.AddAsync(bookingEntity);

            availableDate.AvailableSlots -= 1;
            if (availableDate.AvailableSlots < 1)
            {
                availableDate.Available = false;
            }

            _availableDateCommandRepository.Update(availableDate);
            await _availableDateCommandRepository.SaveAsync();

            return new BaseResponse
            {
                Message = ResponseMessages.Success,
                Status = true
            };
        }

        public async Task<BaseResponse> CancelBooking(CancelBookingRequest request)
        {
            AvailableDate availableDate = await _availableDateQueryRepository.GetByDefaultAsync(x =>
            x.ID == request.AvailableDateId
            && x.LocationId == request.LocationID);


            Booking booking = await _bookingQueryRepository.GetByDefaultAsync(x =>
            x.AvailableDateId == request.AvailableDateId
            && x.LocationID == request.LocationID
            && x.ApplicationUserId == request.ApplicationUserId);


            if (availableDate == null)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoAvailableDatesRecordFound,
                    Status = false
                };
            }

            if (booking == null)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoBookingRecordFound,
                    Status = false
                };
            }

            await _cache.RemoveKeyAsync($"{CacheConstants.AllAvailDateCacheConstant}");

            //add transaction
            booking.BookingStatus = BookingStatus.Cancelled;
            _bookingCommandRepository.Update(booking);

            availableDate.AvailableSlots += 1;
            availableDate.Available = true;

            _availableDateCommandRepository.Update(availableDate);
            await _availableDateCommandRepository.SaveAsync();

            return new BaseResponse
            {
                Status = true,
                Message = ResponseMessages.Success,
            };
        }
    }
}

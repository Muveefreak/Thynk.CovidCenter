using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.Constants;
using Thynk.CovidCenter.Core.DTOs;
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
        private readonly ICache _cache;
        private const string AllAvailDateCacheConstant = "COVID_CENTER_ALL_AVAIL_DATES";
        private const string AvailDateCacheConstant = "COVID_CENTER_AVAIL_DATES";
        private const string BookingCacheConstant = "COVID_CENTER_BOOKINGS";

        public BookingService(IMapper mapper,
            IDBCommandRepository<Booking> bookingCommandRepository,
            IDBQueryRepository<Booking> bookingQueryRepository,
            IDBCommandRepository<AvailableDate> availableDateCommandRepository,
            IDBQueryRepository<AvailableDate> availableDateQueryRepository,
            IDBQueryRepository<ApplicationUser> applicationUserQueryRepository,
            ICache cache,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _bookingCommandRepository = bookingCommandRepository;
            _bookingQueryRepository = bookingQueryRepository;
            _availableDateCommandRepository = availableDateCommandRepository;
            _availableDateQueryRepository = availableDateQueryRepository;
            _applicationUserQueryRepository = applicationUserQueryRepository;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<BookingResponseModel> GetBookings(BookingStatus status)
        {
            List<Booking> bookings = (await _bookingQueryRepository.GetByAsync(x => x.AvailableDateSelected <= DateTime.UtcNow)).ToList();

            if(!bookings.Any())
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
            AvailableDate availableDate = await _availableDateQueryRepository.GetByDefaultAsync(x => x.DateAvailable == request.AvailableDateSelected);

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
                    Message = ResponseMessages.Success,
                    Status = false
                };
            }
            // add transaction
            Booking bookingEntity = new()
            {
                DateCreated = DateTime.UtcNow,
                AvailableDateSelected = request.AvailableDateSelected,
                LocationID = request.LocationID,
                ApplicationUserId = request.ApplicationUserId,
                IndividualName = request.IndividualName,
                BookingStatus = BookingStatus.Pending,
                BookingResult = BookingResultType.None
            };

            await _cache.RemoveKeyAsync($"{AllAvailDateCacheConstant}");

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

        public async Task<BaseResponse> CancelBooking(CreateBookingRequest request)
        {
            AvailableDate availableDate = await _availableDateQueryRepository.GetByDefaultAsync(x => 
            x.DateAvailable == request.AvailableDateSelected 
            && x.LocationId == request.LocationID);


            Booking booking = await _bookingQueryRepository.GetByDefaultAsync(x => 
            x.AvailableDateSelected == request.AvailableDateSelected
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

            await _cache.RemoveKeyAsync($"{AllAvailDateCacheConstant}");

            //add transaction
            booking.BookingStatus = BookingStatus.Cancelled;
            await _bookingCommandRepository.UpdateAsync(booking);

            availableDate.AvailableSlots += 1;
            availableDate.Available = true;

            await _availableDateCommandRepository.UpdateAsync(availableDate);
            await _availableDateCommandRepository.SaveAsync();

            return new BaseResponse
            {
                Status = true,
                Message = ResponseMessages.Success,
            };
        }
    }
}

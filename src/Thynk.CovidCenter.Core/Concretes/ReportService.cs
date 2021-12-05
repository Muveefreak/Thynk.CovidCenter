using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IDBCommandRepository<Booking> _bookingCommandRepository;
        private readonly IDBQueryRepository<Booking> _bookingQueryRepository;
        private readonly IDBQueryRepository<AvailableDate> _availableDateQueryRepository;
        private readonly IDBQueryRepository<ApplicationUser> _applicationUserQueryRepository;
        private readonly IConfiguration _configuration;
        private readonly ICache _cache;
        private readonly IUtilities _utilities;

        public ReportService(IMapper mapper,
            IDBCommandRepository<Booking> bookingCommandRepository,
            IDBQueryRepository<Booking> bookingQueryRepository,
            IDBQueryRepository<AvailableDate> availableDateQueryRepository,
            IDBQueryRepository<ApplicationUser> applicationUserQueryRepository,
            ICache cache,
            IConfiguration configuration,
            IUtilities utilities)
        {
            _configuration = configuration;
            _bookingCommandRepository = bookingCommandRepository;
            _bookingQueryRepository = bookingQueryRepository;
            _availableDateQueryRepository = availableDateQueryRepository;
            _cache = cache;
            _mapper = mapper;
            _utilities = utilities;
        }
        public async Task<BaseResponse> BookingResult(BookingResultRequest request)
        {
            ApplicationUser user = await _utilities.GetUser(request.ApplicationUserId);
            if (user.UserRole != UserRole.LabAdministrator)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.OnlyLabAdministrator,
                    Status = false
                };
            }

            var booking = await _bookingQueryRepository.GetByDefaultAsync(x => x.ID == request.ID
            && x.BookingStatus == BookingStatus.Pending);

            if (booking == null)
            {
                return new BaseResponse
                {
                    Message = ResponseMessages.NoBookingRecordFound,
                    Status = false
                };
            }

            booking.BookingStatus = BookingStatus.Completed;
            booking.BookingResult = request.BookingResult;
            await _bookingCommandRepository.UpdateAsync(booking);
            await _bookingCommandRepository.SaveAsync();

            return new BaseResponse
            {
                Message = ResponseMessages.Success,
                Status = true
            };
        }

        public async Task<ReportResponseModel> GetResults(GetResultsRequest request)
        {
            ApplicationUser user = await _utilities.GetUser(request.ApplicationUserId);
            if (!(user.UserRole == UserRole.LabAdministrator || user.UserRole == UserRole.Administrator))
            {
                return new ReportResponseModel
                {
                    Message = ResponseMessages.OnlyAdminLabAdministrator,
                    Status = false
                };
            }

            List<ReportDTO> reportListDTO = new();
            ReportDTO reportDTO = new();

            List<AvailableDate> results = _availableDateQueryRepository.GetByAllIncluding(x => x.LocationId == request.LocationId, j => j.Bookings).ToList();

            if (!results.Any())
            {
                return new ReportResponseModel
                {
                    Message = ResponseMessages.NoAvailableDatesRecordFound
                };
            }

            for(var i = 0; i<= results.Count; i++)
            {
                reportDTO.AvailableDate = results[i].DateAvailable;
                reportDTO.BookingCapacity = results[i].AvailableSlots + results[i].Bookings.Count;
                reportDTO.Bookings = results[i].Bookings.Count;
                reportDTO.Tests = results[i].Bookings.Count(x => x.BookingStatus == BookingStatus.Completed 
                && x.BookingResult == BookingResultType.Negative || x.BookingResult == BookingResultType.Positive);
                reportDTO.PositiveCases = results[i].Bookings.Count(x => x.BookingStatus == BookingStatus.Completed
                && x.BookingResult == BookingResultType.Positive);
                reportDTO.NegativeCases = results[i].Bookings.Count(x => x.BookingStatus == BookingStatus.Completed
                && x.BookingResult == BookingResultType.Negative);
                reportListDTO.Add(reportDTO);
            }

            return new ReportResponseModel
            {
                Status = true,
                Message = ResponseMessages.Success,
                Data = reportListDTO.OrderBy(x => x.AvailableDate).ToList()
            };
        }
    }
}

using AutoMapper;
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
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IDBCommandRepository<Booking> _bookingCommandRepository;
        private readonly IDBQueryRepository<Booking> _bookingQueryRepository;
        private readonly IDBQueryRepository<AvailableDate> _availableDateQueryRepository;
        private readonly IConfiguration _configuration;
        private readonly ICache _cache;

        public ReportService(IMapper mapper,
            IDBCommandRepository<Booking> bookingCommandRepository,
            IDBQueryRepository<Booking> bookingQueryRepository,
            IDBQueryRepository<AvailableDate> availableDateQueryRepository,
            ICache cache,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _bookingCommandRepository = bookingCommandRepository;
            _bookingQueryRepository = bookingQueryRepository;
            _availableDateQueryRepository = availableDateQueryRepository;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<BaseResponse> BookingResult(BookingResultRequest request)
        {
            var booking = await _bookingQueryRepository.GetByDefaultAsync(x => x.ID == request.ID
            && x.BookingStatus == BookingStatus.Pending);

            if(booking == null)
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

        public async Task<ReportResponseModel> GetResults(Guid locationId)
        {
            List<ReportDTO> reportDTO = new();

            List<AvailableDate> results = (await _availableDateQueryRepository.GetByAsync(x => x.LocationId == locationId)).ToList();

            if (!results.Any())
            {
                return new ReportResponseModel
                {
                    Message = ResponseMessages.NoAvailableDatesRecordFound
                };
            }

            return new ReportResponseModel
            {
                Status = true,
                Message = ResponseMessages.Success,
                Data = reportDTO
            };
        }
    }
}

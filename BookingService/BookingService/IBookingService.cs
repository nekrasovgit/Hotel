﻿using BookingService.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.BookingService
{
    public interface IBookingService
    {
        Task<string> CreateReservation(BookingRequestModel model);
        Task<string> CancelReservation(Guid id);
        IEnumerable<ReservationDTO> GetReservation();
        Task CheckReservation(JobMessage message);
        Task<string> VerifyReservationId(VerificationReservationId verification);
        Task ChangeStatus(Payment payment);
    }
}

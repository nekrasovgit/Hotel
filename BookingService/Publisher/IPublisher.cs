﻿using BookingService.BookingService;
using BookingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Publisher
{
    public interface IPublisher
    {
        Task<string> VerifyRoomId(VerificationRoomId verificationtion);

    }
}

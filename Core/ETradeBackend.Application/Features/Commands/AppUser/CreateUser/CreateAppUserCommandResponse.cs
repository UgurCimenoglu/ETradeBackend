﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateAppUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
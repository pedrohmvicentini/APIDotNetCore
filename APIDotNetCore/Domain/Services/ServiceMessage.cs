﻿using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _iMessage;

        public ServiceMessage(IMessage iMessage)
        {
            _iMessage = iMessage;
        }
    }
}
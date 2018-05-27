﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message.Implementations
{
    [Serializable]
    public class AuthenticateTokenMessage : IMessage
    {
        public DateTime CreatedAt => DateTime.Now;

        public MessageTypes Type => MessageTypes.AuthenticateToken;

        public string Token { get; set; }

        public AuthenticateTokenMessage(string token)
        {
            Token = token;
        }
    }
}
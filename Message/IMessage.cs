﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message
{
    public interface IMessage
    {
        DateTime CreatedAt { get; } // За ненадобностью - убрать.
        MessageTypes Type { get; }
    }
}
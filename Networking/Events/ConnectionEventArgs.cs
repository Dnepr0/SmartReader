﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Networking.Events
{
    public class ConnectionEventArgs : EventArgs
    {
        public IConnection Connection;

        public ConnectionEventArgs(IConnection connection)
        {
            Connection = connection;
        }
    }
}

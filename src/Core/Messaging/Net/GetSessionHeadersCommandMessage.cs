﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gibraltar.Messaging.Net
{
    /// <summary>
    /// Command for retrieving the list of session headers
    /// </summary>
    public class GetSessionHeadersCommandMessage : NetworkMessage
    {
        /// <summary>
        /// create a new session headers command message
        /// </summary>
        public GetSessionHeadersCommandMessage()
        {
            TypeCode = NetworkMessageTypeCode.GetSessionHeaders;
            Version = new Version(1, 0);
        }

        /// <summary>
        /// Write the packet to the stream
        /// </summary>
        protected override void OnWrite(Stream stream)
        {
        }

        /// <summary>
        /// Read packet data from the stream
        /// </summary>
        protected override void OnRead(Stream stream)
        {
        }    
    }
}

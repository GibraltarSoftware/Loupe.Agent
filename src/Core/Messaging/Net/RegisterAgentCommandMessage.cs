﻿
#region File Header

/********************************************************************
 * COPYRIGHT:
 *    This software program is furnished to the user under license
 *    by Gibraltar Software, Inc, and use thereof is subject to applicable 
 *    U.S. and international law. This software program may not be 
 *    reproduced, transmitted, or disclosed to third parties, in 
 *    whole or in part, in any form or by any manner, electronic or
 *    mechanical, without the express written consent of Gibraltar Software, Inc,
 *    except to the extent provided for by applicable license.
 *
 *    Copyright © 2008 by Gibraltar Software, Inc.  All rights reserved.
 *******************************************************************/
using System;
using System.IO;
using Gibraltar.Data;

#endregion File Header

namespace Gibraltar.Messaging.Net
{
    /// <summary>
    /// Sent by an agent to register itself with the remote server or desktop
    /// </summary>
    public class RegisterAgentCommandMessage : NetworkMessage
    {
        private Guid m_SessionId;

        internal RegisterAgentCommandMessage()
        {
            TypeCode = NetworkMessageTypeCode.RegisterAgentCommand;
            Version = new Version(1, 0);
        }

        /// <summary>
        /// Create a new agent registration message for the specified session Id
        /// </summary>
        /// <param name="sessionId"></param>
        public RegisterAgentCommandMessage(Guid sessionId)
            : this()
        {
            m_SessionId = sessionId;
        }

        /// <summary>
        /// The session Id identifying the agent
        /// </summary>
        public Guid SessionId { get { return m_SessionId; } }

        /// <summary>
        /// Write the packet to the stream
        /// </summary>
        protected override void OnWrite(Stream stream)
        {
            BinarySerializer.SerializeValue(stream, m_SessionId);
        }

        /// <summary>
        /// Read packet data from the stream
        /// </summary>
        protected override void OnRead(Stream stream)
        {
            BinarySerializer.DeserializeValue(stream, out m_SessionId);
        }
    }
}

﻿#region File Header
// /********************************************************************
//  * COPYRIGHT:
//  *    This software program is furnished to the user under license
//  *    by Gibraltar Software Inc, and use thereof is subject to applicable 
//  *    U.S. and international law. This software program may not be 
//  *    reproduced, transmitted, or disclosed to third parties, in 
//  *    whole or in part, in any form or by any manner, electronic or
//  *    mechanical, without the express written consent of Gibraltar Software Inc,
//  *    except to the extent provided for by applicable license.
//  *
//  *    Copyright © 2008 - 2015 by Gibraltar Software, Inc.  
//  *    All rights reserved.
//  *******************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Gibraltar
{
    /// <summary>
    /// This exception indicates that an unexpected packet version was encountered.
    /// </summary>
    /// <remarks>This exception occurs when processing a Gibraltar log packet with a version
    /// which is not understood by this version of Gibraltar.  This usually means that an older
    /// version of Gibraltar is trying to read a log stream generated by a newer and incompatible
    /// version of Gibraltar.  For more information, see the root class, Exception.</remarks>
    [Serializable]
    public class GibraltarPacketVersionException : GibraltarException
    {

        /// <summary>
        /// Initializes a new instance of the GibraltarPacketVersionException class.
        /// </summary>
        /// <remarks>This constructor initializes the Message property of the new instance to a system-supplied
        /// message that describes the error and takes into account the current system culture.
        /// For more information, see the base constructor in Exception.</remarks>
        public GibraltarPacketVersionException()
        {
            // Just the base default constructor
        }

        /// <summary>
        /// Initializes a new instance of the GibraltarPacketVersionException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message string.</param>
        /// <remarks>This constructor initializes the Message property of the new instance using the
        /// message parameter.  The InnerException property is left as a null reference.
        /// For more information, see the base constructor in Exception.</remarks>
        public GibraltarPacketVersionException(string message)
            : base(message)
        {
            // Just the base constructor
        }

        /// <summary>
        /// Initializes a new instance of the GibraltarPacketVersionException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message string.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a
        /// null reference if no inner exception is specified.</param>
        /// <remarks>An exception that is thrown as a direct result of a previous exception should include
        /// a reference to the previous exception in the innerException parameter.
        /// For more information, see the base constructor in Exception.</remarks>
        public GibraltarPacketVersionException(string message, Exception innerException)
            : base(message, innerException)
        {
            // Just the base constructor
        }

        /// <summary>
        /// Initializes a new instance of the GibraltarPacketVersionException class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about
        /// the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about
        /// the source or destination.</param>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object
        /// transmitted over a stream.  For more information, see the base constructor in Exception.</remarks>
        protected GibraltarPacketVersionException(System.Runtime.Serialization.SerializationInfo info,
                                                  System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            // Just the base constructor            
        }

        /// <summary>
        /// Initializes a new instance of the GibraltarPacketVersionException class to a standard
        /// error message string given a supplied version parameter.
        /// </summary>
        /// <param name="version">The unexpected version encountered</param>
        /// <remarks>This is the preferred way to initialize this exception type, because
        /// this constructor automatically formats the message string to be "Unexpected version: {0}",
        /// where {0} is replaced with the provided version argument.</remarks>
        public GibraltarPacketVersionException(int version)
            : this(string.Format(CultureInfo.InvariantCulture, "Unexpected version: {0}", version))
        {
            // Just call the other constructor
        }
    }
}
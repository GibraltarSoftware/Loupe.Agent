﻿
namespace Gibraltar.Server.Client
{
    /// <summary>
    /// The format of a web request provided to the GibraltarWebClient
    /// </summary>
    public interface IWebRequest
    {
        /// <summary>
        /// Raised when the request is canceled prior to successful execution completion
        /// </summary>
        event WebRequestEventHandler Canceled;

        /// <summary>
        /// Raised whenever a request completes (even if it is canceled)
        /// </summary>
        event WebRequestEventHandler Complete;

        /// <summary>
        /// Indicates if the web request requires authentication (so the channel should authenticate before attempting the request)
        /// </summary>
        bool RequiresAuthentication { get; }

        /// <summary>
        /// Indicates if the web request supports authentication, so if the server requests credentials the request can provide them.
        /// </summary>
        bool SupportsAuthentication { get; }
/*
        /// <summary>
        /// The sub-URL (including query string) of the command
        /// </summary>
        string ResourceUrl { get; }

        /// <summary>
        /// The http command to execute on the specified resource
        /// </summary>
        HttpCommand Command { get; }

        /// <summary>
        /// The type of data expected in response to the command
        /// </summary>
        WebDataType ResponseType { get; }

        /// <summary>
        /// The type of data being provided to the command
        /// </summary>
        WebDataType RequestType { get; }

        /// <summary>
        /// The value to use in the request (may be null if RequestType is None)
        /// </summary>
        object RequestValue { get; }

        /// <summary>
        /// The value returned by the request.
        /// </summary>
        object ResponseValue { get; set; }
 */

        /// <summary>
        /// Perform the request against the specified web client connection.
        /// </summary>
        /// <param name="connection"></param>
        void ProcessRequest(IWebChannelConnection connection);
    }
}

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

using System.Collections.Generic;

namespace Gibraltar.Server.Client
{
    /// <summary>
    /// The low level web client connection used by the web channel.
    /// </summary>
    public interface IWebChannelConnection
    {
        /// <summary>
        /// Downloads the resource with the specified URI to a byte array
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <param name="timeout">The number of seconds to wait for a response to the request</param>
        /// <returns>A byte array containing the body of the response from the resource</returns>
        byte[] DownloadData(string relativeUrl, int? timeout = -1);

        /// <summary>
        /// Downloads the resource with the specified URI to a byte array
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <param name="additionalHeaders">Extra headers to add to the request</param>
        /// <param name="timeout">The number of seconds to wait for a response to the request</param>
        /// <returns>A byte array containing the body of the response from the resource</returns>
        byte[] DownloadData(string relativeUrl, IList<NameValuePair<string>> additionalHeaders, int? timeout = -1);

        /// <summary>
        /// Downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <param name="destinationFileName"></param>
        /// <param name="timeout">The number of seconds to wait for a response to the request</param>
        void DownloadFile(string relativeUrl, string destinationFileName, int? timeout = -1);

        /// <summary>
        /// Downloads the resource with the specified URI to a string
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <param name="timeout">The number of seconds to wait for a response to the request</param>
        /// <returns></returns>
        string DownloadString(string relativeUrl, int? timeout = -1);

        /// <summary>
        /// Uploads the provided byte array to the specified URI using the provided method.
        /// </summary>
        /// <param name="relativeUrl">The URI of the resource to receive the data. This URI must identify a resource that can accept a request sent with the method.</param>
        /// <param name="method">The HTTP method used to send the string to the resource.  If null, the default is POST</param>
        /// <param name="contentType">The content type to inform the server of for this file</param>
        /// <param name="data"></param>
        /// <param name="additionalHeaders">Extra headers to add to the request</param>
        /// <param name="timeout">The number of seconds to wait for a response to the request</param>
        /// <returns>A byte array containing the body of the response from the resource</returns>
        byte[] UploadData(string relativeUrl, string method, string contentType, byte[] data, IList<NameValuePair<string>> additionalHeaders= null, int? timeout = -1);


        /// <summary>
        /// Uploads the specified local file to the specified URI using the specified method
        /// </summary>
        /// <param name="relativeUrl">The URI of the resource to receive the file. This URI must identify a resource that can accept a request sent with the method.</param>
        /// <param name="method">The HTTP method used to send the string to the resource.  If null, the default is POST</param>
        /// <param name="contentType">The content type to inform the server of for this file</param>
        /// <param name="sourceFileNamePath"></param>
        /// <param name="timeout">The number of seconds to wait for a response to the request</param>
        /// <returns>A byte array containing the body of the response from the resource</returns>
        byte[] UploadFile(string relativeUrl, string method, string contentType, string sourceFileNamePath, int? timeout = -1);

        /// <summary>
        /// Uploads the specified string to the specified resource, using the specified method
        /// </summary>
        /// <param name="relativeUrl">The URI of the resource to receive the string. This URI must identify a resource that can accept a request sent with the method.</param>
        /// <param name="method">The HTTP method used to send the string to the resource.  If null, the default is POST</param>
        /// <param name="contentType">The content type to inform the server of for this file</param>
        /// <param name="data">The string to be uploaded. </param>
        /// <param name="timeout">The number of seconds to wait for a response to the request</param>
        /// <returns>A string containing the body of the response from the resource</returns>
        string UploadString(string relativeUrl, string method, string contentType, string data, int? timeout = -1);
    }
}
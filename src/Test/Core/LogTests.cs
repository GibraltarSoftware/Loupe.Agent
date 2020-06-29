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
using System.Diagnostics;
using System.Threading;
using Gibraltar.Monitor;
using Loupe.Extensibility.Data;
using NUnit.Framework;


namespace Gibraltar.Test.Core
{
    /// <summary>
    /// Test the Log class's logging capabilities.  Metrics are not verified in this class
    /// </summary>
    [TestFixture]
    public class LogTests
    {
        /// <summary>
        /// Write a log message using each different trace log statement on the Log object
        /// </summary>
        [Test]
        public void WriteTrace()
        {
            Log.Trace("This is a call to Log.Trace with no arguments");
            Log.Trace("This is a call to Log.Trace with two arguments #1:{0}, #2:{1}", 1, 2);

            Log.Write(LogMessageSeverity.Information, "Unit Tests", null, "This is a call to Log.Write with no arguments");
            Log.Write(LogMessageSeverity.Information, "Unit Tests", null, "This is a call to Log.Write with two arguments #1:{0}, #2:{1}", 1, 2);

            Exception exception = new GibraltarException("This is a dummy exception to test API calls.");

            Log.Trace(exception, "This is a call to Log.Trace with an exception and no arguments");
            Log.Trace(exception, "This is a call to Log.Trace with an exception and two arguments #1:{0}, #2:{1}", 1, 2);

            Log.Write(LogMessageSeverity.Warning, LogWriteMode.Queued, exception, "Unit Tests", null, "This is a call to Log.Write with an exception and no arguments");
            Log.Write(LogMessageSeverity.Warning, LogWriteMode.Queued, exception, "Unit Tests", null, "This is a call to Log.Write with an exception and two arguments #1:{0}, #2:{1}", 1, 2);

            Log.Write(LogMessageSeverity.Verbose, "Unit Tests", "This is a call to Log.Write with a caption and null description", null);
            Log.Write(LogMessageSeverity.Verbose, "Unit Tests", "This is a call to Log.Write with a caption and description", "with no formatting arguments");
            Log.Write(LogMessageSeverity.Verbose, "Unit Tests", "This is a call to Log.Write with a caption and description", "formatted with two arguments #1:{0}, #2:{1}", 1, 2);

            Log.Write(LogMessageSeverity.Information, LogWriteMode.WaitForCommit, null, "Unit Tests", "This is a call to Log.Write with WaitForCommit and null exception and with a caption and null description", null);
        }

        /// <summary>
        /// Write a log message with an attached exception object.
        /// </summary>
        [Test]
        public void WriteException()
        {
            Log.Write(LogMessageSeverity.Warning, LogWriteMode.Queued, new AssertionException("This is our test assertion exception"), "Unit Tests", 
                      "Test of logging exception attachment.", null);

            Log.Write(LogMessageSeverity.Warning, LogWriteMode.Queued, new AssertionException("This is our top exception", new AssertionException("This is our middle exception", new AssertionException("This is our bottom exception"))),
                      "Unit Tests", "Test of logging exception attachment with nested exceptions.", null);
        }

        /// <summary>
        /// Write many messages so we can verify order in the viewer.  The goal is to write many messages so that the order has to be
        /// controlled by sequence number, not timestamp.
        /// </summary>
        [Test]
        public void WriteMessagesForOrderTesting()
        {
            for (int curLogMessage = 1; curLogMessage < 3000; curLogMessage++)
            {
                Log.Trace("This is log message #{0}", curLogMessage);
            }
        }

        /// <summary>
        /// Write a log message using the full trace message entrance point
        /// </summary>
        [Test]
        public void WriteTraceFullFormat()
        {
            //do one that should be pinned on US
            Log.WriteMessage(LogMessageSeverity.Verbose,  LogWriteMode.Queued, 0, null, null, "This message should be verbose and ascribed to the LogTests class.", null);
            Log.WriteMessage(LogMessageSeverity.Critical, LogWriteMode.Queued, 1, null, null, "This message should be critical and ascribed to whatever is calling our test class.", null);
            Log.WriteMessage(LogMessageSeverity.Error,    LogWriteMode.Queued, -1, null, null, "This message should be error and also ascribed to the LogTests class.", null);
        }

        /// <summary>
        /// Test our handling of errors in formatted log message calls.
        /// </summary>
        [Test]
        public void WriteBadFormat()
        {
            string[] messageArray = new string[5];
            string fullMessage = null;

            // We aren't checking the actual returns, just making sure these don't result in exceptions.
            // ToDo: Add logic to check that the result contains each item of data?
            fullMessage = ErrorNotifier.SafeFormat(null, "This is a test\tof a bad format call\n{0}, {1}, {2}", "zero", 1);
            messageArray[0] = fullMessage;
            fullMessage = ErrorNotifier.SafeFormat(null, "This is a test\r\nof a legal format call\t{0},\t{1},\t{2}", 0, null, "two");
            messageArray[1] = fullMessage;
            fullMessage = ErrorNotifier.SafeFormat(null, "This is a test\n\rof a bad format call\t{0},\t{1},\n{2}\t{3}", null, "one", "\"two\"");
            messageArray[2] = fullMessage;
            fullMessage = ErrorNotifier.SafeFormat(null, null, 0, "\"null format test\"", 2);
            messageArray[3] = fullMessage;
            fullMessage = ErrorNotifier.SafeFormat(null, string.Empty, "empty\rformat\ntest", 1);
            messageArray[4] = fullMessage;

            Log.Trace("This is a test\tof a bad format call to Log.Trace()\n{0}, {1}, {2}", "zero", 1);
            Log.Trace("This is a test\r\nof a legal format call to Log.Trace()\t{0},\t{1},\t{2}", 0, null, "two");
            Log.Trace("This is a test\n\rof a bad format call to Log.Trace()\t{0},\t{1},\n{2}\t{3}", null, "one", "\"two\"");
            Log.Trace((string)null, 0, "null format test", 2);
            Log.Trace(string.Empty, "empty format test", 1);

            Thread.Sleep(2000); // Give it time to put stuff in the log.

            return;
        }

        /// <summary>
        /// Test our performance flushing a large message queue.
        /// </summary>
        [Test]
        public void MessageQueueFlushTest()
        {
            const int count = 10000;
            MessageSourceProvider source = new MessageSourceProvider(0, true);
            Messaging.IMessengerPacket[] batch = new Messaging.IMessengerPacket[count];
            for (int i = 0; i < count; i++)
            {
                batch[i] = Log.MakeLogPacket(LogMessageSeverity.Verbose, "GibraltarTest", "Test.Core.LogMessage.Performance.Flush",
                                             source, null, null, null, null, "Batch message #{0} of {1}", i, count);
            }

            Log.Write(LogMessageSeverity.Information, LogWriteMode.WaitForCommit, null, "Unit Tests", "Clearing message queue", null);

            DateTimeOffset startWrite = DateTimeOffset.UtcNow;

            Log.Write(batch, LogWriteMode.Queued);

            DateTimeOffset startFlush = DateTimeOffset.UtcNow;

            Log.Write(LogMessageSeverity.Information, LogWriteMode.WaitForCommit, null, "Unit Tests", "Message batch flushed",
                "All {0} messages have been flushed.", count);

            DateTimeOffset endFlush = DateTimeOffset.UtcNow;

            TimeSpan writeDuration = startFlush - startWrite;
            TimeSpan flushDuration = endFlush - startFlush;

            Trace.TraceInformation("Write of {0:N0} messages to queue took {1:F3} ms and flush took {2:F3} ms.",
                count, writeDuration.TotalMilliseconds, flushDuration.TotalMilliseconds);

            for (int i = 0; i < count; i++)
            {
                batch[i] = Log.MakeLogPacket(LogMessageSeverity.Verbose, "GibraltarTest", "Test.Core.LogMessage.Performance.Flush",
                                             source, null, null, null, null, "Batch message #{0} of {1}", i, count);
            }

            DateTimeOffset startWriteThrough = DateTimeOffset.UtcNow;

            Log.Write(batch, LogWriteMode.WaitForCommit);

            DateTimeOffset endWriteThrough = DateTimeOffset.UtcNow;

            TimeSpan writeThroughDuration = endWriteThrough - startWriteThrough;

            Trace.TraceInformation("Write of {0:N0} messages as WaitForCommit took {1:F3} ms.",
                count, writeThroughDuration.TotalMilliseconds);
        }

        [Test]
        public void ApplicationUserAssignForCurrentPrincipal()
        {
            Log.UserResolutionNotifier.ResolveUser += OnResolveUserForCurrentPrincipal;
            try
            {
                Log.Write(LogMessageSeverity.Information, "LogTests.ApplicationUser.Assign For Current Principal", "This message should be attributed to the current user", 
                    "And we should get the resolution event following it.");
            }
            finally
            {
                Log.UserResolutionNotifier.ResolveUser -= OnResolveUserForCurrentPrincipal;
            }
        }

        private void OnResolveUserForCurrentPrincipal(object sender, ResolveUserEventArgs e)
        {
            var identity = e.Principal.Identity;
            var newUser = e.GetUser();
            newUser.Key = identity.Name;
            newUser.Organization = "Unit test";
            newUser.EmailAddress = "support@gibraltarsoftware.com";
            newUser.Phone = "443 738-0680";
            newUser.Properties.Add("Customer Key", "1234-5678-90");
            newUser.Properties.Add("License Check", null);
        }

        [Test]
        public void ApplicationUserAssignJustOnce()
        {
            Log.UserResolutionNotifier.ResolveUser += OnResolveUserJustOnce;
            try
            {
                m_UserResolutionRequests = 0;
                Log.WriteMessage(LogMessageSeverity.Information, LogWriteMode.WaitForCommit, "Loupe", "LogTests.ApplicationUser.Assign Just Once", null, "ApplicationUserAssignJustOnce",
                    null, null, "This message should be attributed to ApplicationUserAssignJustOnce",
                    "And we should get the resolution event following it.");
                Assert.AreEqual(1, m_UserResolutionRequests, "We didn't get exactly one resolution");

                Log.WriteMessage(LogMessageSeverity.Information, LogWriteMode.WaitForCommit, "Loupe", "LogTests.ApplicationUser.Assign Just Once", null, "ApplicationUserAssignJustOnce",
                    null, null, "This message should be attributed to ApplicationUserAssignJustOnce",
                    "And we should NOT get the resolution event following it.");
                Assert.AreEqual(1, m_UserResolutionRequests, "We didn't get exactly one resolution");
            }
            finally
            {
                Log.UserResolutionNotifier.ResolveUser -= OnResolveUserJustOnce;
            }            
        }

        private void OnResolveUserJustOnce(object sender, ResolveUserEventArgs e)
        {
            m_UserResolutionRequests++;

            //all I have to do to lock in the user is get it..
            var newUser = e.GetUser();
        }

        private int m_UserResolutionRequests;
    }
}

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
using System.Reflection;
using Gibraltar.Monitor.Internal;
using Gibraltar.Serialization;

#endregion File Header

namespace Gibraltar.Monitor.Internal
{
    /// <summary>
    /// The serializable representation of a custom sampled metric
    /// </summary>
    internal class EventMetricPacket : MetricPacket, ICachedPacket, IPacketObjectFactory<Metric, MetricDefinition>, IComparable<EventMetricPacket>, IEquatable<EventMetricPacket>
    {
        /// <summary>
        /// Create a new event metric packet for the provided metric definition and a specific instance.
        /// </summary>
        /// <param name="metricDefinitionPacket">The metric definition packet that defines this metric</param>
        /// <param name="instanceName">The unique instance name of this metric or null for the default instance.</param>
        public EventMetricPacket(EventMetricDefinitionPacket metricDefinitionPacket, string instanceName)
            : base(metricDefinitionPacket, instanceName)
        {
        }

        /// <summary>
        /// Create a new event metric packet for rehydration
        /// </summary>
        /// <param name="session"></param>
        public EventMetricPacket(Session session) 
            : base(session)
        {            
        }

        #region Public Properties and Methods

        /// <summary>
        /// Compare this event metric packet to another to determine sort order
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(EventMetricPacket other)
        {
            //we just gateway to our base object.
            return base.CompareTo(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public override bool Equals(object other)
        {
            //use our type-specific override
            return Equals(other as EventMetricPacket);
        }

        /// <summary>
        /// Determines if the provided object is identical to this object.
        /// </summary>
        /// <param name="other">The object to compare this object to</param>
        /// <returns>True if the objects represent the same data.</returns>
        public bool Equals(EventMetricPacket other)
        {
            //We're really just a type cast, refer to our base object
            return base.Equals(other);
        }

        /// <summary>
        /// Provides a representative hash code for objects of this type to spread out distribution
        /// in hash tables.
        /// </summary>
        /// <remarks>Objects which consider themselves to be Equal (a.Equals(b) returns true) are
        /// expected to have the same hash code.  Objects which are not Equal may have the same
        /// hash code, but minimizing such overlaps helps with efficient operation of hash tables.
        /// </remarks>
        /// <returns>
        /// an int representing the hash code calculated for the contents of this object
        /// </returns>
        public override int GetHashCode()
        {
            int myHash = base.GetHashCode(); // Equals defers to base, so just use hash code for inherited base type

            return myHash;
        }

        #endregion

        #region IPacket Members

        private const int SerializationVersion = 1;

        /// <summary>
        /// The list of packets that this packet depends on.
        /// </summary>
        /// <returns>An array of IPackets, or null if there are no dependencies.</returns>
        IPacket[] IPacket.GetRequiredPackets()
        {
            //we need to add in required packets for the metric definition values.  If we don't,
            //they will never get written out.
            EventMetricDefinitionPacket metricDefinitionPacket = (EventMetricDefinitionPacket)DefinitionPacket;
            IPacket[] requiredPackets = new IPacket[metricDefinitionPacket.MetricValues.Count];

            for (int curValueIndex = 0; curValueIndex < metricDefinitionPacket.MetricValues.Count; curValueIndex++)
            {
                requiredPackets[curValueIndex] = ((EventMetricValueDefinition)metricDefinitionPacket.MetricValues[curValueIndex]).Packet;
            }

            return requiredPackets;
        }

        PacketDefinition IPacket.GetPacketDefinition()
        {
            string typeName = MethodBase.GetCurrentMethod().DeclaringType.Name;
            PacketDefinition definition = new PacketDefinition(typeName, SerializationVersion, true);
            //we only exist to do the required packet thing
            return definition;
        }

        void IPacket.WriteFields(PacketDefinition definition, SerializedPacket packet)
        {
            //we only exist to do the required packet thing
        }

        void IPacket.ReadFields(PacketDefinition definition, SerializedPacket packet)
        {
            //we only exist to do the required packet thing
        }

        #endregion

        #region IPacketObjectFactory<Metric, MetricDefinition> Members

        Metric IPacketObjectFactory<Metric, MetricDefinition>.GetDataObject(MetricDefinition optionalParent)
        {
            //this is just here for us to be able to create our derived type for the generic infrastructure
            return new EventMetric((EventMetricDefinition)optionalParent, this);
        }

        #endregion
    }
}

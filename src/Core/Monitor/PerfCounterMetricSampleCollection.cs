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
using Gibraltar.Monitor.Internal;

#endregion

namespace Gibraltar.Monitor
{
    /// <summary>
    /// A collection of metric samples for one performance counter instance.
    /// </summary>
    public sealed class PerfCounterMetricSampleCollection : MetricSampleCollection
    {
        //we are caching our type-safe handle for convenience
        private readonly PerfCounterMetric m_PerfCounterMetric;

        /// <summary>
        /// Create a new sample collection for the specified metric object
        /// </summary>
        /// <param name="metric"></param>
        internal PerfCounterMetricSampleCollection(PerfCounterMetric metric)
            : base(metric)
        {
            m_PerfCounterMetric = metric;
        }

        /// <summary>
        /// Add the specified PerfCounterMetricSample object to the collection
        /// </summary>
        /// <param name="newPerfCounterMetricSample">The new PerfCounterMetricSample object to add</param>
        public void Add(PerfCounterMetricSample newPerfCounterMetricSample)
        {
            if (newPerfCounterMetricSample == null)
            {
                throw new ArgumentNullException(nameof(newPerfCounterMetricSample), "A new performance counter metric sample object must be provided to add it to the collection.");
            }
            
            //our base object does all the work
            base.Add(newPerfCounterMetricSample);
        }

        /// <summary>
        /// Add a new performance counter metric sample from the specified sample packet
        /// </summary>
        /// <param name="newMetricSamplePacket">The sample packet to create a new metric sample object from</param>
        internal PerfCounterMetricSample Add(PerfCounterMetricSamplePacket newMetricSamplePacket)
        {
            if (newMetricSamplePacket == null)
            {
                throw new ArgumentNullException(nameof(newMetricSamplePacket), "A new performance counter metric sample packet object must be provided to add it to the collection.");
            }

            //now we have to create a new sample object to wrap the provided packet
            PerfCounterMetricSample newPerfCounterMetricSample = new PerfCounterMetricSample(m_PerfCounterMetric, newMetricSamplePacket);

            //and forward to our normal add routine
            Add(newPerfCounterMetricSample);

            //returning our new wrapped object
            return newPerfCounterMetricSample;
        }

        /// <summary>
        /// Select a metric sample by its numerical index
        /// </summary>
        /// <remarks>Setting a metric sample to a particular index is not supported and will result in an exception being thrown.</remarks>
        /// <param name="index"></param>
        /// <returns></returns>
        public new PerfCounterMetricSample this[int index]
        {
            get
            {
                return (PerfCounterMetricSample)base[index];
            }
            set
            {
                //we don't want to support setting an object by index, we are sorted.
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// The metric this sample is for.
        /// </summary>
        public new PerfCounterMetric Metric { get { return (PerfCounterMetric)base.Metric; } }

        /// <summary>
        /// The first object in the collection
        /// </summary>
        public new PerfCounterMetricSample First { get { return (PerfCounterMetricSample)base.First; } }

        /// <summary>
        /// The last object in the collection
        /// </summary>
        public new PerfCounterMetricSample Last { get { return (PerfCounterMetricSample)base.Last; } }
    }
}

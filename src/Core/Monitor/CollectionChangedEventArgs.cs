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

#endregion


namespace Gibraltar.Monitor
{
    /// <summary>
    /// The event arguments for a collection change event, indicating what change was made to which collection.
    /// </summary>
    /// <remarks>For add, remove, and update events the specific object in the collection that was added, removed, or updated is provided.</remarks>
    /// <typeparam name="CollectionType"></typeparam>
    /// <typeparam name="CollectedObjectType"></typeparam>
    public class CollectionChangedEventArgs<CollectionType, CollectedObjectType> : EventArgs
    {
        private readonly CollectionType m_Collection;
        private readonly CollectedObjectType m_Subject;
        private readonly CollectionAction m_Action = CollectionAction.NoChange;

        /// <summary>
        /// Create a new object with the provided collection object, subject, and action
        /// </summary>
        /// <param name="collection">The collection object affected</param>
        /// <param name="subject">Optional.  The specific object in the collection that was affected</param>
        /// <param name="action">The action that was performed on the collection</param>
        public CollectionChangedEventArgs(CollectionType collection, CollectedObjectType subject, CollectionAction action)
        {
            m_Collection = collection;
            m_Subject = subject;
            m_Action = action;
        }

        /// <summary>
        /// The object that was just added, removed, or updated.  May be null in the case of No Change and Clear.
        /// </summary>
        public CollectedObjectType Subject
        {
            get { return m_Subject; }
        }

        /// <summary>
        /// The collection that was changed
        /// </summary>
        public CollectionType Collection
        {
            get { return m_Collection; }
        }

        /// <summary>
        /// The action performed on the collection
        /// </summary>
        public CollectionAction Action
        {
            get { return m_Action; }
        }
    }

}

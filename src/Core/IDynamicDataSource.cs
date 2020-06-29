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
using System.ComponentModel;
using System.Text;

namespace Gibraltar
{
    /// <summary>
    /// Provides data updates to a BindingListController
    /// </summary>
    public interface IDynamicDataSource<TList>
    {
        /// <summary>
        /// Synchronize the passed BindingListEx with the authoritative data managed by this object.
        /// </summary>
        /// <param name="list">The list to be updated by this method</param>
        /// <param name="deferredUpdates">Optional.  A list of objects that want to do deferred 
        /// commits of changes to be invoked during the synchronization process</param>
        void Synchronize(TList list, out IList<IDynamicData> deferredUpdates);

        /// <summary>
        /// The category to log synchronization events under
        /// </summary>
        string LogCategory { get; }
    }

    /// <summary>
    /// Implement to allow deferred updates to data objects with the Binding List Controller
    /// </summary>
    public interface IDynamicData
    {
        /// <summary>
        /// Called by the binding list controller to have the object commit its changes
        /// </summary>
        /// <param name="synchObject"></param>
        void CommitChanges(ISynchronizeInvoke synchObject);

        /// <summary>
        /// Called by the binding list controller when 
        /// </summary>
        void Clear();

        /// <summary>
        /// Indicates if the object has any deferred changes
        /// </summary>
        bool HasDeferredChanges { get; }
    }
}

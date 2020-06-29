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

namespace Gibraltar.Serialization
{
    /// <summary>
    /// FieldDefinition is only used internally by PacketDefinition to hold the name and type of a field
    /// </summary>
    public class FieldDefinition
    {
        private readonly FieldType m_FieldType;
        private readonly string m_FieldName;

        /// <summary>
        /// Create a new field definition.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        public FieldDefinition(string fieldName, FieldType fieldType)
        {
            m_FieldName = fieldName;
            m_FieldType = fieldType;
        }

        /// <summary>
        /// The exact serializable field type of the field
        /// </summary>
        public FieldType FieldType { get { return m_FieldType; } }

        /// <summary>
        /// The unique name of this field within the packet
        /// </summary>
        public string Name { get { return m_FieldName; } }

        /// <summary>
        /// Indicates if this field definition can store data of the provided type losslessly.
        /// </summary>
        /// <param name="type">The prospective value type to be serialized</param>
        /// <returns>True if the provided type can be converted into this field type without
        /// losing precision.</returns>
        /// <remarks>This method will indicate if a provided value type is sufficiently compatible
        /// with the exact type of this field to be converted without losing data.  For example,
        /// a signed integer can be stored in an unsigned integer field.  A short can be stored 
        /// as a long, etc.</remarks>
        public bool IsCompatible(FieldType type)
        {
            //exact matches are always good.
            if (type == m_FieldType)
                return true;
             
            //now handle odd overrides.
            switch(type)
            {
                case FieldType.Int32:
                    return ((m_FieldType == Serialization.FieldType.Int64) || (m_FieldType == Serialization.FieldType.Double));
                case FieldType.UInt32:
                    return ((m_FieldType == Serialization.FieldType.UInt64) || (m_FieldType == Serialization.FieldType.Double));
                case FieldType.DateTimeOffset:
                    return (m_FieldType == Serialization.FieldType.DateTime);
            }

            //if it isn't one of our specific overrides, no dice
            return false;
        }
    }
}
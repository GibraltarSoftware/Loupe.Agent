﻿

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Gibraltar.Serialization;
using Gibraltar.Serialization.Internal;

namespace Gibraltar.Serialization.Internal
{
    /// <summary>
    /// This helper class is used by PacketReader to manage the list of IPacketFactory
    /// classes used to deserialize a stream of packets.
    /// </summary>
    internal class PacketFactory
    {
        private readonly Dictionary<string, IPacketFactory> m_PacketFactories;
        private readonly GenericPacketFactory m_GenericFactory;

        /// <summary>
        /// Creates an empty list of IPacketFactory objects.
        /// </summary>
        public PacketFactory()
        {
            m_PacketFactories = new Dictionary<string, IPacketFactory>();
            m_GenericFactory = new GenericPacketFactory();
        }

        /// <summary>
        /// Registers a SimplePacketFactory wrappering the specified type.
        /// </summary>
        /// <param name="type">Type must implement IPacket.</param>
        public void RegisterType(Type type)
        {
            var factory = new SimplePacketFactory(type);
            if ( factory.IsValid )
                RegisterFactory(type.Name, factory);
        }

        /// <summary>
        /// Associates the specified IPacketFactory with a type name
        /// </summary>
        /// <param name="typeName">Should refer to a type that implements IPacket</param>
        /// <param name="factory">IPacketFactory class used to </param>
        public void RegisterFactory(string typeName, IPacketFactory factory)
        {
            m_PacketFactories[typeName] = factory;
        }

        public IPacketFactory GetPacketFactory(string typeName)
        {
            IPacketFactory factory;

            if (m_PacketFactories.TryGetValue(typeName, out factory))
                return factory;
#if DEBUG
//            Debug.Print("Warning:  No packet factory found for type, will use Generic factory.");
//            if (Debugger.IsAttached)
//                Debugger.Break(); // Stop in debugger, ignore in production.
#endif

            return m_GenericFactory;
        }
    }
}
﻿using Rhisis.Game.Abstractions.Protocol;
using Sylver.Network.Data;

namespace Rhisis.Network.Packets.World
{
    public class AwakeningPacket : IPacketDeserializer
    {
        /// <summary>
        /// Gets the item id.
        /// </summary>
        public int Item { get; private set; }

        /// <inheritdoc />
        public void Deserialize(INetPacketStream packet)
        {
            Item = packet.Read<int>();
        }
    }
}
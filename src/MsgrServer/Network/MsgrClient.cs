﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using Aura.Shared.Network;
using Aura.Shared.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aura.Msgr.Network
{
	public class MsgrClient : BaseClient
	{
		protected override void EncodeBuffer(ref byte[] buffer)
		{
		}

		protected override byte[] BuildPacket(Packet packet)
		{
			var packetSize = packet.GetSize();

			// Calculate header size
			var headerSize = 3;
			int n = packetSize;
			do { headerSize++; n >>= 7; } while (n != 0);

			// Write header
			var result = new byte[headerSize + packetSize];
			result[0] = 0x55;
			result[1] = 0x12;
			result[2] = 0x00;

			// Length
			var ptr = 3;
			n = packetSize;
			do
			{
				result[ptr++] = (byte)(n > 0x7F ? (0x80 | (n & 0xFF)) : n & 0xFF);
				n >>= 7;
			}
			while (n != 0);

			// Write packet
			packet.Build(ref result, ptr);

			return result;
		}
	}
}
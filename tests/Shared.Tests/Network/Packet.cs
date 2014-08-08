// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using Aura.Shared.Network;
using Xunit;

namespace Aura.Tests.Shared.Network
{
	public class PacketTests
	{
		/// <summary>
		/// Building packet to byte array.
		/// </summary>
		[Fact]
		public void BuildingNewPacket()
		{
			var packet = new Packet(0x1020, 0x3040506070809010);
			packet.PutByte(1);
			packet.PutShort(2);
			packet.PutInt(3);
			packet.PutLong(4);
			packet.PutFloat(5.6f);
			packet.PutString("test");
			packet.PutBin(new byte[] { 1, 2, 3, 4, 5, 6, 8, 9 });

			var built = packet.Build();

			Assert.Equal(built, new byte[] { 0x00, 0x00, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0x10, 0x2B, 0x07, 0x00, 0x01, 0x01, 0x02, 0x00, 0x02, 0x03, 0x00, 0x00, 0x00, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x05, 0x33, 0x33, 0xB3, 0x40, 0x06, 0x00, 0x05, 0x74, 0x65, 0x73, 0x74, 0x00, 0x07, 0x00, 0x08, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x08, 0x09 });
		}

		/// <summary>
		/// Reading packet from byte array.
		/// </summary>
		[Fact]
		public void ReadingPacketFromBuffer()
		{
			var buffer = new byte[] { 0x00, 0x00, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0x10, 0x2B, 0x07, 0x00, 0x01, 0x01, 0x02, 0x00, 0x02, 0x03, 0x00, 0x00, 0x00, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x05, 0x33, 0x33, 0xB3, 0x40, 0x06, 0x00, 0x05, 0x74, 0x65, 0x73, 0x74, 0x00, 0x07, 0x00, 0x08, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x08, 0x09 };

			var packet = new Packet(buffer, 0);

			Assert.Equal(1, packet.GetByte());
			Assert.Equal(2, packet.GetShort());
			Assert.Equal(3, packet.GetInt());
			Assert.Equal(4, packet.GetLong());
			Assert.Equal(5.6f, packet.GetFloat());
			Assert.Equal("test", packet.GetString());
			Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6, 8, 9 }, packet.GetBin());
		}

		/// <summary>
		/// Reading packet from built packet.
		/// </summary>
		[Fact]
		public void ReadingPacketFromPacket()
		{
			var packetW = new Packet(0x1020, 0x3040506070809010);
			packetW.PutByte(1);
			packetW.PutShort(2);
			packetW.PutInt(3);
			packetW.PutLong(4);
			packetW.PutFloat(5.6f);
			packetW.PutString("test");
			packetW.PutBin(new byte[] { 1, 2, 3, 4, 5, 6, 8, 9 });

			var buffer = packetW.Build();

			var packetR = new Packet(buffer, 0);

			Assert.Equal(1, packetR.GetByte());
			Assert.Equal(2, packetR.GetShort());
			Assert.Equal(3, packetR.GetInt());
			Assert.Equal(4, packetR.GetLong());
			Assert.Equal(5.6f, packetR.GetFloat());
			Assert.Equal("test", packetR.GetString());
			Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6, 8, 9 }, packetR.GetBin());
		}

		/// <summary>
		/// Getting size required for packet.
		/// </summary>
		[Fact]
		public void GettingSize()
		{
			var packet = new Packet(0x1020, 0x3040506070809010);
			packet.PutByte(1);
			packet.PutShort(2);
			packet.PutInt(3);
			packet.PutLong(4);
			packet.PutFloat(5.6f);
			packet.PutBin(new byte[] { 1, 2, 3, 4, 5, 6, 8, 9 });

			Assert.Equal(50, packet.GetSize());

			for (int i = 0; i < 1000; ++i)
				packet.PutString("test" + i);

			Assert.Equal(10942, packet.GetSize());
		}
	}
}

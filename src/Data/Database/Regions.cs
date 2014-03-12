// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System.Collections.Generic;
using System.Linq;

namespace Aura.Data.Database
{
	public class RegionData
	{
		public int Id { get; internal set; }
		public string Name { get; internal set; }
	}

	/// <summary>
	/// Indexed by map name.
	/// </summary>
	public class RegionDb : DatabaseCSVIndexed<string, RegionData>
	{
		public Dictionary<int, RegionData> EntriesId = new Dictionary<int, RegionData>();

		public RegionData Find(int id)
		{
			return this.EntriesId.GetValueOrDefault(id);
		}

		public int TryGetRegionId(string region, int fallBack = 0)
		{
			int regionId = fallBack;
			if (!int.TryParse(region, out regionId))
			{
				var mapInfo = this.Find(region);
				if (mapInfo != null)
					regionId = mapInfo.Id;
			}

			return regionId;
		}

		[MinFieldCount(2)]
		protected override void ReadEntry(CSVEntry entry)
		{
			var info = new RegionData();
			info.Id = entry.ReadInt();
			info.Name = entry.ReadString();

			this.Entries[info.Name] = info;
			this.EntriesId[info.Id] = info;
		}
	}
}

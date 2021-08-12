using System.Collections.Generic;
using Labyrinth.GameObjects.Enums;

namespace Labyrinth.GameObjects.Dictionaries
{
	public static class TileObjectDictionary
	{
		private static Dictionary<TileObjectType, TileObjectInfo> _dictionary;

		public static Dictionary<TileObjectType, TileObjectInfo> Dictionary => _dictionary ?? (_dictionary = BuildDictionary());

		private static Dictionary<TileObjectType, TileObjectInfo> BuildDictionary()
		{
			var dictionary = new Dictionary<TileObjectType, TileObjectInfo>
			{
				{TileObjectType.Null, new TileObjectInfo {TextureName = "null"} },
				{TileObjectType.Slime, new TileObjectInfo {TextureName = "tile_object\\slime"} },
				{TileObjectType.Treasure, new TileObjectInfo {TextureName = "tile_object\\treasure"} }
			};

			return dictionary;
		}
	}

	public class TileObjectInfo
	{
		public string TextureName { get; set; }
	}
}

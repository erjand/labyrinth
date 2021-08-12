using System.Collections.Generic;
using Labyrinth.GameObjects.Enums;

namespace Labyrinth.GameObjects.Dictionaries
{
	public static class TileDictionary
	{
		private static Dictionary<TileType, TileInfo> _dictionary;

		public static Dictionary<TileType, TileInfo> Dictionary => _dictionary ?? (_dictionary = BuildDictionary());
		
		private static Dictionary<TileType, TileInfo> BuildDictionary()
		{
			var dictionary = new Dictionary<TileType, TileInfo>
			{
				{ TileType.Null, new TileInfo { TextureName = "null", IsPassable = false } },
				{ TileType.Floor, new TileInfo { TextureName = "tile\\floor", IsPassable = true } },
				{ TileType.Wall, new TileInfo { TextureName = "tile\\wall", IsPassable = false } }
			};

			return dictionary;
		}
	}

	public class TileInfo
	{
		public string TextureName { get; set; }
		public bool IsPassable { get; set; }
	}
}

using System.Collections.Generic;
using Labyrinth.GameObjects.Enums;

namespace Labyrinth.GameObjects.Dictionaries
{
	public static class TileEdgeDictionary
	{
		private static Dictionary<TileEdgeType, TileEdgeInfo> _dictionary;

		public static Dictionary<TileEdgeType, TileEdgeInfo> Dictionary => _dictionary ?? (_dictionary = BuildDictionary());

		private static Dictionary<TileEdgeType, TileEdgeInfo> BuildDictionary()
		{
			var dictionary = new Dictionary<TileEdgeType, TileEdgeInfo>
			{
				{ TileEdgeType.Null, new TileEdgeInfo { TextureName = "null", IsPassable = true } },
				{ TileEdgeType.WallHorizontal, new TileEdgeInfo { TextureName = "tile_edge\\wall_skinny_horizontal", IsPassable = false } },
				{ TileEdgeType.WallVertical, new TileEdgeInfo { TextureName = "tile_edge\\wall_skinny_vertical", IsPassable = false } },
				{ TileEdgeType.DoorHorizontalClosed, new TileEdgeInfo { TextureName = "tile_edge\\door_horizontal_closed", IsPassable = false } },
				{ TileEdgeType.DoorVerticalClosed, new TileEdgeInfo { TextureName = "tile_edge\\door_vertica_closedl", IsPassable = false } },
				{ TileEdgeType.DoorHorizontalOpen, new TileEdgeInfo { TextureName = "tile_edge\\door_horizontal_open", IsPassable = false } },
				{ TileEdgeType.DoorVerticalOpen, new TileEdgeInfo { TextureName = "tile_edge\\door_vertical_open", IsPassable = false } }
			};

			return dictionary;
		}
	}

	public class TileEdgeInfo
	{
		public string TextureName { get; set; }
		public bool IsPassable { get; set; }
	}
}

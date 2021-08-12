using Labyrinth.GameObjects.Dictionaries;
using Labyrinth.GameObjects.Enums;
using Labyrinth.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth.GameObjects
{
	public class TileEdge
	{
		public TileEdgeType TileEdgeType { get; set; }
		private Texture2D Texture { get; set; }
		public TileEdgeOrientation TileEdgeOrientation { get; set; }
		public Tile FromTile { get; }
		public Tile ToTile { get; }

		public bool IsPassable => TileEdgeDictionary.Dictionary[TileEdgeType].IsPassable;

		public TileEdge(Tile fromTile, Tile toTile)
		{
			FromTile = fromTile;
			ToTile = toTile;
		}

		public void LoadContent()
		{
			Texture = GameServices.GetService<ContentManager>().Load<Texture2D>(TileEdgeDictionary.Dictionary[TileEdgeType].TextureName);
		}

		public void Draw()
		{
			GameServices.GetService<SpriteBatch>().Draw(Texture, 
				GridHelper.PixelFromAdjacentTiles(FromTile.GridPosition, ToTile.GridPosition, TileEdgeOrientation), Color.White);
		}

		public void ChangeTileEdgeType(TileEdgeType newTileEdgeType)
		{
			TileEdgeType = newTileEdgeType;
			LoadContent();
		}
	}
}

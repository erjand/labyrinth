using System.Collections.Generic;
using System.Linq;
using Labyrinth.GameObjects.Dictionaries;
using Labyrinth.GameObjects.Enums;
using Labyrinth.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth.GameObjects
{
	public class Tile
	{
		public TileType TileType { get; private set; }
		private Texture2D Texture { get; set; }
		public Point GridPosition { get; }
		public List<TileEdge> AdjacentTileEdges = new List<TileEdge>();
		public int? Distance;
		public Tile ParentTile;

		public TileObject TileObject { get; private set; }

		public bool IsPassable => TileDictionary.Dictionary[TileType].IsPassable;

		public bool IsBoardEdge() => GridPosition.X == 0 || GridPosition.X == BoardHelper.BoardSize - 1
					|| GridPosition.Y == 0 || GridPosition.Y == BoardHelper.BoardSize - 1;

		public Tile(int gridX, int gridY, TileType tileType)
		{
			GridPosition = new Point(gridX, gridY);
			TileType = tileType;
			TileObject = new TileObject(TileObjectType.Null);
		}

		public void LoadContent()
		{
			Texture = GameServices.GetService<ContentManager>().Load<Texture2D>(TileDictionary.Dictionary[TileType].TextureName);
			TileObject.LoadContent();
		}

		public void Draw()
		{
			GameServices.GetService<SpriteBatch>().Draw(Texture, GridHelper.PixelFromGrid(GridPosition), Color.White);

			TileObject.Draw(GridHelper.PixelFromGrid(GridPosition));
		}

		public void ChangeTileType(TileType newTileType)
		{
			TileType = newTileType;
			LoadContent();
		}

		public void ChangeTileObjectType(TileObjectType newTileObjectType)
		{
			TileObject = new TileObject(newTileObjectType);
			TileObject.LoadContent();
		}

		public void AddAdjacentTileEdges(Point tileGridPosition)
		{
			if (tileGridPosition.Y != 0)
			{
				var northTileEdge = new Tile(tileGridPosition.X, tileGridPosition.Y - 1, 
					Board.Instance.TileAt(new Point(tileGridPosition.X, tileGridPosition.Y - 1)).TileType);
				if (northTileEdge.IsPassable)
				{
					AdjacentTileEdges.Add(new TileEdge(this, northTileEdge));
				}
			}

			if (tileGridPosition.Y != BoardHelper.BoardSize - 1)
			{
				var southTileEdge = new Tile(tileGridPosition.X, tileGridPosition.Y + 1,
					Board.Instance.TileAt(new Point(tileGridPosition.X, tileGridPosition.Y + 1)).TileType);
				if (southTileEdge.IsPassable)
				{
					AdjacentTileEdges.Add(new TileEdge(this, southTileEdge));
				}
			}

			if (tileGridPosition.X != 0)
			{
				var westTileEdge = new Tile(tileGridPosition.X - 1, tileGridPosition.Y,
					Board.Instance.TileAt(new Point(tileGridPosition.X - 1, tileGridPosition.Y)).TileType);
				if (westTileEdge.IsPassable)
				{
					AdjacentTileEdges.Add(new TileEdge(this, westTileEdge));
				}
			}

			if (tileGridPosition.X != BoardHelper.BoardSize - 1)
			{
				var eastTileEdge = new Tile(tileGridPosition.X + 1, tileGridPosition.Y,
					Board.Instance.TileAt(new Point(tileGridPosition.X + 1, tileGridPosition.Y)).TileType);
				if (eastTileEdge.IsPassable)
				{
					AdjacentTileEdges.Add(new TileEdge(this, eastTileEdge));
				}
			}
		}

		public IEnumerable<Tile> GetAdjacentTileEdges()
		{
			return AdjacentTileEdges.Select(edge => edge.ToTile).ToList();
		}
	}
}

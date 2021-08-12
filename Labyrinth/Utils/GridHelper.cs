using Labyrinth.GameObjects;
using Labyrinth.GameObjects.Enums;
using Microsoft.Xna.Framework;

namespace Labyrinth.Utils
{
	/// <summary>
	/// We want all Grid to Pixel conversations to flow through here by way of .Draw()
	/// Everything else should only know / care about its GridPosition
	/// </summary>
	public static class GridHelper
	{
		public const int DefaultGameWindowWidth = 1280;
		public const int DefaultGameWindowHeight = 720;

		private const float TileSize = 32;

		private const float AbsoluteBoardPositionX = 300f;
		private const float AbsoluteBoardPositionY = 25 + TileSize / 2;

		public static Vector2 PixelFromGrid(Point gridPosition)
		{
			var pixelPosition = new Vector2
			{
				X = AbsoluteBoardPositionX - TileSize / 2 + gridPosition.X * TileSize,
				Y = AbsoluteBoardPositionY - TileSize / 2 + gridPosition.Y * TileSize
			};

			return pixelPosition;
		}

		public static Vector2 PixelFromAdjacentTiles(Point fromTile, Point toTile, TileEdgeOrientation orientation)
		{
			if (orientation == TileEdgeOrientation.Horizontal)
			{
				if (fromTile.Y < toTile.Y)
				{
					var pixelPosition = new Vector2
					{
						X = AbsoluteBoardPositionX - TileSize / 2 + fromTile.X * TileSize,
						Y = AbsoluteBoardPositionY - TileSize / 2 + fromTile.Y * TileSize
					};

					return pixelPosition;
				}

				if (fromTile.Y > toTile.Y)
				{
					var pixelPosition = new Vector2
					{
						X = AbsoluteBoardPositionX - TileSize / 2 + fromTile.X * TileSize,
						Y = AbsoluteBoardPositionY - TileSize / 2 + fromTile.Y * TileSize
					};

					return pixelPosition;
				}
			}

			if (orientation == TileEdgeOrientation.Vertical)
			{
				if (fromTile.X < toTile.X)
				{
					var pixelPosition = new Vector2
					{
						X = AbsoluteBoardPositionX - TileSize / 2 + fromTile.X * TileSize,
						Y = AbsoluteBoardPositionY - TileSize / 2 + fromTile.Y * TileSize
					};

					return pixelPosition;
				}

				if (fromTile.X > toTile.X)
				{
					var pixelPosition = new Vector2
					{
						X = AbsoluteBoardPositionX - TileSize / 2 + fromTile.X * TileSize,
						Y = AbsoluteBoardPositionY - TileSize / 2 + fromTile.Y * TileSize
					};

					return pixelPosition;
				}
			}

			return new Vector2(0,0);
		}
	}
}

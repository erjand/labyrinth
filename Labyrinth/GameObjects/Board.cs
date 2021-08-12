using System.Collections.Generic;
using Labyrinth.GameObjects.Enums;
using Labyrinth.Utils;
using Microsoft.Xna.Framework;

namespace Labyrinth.GameObjects
{
	public class Board
	{
		public Player[] Players { get; }
		public Tile[,] Tiles { get; }
		public Dictionary<Point, Tile> TileGraph;

		private static Board _instance;
		public static Board Instance => _instance ?? (_instance = new Board());

		public Tile TileAt(Point point)
		{
			return Tiles[point.X, point.Y];
		}

		public Tile GetTileAtGridPosition(Point gridPosition)
		{
			TileGraph.TryGetValue(gridPosition, out Tile tile);
			return tile;
		}

		private Board()
		{
			Tiles = new Tile[BoardHelper.BoardSize, BoardHelper.BoardSize];
			TileGraph = new Dictionary<Point, Tile>();
			Players = new Player[2];
			Players[0] = new Player(new Point(1,1), "avatars\\avatar_blue", PlayerType.Player);
			Players[1] = new Player(new Point(18, 18), "avatars\\avatar_red", PlayerType.Enemy);
		}
		
		public void LoadContent()
		{
			BoardHelper.ConstructBoard();
			BoardHelper.PlaceRandomWallBlocks();
			BoardHelper.PlaceRandomTileObject(TileObjectType.Slime, 30);
			BoardHelper.PlaceRandomTileObject(TileObjectType.Treasure, 10);
			
			foreach (Tile tile in Tiles)
			{
				tile.LoadContent();
			}

			foreach (Player player in Players)
			{
				player.LoadContent();
			}

			BoardHelper.PopulateTileGraph();
		}

		public void Update()
		{
			BoardHelper.CheckVictoryCondition();
			
			if (GameStateManager.CurrentGameState() == GameState.PlayerTurn)
			{
				BoardHelper.HandlePlayerMovementAndUpdateScore(Players[0]);
			}

			BoardHelper.CheckVictoryCondition();

			if (GameStateManager.CurrentGameState() == GameState.EnemyTurn)
			{
				BoardHelper.HandleEnemyMovementAndUpdateScore(Players[1]);
			}
		}

		public void Draw()
		{
			if (GameStateManager.CurrentGameState() == GameState.PlayerTurn ||
				GameStateManager.CurrentGameState() == GameState.EnemyTurn)
			{
				foreach (Tile tile in Instance.Tiles)
				{
					tile.Draw();
				}

				foreach (Player player in Players)
				{
					player.Draw();
				}
			}
		}

		public static void NewGame()
		{
			_instance = new Board();
			_instance.LoadContent();
		}
	}
}

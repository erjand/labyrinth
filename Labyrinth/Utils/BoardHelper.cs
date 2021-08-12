using System;
using System.Collections.Generic;
using Labyrinth.GameLogic.Logic;
using Labyrinth.GameObjects;
using Labyrinth.GameObjects.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth.Utils
{
	public static class BoardHelper
	{
		public const int BoardSize = 20;
		private const int WallBlockCount = 10;

		private static KeyboardState _previousKeyboardState;
		private static bool _hasMoved;

		public static int RemainingTreasureCount
		{
			get
			{
				var treasureCount = 0;
				foreach (Tile tile in Board.Instance.Tiles)
				{
					if (tile.TileObject.TileObjectType == TileObjectType.Treasure)
					{
						treasureCount++;
					}
				}

				return treasureCount;
			}
		}

		public static int SlimeCount
		{
			get
			{
				var slimeCount = 0;
				foreach (Tile tile in Board.Instance.Tiles)
				{
					if (tile.TileObject.TileObjectType == TileObjectType.Slime)
					{
						slimeCount++;
					}
				}

				return slimeCount;
			}
		}

		public static void CheckVictoryCondition()
		{
			if (RemainingTreasureCount == 0)
			{
				GameStateManager.ChangeGameState(GameState.PostGame);
			}
		}

		public static void ConstructBoard()
		{
			// Instantiate all null tiles
			for (var i = 0; i < BoardSize; i++)
			{
				for (var j = 0; j < BoardSize; j++)
				{
					var tile = new Tile(i, j, TileType.Null);
					Board.Instance.Tiles[i, j] = tile;
				}
			}

			// Add walls and floor
			foreach (Tile tile in Board.Instance.Tiles)
			{
				tile.ChangeTileType(tile.IsBoardEdge()
					? TileType.Wall
					: TileType.Floor);
			}
		}

		public static void PlaceRandomWallBlocks()
		{
			var randomBottom = 1;
			var randomTop = BoardSize - 1;
			var random = new Random(Guid.NewGuid().GetHashCode());

			for (int i = 0; i < WallBlockCount;)
			{
				int randomX = random.Next(randomBottom, randomTop);
				int randomY = random.Next(randomBottom, randomTop);

				if (randomX == 1 && randomY == 1 || randomX == 18 && randomY == 18)
				{
					continue;
				}

				Board.Instance.Tiles[randomX, randomY].ChangeTileType(TileType.Wall);
				i++;
			}
		}

		public static void PlaceRandomTileObject(TileObjectType tileObjectType, int count)
		{
			var randomBottom = 1;
			var randomTop = BoardSize - 1;
			var random = new Random(Guid.NewGuid().GetHashCode());

			for (int i = 0; i < count;)
			{
				int randomX = random.Next(randomBottom, randomTop);
				int randomY = random.Next(randomBottom, randomTop);

				if (randomX == 1 && randomY == 1 || randomX == 18 && randomY == 18)
				{
					continue;
				}

				if (Board.Instance.Tiles[randomX, randomY].TileType == TileType.Wall)
				{
					continue;
				}

				Board.Instance.Tiles[randomX, randomY].ChangeTileObjectType(tileObjectType);
				i++;
			}
		}

		public static void HandlePlayerMovementAndUpdateScore(Player player)
		{
			_hasMoved = false;

			HandleMovementInput(player, Keyboard.GetState());
			
			if (_hasMoved)
			{
				UpdateScore(player);
				SwapPlayerGameState(player);
			}
		}

		public static void HandleEnemyMovementAndUpdateScore(Player player)
		{
			_hasMoved = false;

			Stack<Tile> path = Pathfinding.GetEnemyMovementPath(player);

			Tile destinationTile = path.Pop();
			Tile nextMove = Pathfinding.GetNextMove(destinationTile);

			Point currentPosition = player.GridPosition;

			player.HandleIntendedMove(nextMove.GridPosition); // TODO Make enemy find a new route if player is blocking them

			if (player.GridPosition != currentPosition)
			{
				_hasMoved = true;
			}

			if (_hasMoved)
			{
				UpdateScore(player);
				SwapPlayerGameState(player);
			}
		}

		private static void HandleMovementInput(Player player, KeyboardState currentKeyboardState)
		{
			InputHelper.LockInput = false;

			if (InputHelper.IsKeyPressedSingle(_previousKeyboardState, currentKeyboardState, Keys.A))
			{
				InputHelper.LockInput = true;
				AttemptMove(player, MoveDirection.Left);
			}

			if (InputHelper.IsKeyPressedSingle(_previousKeyboardState, currentKeyboardState, Keys.D))
			{
				InputHelper.LockInput = true;
				AttemptMove(player, MoveDirection.Right);
			}

			if (InputHelper.IsKeyPressedSingle(_previousKeyboardState, currentKeyboardState, Keys.S))
			{
				InputHelper.LockInput = true;
				AttemptMove(player, MoveDirection.Down);
			}

			if (InputHelper.IsKeyPressedSingle(_previousKeyboardState, currentKeyboardState, Keys.W))
			{
				InputHelper.LockInput = true;
				AttemptMove(player, MoveDirection.Up);
			}

			InputHelper.LockInput = false;
			_previousKeyboardState = currentKeyboardState;
		}

		private static void AttemptMove(Player player, MoveDirection moveDirection)
		{
			Point currentPosition = player.GridPosition;
			player.Move(moveDirection);

			if (player.GridPosition != currentPosition)
			{
				_hasMoved = true;
			}
		}

		private static void UpdateScore(Player player)
		{
			Point newPlayerPosition = player.GridPosition;

			if (Board.Instance.TileAt(newPlayerPosition).TileObject.TileObjectType == TileObjectType.Treasure)
			{
				player.UpdateScore(1);
				Board.Instance.TileAt(newPlayerPosition).ChangeTileObjectType(TileObjectType.Null);
			}

			if (Board.Instance.TileAt(newPlayerPosition).TileObject.TileObjectType == TileObjectType.Slime)
			{
				player.UpdateScore(-1);
				Board.Instance.TileAt(newPlayerPosition).ChangeTileObjectType(TileObjectType.Null);
			}
		}

		private static void SwapPlayerGameState(Player player)
		{
			if (player.PlayerType == PlayerType.Player)
			{
				GameStateManager.ChangeGameState(GameState.EnemyTurn);
			}

			if (player.PlayerType == PlayerType.Enemy)
			{
				GameStateManager.ChangeGameState(GameState.PlayerTurn);
			}
		}

		public static bool DoesTileContainPlayer(Point point)
		{
			if (Board.Instance.Players[0].GridPosition == point)
			{
				return true;
			}

			if (Board.Instance.Players[1].GridPosition == point)
			{
				return false;
			}

			return false;
		}

		public static void PopulateTileGraph()
		{
			for (var i = 0; i < BoardHelper.BoardSize; i++)
			{
				for (var j = 0; j < BoardSize; j++)
				{
					if (Board.Instance.TileAt(new Point(i, j)).IsPassable)
					{
						var tile = new Tile(i, j, Board.Instance.TileAt(new Point(i, j)).TileType);
						tile.AddAdjacentTileEdges(new Point(i, j)); //TODO Why is this not working here?
						Board.Instance.TileGraph.Add(new Point(i, j), tile);
					}

					if (!Board.Instance.TileAt(new Point(i, j)).IsPassable)
					{
						var tile = new Tile(i, j, Board.Instance.TileAt(new Point(i, j)).TileType);
						Board.Instance.TileGraph.Add(new Point(i, j), tile);
					}
				}
			}
		}
	}
}

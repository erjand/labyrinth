using System;
using System.Collections.Generic;
using System.Linq;
using Labyrinth.GameObjects;
using Labyrinth.GameObjects.Enums;
using Microsoft.Xna.Framework;

namespace Labyrinth.GameLogic.Logic
{
	public static class Pathfinding
	{
		public static Tile GetNextMove(Tile finalTile)
		{
			while (true)
			{
				if (finalTile.Distance <= 1)
				{
					return finalTile;
				}

				finalTile = finalTile.ParentTile;
			}
		}

		public static Stack<Tile> GetEnemyMovementPath(Player player)
		{
			Point targetPoint = CalculateTargetPoint(player);

			Stack<Tile> path = TraverseBoardGraph(Board.Instance.TileGraph,
				Board.Instance.TileAt(player.GridPosition), Board.Instance.TileAt(targetPoint));

			return path;
		}
		
		private static Point CalculateTargetPoint(Player player)
		{
			var treasureTileGridPositionList = new List<Point>();

			foreach (Tile tile in Board.Instance.Tiles)
			{
				if (tile.TileObject.TileObjectType == TileObjectType.Treasure)
				{
					treasureTileGridPositionList.Add(tile.GridPosition);
				}
			}

			var closestTreasureTile = new Point(100, 100);

			foreach (Point treasureTileGridPosition in treasureTileGridPositionList)
			{
				int currentClosestDistance = GetManhattanDistance(player.GridPosition, closestTreasureTile);
				int potentialNewDistance = GetManhattanDistance(player.GridPosition, treasureTileGridPosition);

				if (potentialNewDistance < currentClosestDistance)
				{
					closestTreasureTile = treasureTileGridPosition;
				}
			}

			return closestTreasureTile;
		}

		private static int GetManhattanDistance(Point playerGridPosition, Point treasureTileGridPosition)
		{
			return Math.Abs(playerGridPosition.X - treasureTileGridPosition.X) +
			       Math.Abs(playerGridPosition.Y - treasureTileGridPosition.Y);
		}

		private static Stack<Tile> TraverseBoardGraph(Dictionary<Point, Tile> tileGraph, Tile startTile, Tile endTile)
		{
			foreach (Tile tile in tileGraph.Values.ToList())
			{
				tile.Distance = null;
				tile.ParentTile = null;
			}

			var queue = new Queue<Tile>();
			var path = new Stack<Tile>();

			startTile.Distance = 0;
			startTile.AddAdjacentTileEdges(startTile.GridPosition);
			queue.Enqueue(startTile);

			while (queue.Count > 0)
			{
				Tile currentTile = queue.Dequeue();
				path.Push(currentTile);

				// DEBUG Console
				Console.Write(
					"\rDEBUG: Current Distance: " + path.Peek().Distance + "; " +
					"Visiting Vertex (" + currentTile.GridPosition + ")");

				// If we've hit the end Vertex, we've found a path through
				if (Equals(currentTile.GridPosition, endTile.GridPosition))
				{
					return path;
				}

				foreach (Tile adjacentVertex in currentTile.GetAdjacentTileEdges())
				{
					Tile tile = Board.Instance.GetTileAtGridPosition(new Point(adjacentVertex.GridPosition.X, adjacentVertex.GridPosition.Y));

					if (tile.Distance == null)
					{
						tile.Distance = currentTile.Distance + 1;
						tile.ParentTile = currentTile;
						queue.Enqueue(tile);
					}
				}
			}

			// No successful route found
			return path;
		}
	}
}

using Labyrinth.GameObjects.Enums;
using Labyrinth.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth.GameObjects
{
	public class Player
	{
		private Texture2D _texture2D;
		private readonly string _textureName;

		public PlayerType PlayerType { get; }
		public Point GridPosition { get; private set; }
		public int Score { get; private set; }

		public Player(Point startGridPosition, string textureName, PlayerType playerType)
		{
			GridPosition = startGridPosition;
			_textureName = textureName;
			PlayerType = playerType;
			Score = 0;
		}

		public void LoadContent()
		{
			_texture2D = GameServices.GetService<ContentManager>().Load<Texture2D>(_textureName);
		}

		public void Draw()
		{
			GameServices.GetService<SpriteBatch>().Draw(_texture2D, GridHelper.PixelFromGrid(GridPosition), Color.White);
		}

		public void UpdateScore(int addToScore)
		{
			if (Score + addToScore != -1)
			{
				Score = Score + addToScore;
			}
		}

		public void Move(MoveDirection moveDirection)
		{
			if (moveDirection == MoveDirection.Left)
			{
				HandleIntendedMove(new Point(GridPosition.X - 1, GridPosition.Y));
			}

			if (moveDirection == MoveDirection.Right)
			{
				HandleIntendedMove(new Point(GridPosition.X + 1, GridPosition.Y));
			}

			if (moveDirection == MoveDirection.Up)
			{
				HandleIntendedMove(new Point(GridPosition.X, GridPosition.Y - 1));
			}

			if (moveDirection == MoveDirection.Down)
			{
				HandleIntendedMove(new Point(GridPosition.X, GridPosition.Y + 1));
			}
		}

		public void HandleIntendedMove(Point intendedMove)
		{
			if (Board.Instance.TileAt(intendedMove).IsPassable && !BoardHelper.DoesTileContainPlayer(intendedMove))
			{
				GridPosition = intendedMove;
			}
		}
	}
}

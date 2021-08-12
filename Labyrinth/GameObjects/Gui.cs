using Labyrinth.Startup;
using Labyrinth.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth.GameObjects
{
	public class Gui
	{
		private Texture2D _background;
		private Texture2D _frame;
		private Texture2D _selectorArrow;
		private static SpriteFont _pixelSpriteFont;

		private static KeyboardState _previousKeyboardState;

		private static Vector2 SelectorArrowPosition { get; set; }

		public void LoadContent()
		{
			var contentManager = GameServices.GetService<ContentManager>();

			_pixelSpriteFont = contentManager.Load<SpriteFont>("pixelFont");
			_background = contentManager.Load<Texture2D>("cave_background");
			_frame = contentManager.Load<Texture2D>("ui\\empty_frame_240x240");
			_selectorArrow = contentManager.Load<Texture2D>("ui\\selector_arrow_horizontal_right");

			SelectorArrowPosition = new Vector2(35, 93);
		}

		public void Update()
		{
			if (GameStateManager.CurrentGameState() == GameState.PreGame || GameStateManager.CurrentGameState() == GameState.PostGame)
			{
				KeyboardState currentKeyboardState = Keyboard.GetState();

				if (InputHelper.IsKeyPressedSingle(_previousKeyboardState, currentKeyboardState, Keys.W))
				{
					SelectorArrowPosition = new Vector2(35, 93);
				}

				if (InputHelper.IsKeyPressedSingle(_previousKeyboardState, currentKeyboardState, Keys.S))
				{
					SelectorArrowPosition = new Vector2(35, 143);
				}

				if (InputHelper.IsKeyPressedSingle(_previousKeyboardState, currentKeyboardState, Keys.Enter))
				{
					if ((int)SelectorArrowPosition.Y == 93)
					{
						Board.NewGame();
						GameStateManager.ChangeGameState(GameState.PlayerTurn);
					}

					if ((int)SelectorArrowPosition.Y == 143)
					{
						LabyrinthGame.Instance.Exit();
					}
				}

				_previousKeyboardState = currentKeyboardState;
			}
		}

		public void Draw()
		{
			var spriteBatch = GameServices.GetService<SpriteBatch>();

			if (GameStateManager.CurrentGameState() == GameState.PreGame)
			{
				DrawPreGame(spriteBatch);
			}

			if (GameStateManager.CurrentGameState() == GameState.PlayerTurn ||
				GameStateManager.CurrentGameState() == GameState.EnemyTurn)
			{
				DrawGameTime(spriteBatch);
			}

			if (GameStateManager.CurrentGameState() == GameState.PostGame)
			{
				DrawPostGame(spriteBatch);
			}
		}

		private void DrawPreGame(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_frame, new Vector2(25, 75), Color.White);

			spriteBatch.DrawString(_pixelSpriteFont, "NEW GAME", new Vector2(85, 100), Color.Black);
			spriteBatch.DrawString(_pixelSpriteFont, "EXIT", new Vector2(85, 150), Color.Black);
			spriteBatch.Draw(_selectorArrow, SelectorArrowPosition, Color.White);

			spriteBatch.Draw(_frame, new Vector2(425, 75), Color.White);

			spriteBatch.DrawString(_pixelSpriteFont, "MOVE WITH WASD", new Vector2(445, 100), Color.Black);
			spriteBatch.DrawString(_pixelSpriteFont, "SELECT WITH ENTER", new Vector2(445, 150), Color.Black);
		}

		private void DrawGameTime(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_background, new Rectangle(0, 0, GridHelper.DefaultGameWindowWidth,
				GridHelper.DefaultGameWindowHeight), Color.White);

			spriteBatch.Draw(_frame, new Vector2(25, 75), Color.White);

			spriteBatch.DrawString(_pixelSpriteFont, "PLAYER SCORE " + Board.Instance.Players[0].Score,
				new Vector2(85, 100), Color.Blue);

			spriteBatch.DrawString(_pixelSpriteFont, "ENEMY SCORE " + Board.Instance.Players[1].Score,
				new Vector2(85, 150), Color.Red);

			spriteBatch.DrawString(_pixelSpriteFont, "TREASURE " + BoardHelper.RemainingTreasureCount,
				new Vector2(85, 200), Color.Black);

			spriteBatch.DrawString(_pixelSpriteFont, "SLIME " + BoardHelper.SlimeCount,
				new Vector2(85, 250), Color.Black);

			if (GameStateManager.CurrentGameState() == GameState.PlayerTurn)
			{
				spriteBatch.Draw(_selectorArrow, new Vector2(35, 93), Color.White);
			}

			if (GameStateManager.CurrentGameState() == GameState.EnemyTurn)
			{
				spriteBatch.Draw(_selectorArrow, new Vector2(35, 143), Color.White);
			}
		}

		private void DrawPostGame(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_frame, new Vector2(25, 75), Color.White);

			spriteBatch.DrawString(_pixelSpriteFont, "NEW GAME", new Vector2(85, 100), Color.Black);
			spriteBatch.DrawString(_pixelSpriteFont, "EXIT", new Vector2(85, 150), Color.Black);
			spriteBatch.Draw(_selectorArrow, SelectorArrowPosition, Color.White);

			spriteBatch.Draw(_frame, new Vector2(425, 75), Color.White);

			spriteBatch.DrawString(_pixelSpriteFont, "PLAYER SCORE " + Board.Instance.Players[0].Score,
				new Vector2(445, 100), Color.Blue);

			spriteBatch.DrawString(_pixelSpriteFont, "ENEMY SCORE " + Board.Instance.Players[1].Score,
				new Vector2(445, 150), Color.Red);

			if (Board.Instance.Players[0].Score > Board.Instance.Players[1].Score)
			{
				spriteBatch.DrawString(_pixelSpriteFont, "PLAYER WINS!", new Vector2(445, 250), Color.Blue);
			}

			if (Board.Instance.Players[0].Score < Board.Instance.Players[1].Score)
			{
				spriteBatch.DrawString(_pixelSpriteFont, "ENEMY WINS!", new Vector2(445, 250), Color.Red);
			}

			if (Board.Instance.Players[0].Score == Board.Instance.Players[1].Score)
			{
				spriteBatch.DrawString(_pixelSpriteFont, "TIE GAME!", new Vector2(445, 250), Color.Black);
			}
		}
	}
}

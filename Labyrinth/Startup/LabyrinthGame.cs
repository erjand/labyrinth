using Labyrinth.GameObjects;
using Labyrinth.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth.Startup
{
	public class LabyrinthGame : Game
	{
		public static LabyrinthGame Instance;
		private Gui _gui;

		public LabyrinthGame()
		{
			Instance = this;
			Content.RootDirectory = "Content";

			var graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = GridHelper.DefaultGameWindowWidth;
			graphics.PreferredBackBufferHeight = GridHelper.DefaultGameWindowHeight;
			
			int currentDisplayWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			int currentDisplayHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

			Window.Position = new Point(currentDisplayWidth / 2 - GridHelper.DefaultGameWindowWidth / 2,
				currentDisplayHeight / 2 - GridHelper.DefaultGameWindowHeight / 2);

			IsMouseVisible = true;

			Window.AllowUserResizing = true;
			Window.IsBorderless = false;
			Window.Title = "Labyrinth";
		}

		protected override void Initialize()
		{
			GameServices.AddService(new SpriteBatch(GraphicsDevice));
			GameServices.AddService(Content);

			GameStateManager.ChangeGameState(GameState.PreGame);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_gui = new Gui();

			Board.Instance.LoadContent();
			_gui.LoadContent();
		}

		protected override void Update(GameTime gameTime)
		{
			_gui.Update();
			Board.Instance.Update();
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.DarkGray);

			var spriteBatch = GameServices.GetService<SpriteBatch>();

			spriteBatch.Begin();

			_gui.Draw();
			Board.Instance.Draw();

			spriteBatch.End();
		}
	}
}

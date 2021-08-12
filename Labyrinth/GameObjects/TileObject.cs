using Labyrinth.GameObjects.Dictionaries;
using Labyrinth.GameObjects.Enums;
using Labyrinth.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth.GameObjects
{
	public class TileObject
	{
		public TileObjectType TileObjectType { get; }
		private Texture2D Texture { get; set; }

		public TileObject(TileObjectType tileObjectType)
		{
			TileObjectType = tileObjectType;
		}

		public void LoadContent()
		{
			Texture = GameServices.GetService<ContentManager>().Load<Texture2D>(TileObjectDictionary.Dictionary[TileObjectType].TextureName);
		}

		public void Draw(Vector2 parentTilePixelPosition)
		{
			GameServices.GetService<SpriteBatch>().Draw(Texture, parentTilePixelPosition, Color.White);
		}
	}
}

using Microsoft.Xna.Framework.Input;

namespace Labyrinth.Utils
{
	public static class InputHelper
	{
		public static bool LockInput;

		/// <summary>
		/// Will only register a single key-stroke until the key is pressed again.
		/// </summary>
		public static bool IsKeyPressedSingle(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, Keys key)
		{
			return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key) && !LockInput;
		}
	}
}

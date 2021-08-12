using Microsoft.Xna.Framework;

namespace Labyrinth.Utils
{
	/// <summary>
	/// A container for GameServices so we can grab them from anywhere without having to pass around.
	/// </summary>
	public static class GameServices
	{
		private static readonly GameServiceContainer GameServiceContainer = new GameServiceContainer();

		public static T GetService<T>()
		{
			return (T)GameServiceContainer.GetService(typeof(T));
		}

		public static void AddService<T>(T service)
		{
			GameServiceContainer.AddService(typeof(T), service);
		}
	}
}

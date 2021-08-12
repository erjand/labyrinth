namespace Labyrinth.Utils
{
	public static class GameStateManager
	{
		private static GameState _currentGameState;

		public static void ChangeGameState(GameState newGameState)
		{
			_currentGameState = newGameState;
		}

		public static GameState CurrentGameState()
		{
			return _currentGameState;
		}
	}

	public enum GameState
	{
		PreGame,
		PlayerTurn,
		EnemyTurn,
		PostGame
	}
}

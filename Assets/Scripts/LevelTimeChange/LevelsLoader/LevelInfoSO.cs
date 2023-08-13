using UnityEngine;

namespace LevelTimeChange.LevelsLoader {
	/// <summary>
	/// Stores information about one 'platform' in the game.
	/// </summary>
	[CreateAssetMenu()]
	public class LevelInfoSO : ScriptableObject
	{
		[Tooltip("Name of the scene object associated to this level.")]
		public string sceneName;
		
		[Tooltip("All levels to which player can get from this level.")]
		public LevelInfoSO[] neighbourLevels;
	}
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTimeChange.LevelsLoader {
	[CreateAssetMenu()]
	public class LevelInfoSO : ScriptableObject
	{
		public AvailableLevels level;
		public AvailableLevels[] neighbourLevels;
	}
}



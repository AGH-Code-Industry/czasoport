using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelInfoSO : ScriptableObject
{
	public LevelsList level;
	public LevelsList[] neighbourLevels;
}

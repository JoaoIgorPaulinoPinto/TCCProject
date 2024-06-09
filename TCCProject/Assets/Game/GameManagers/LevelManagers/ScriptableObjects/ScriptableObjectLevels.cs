using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelStats", menuName = "NewLevel")]
public class ScriptableObjectLevels : ScriptableObject
{
    public string levelName;
    public float duration;
    public int difficultyLevel;
}

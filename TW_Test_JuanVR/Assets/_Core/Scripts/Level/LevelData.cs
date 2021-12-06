using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Levels", order = 1)]
public class LevelData : ScriptableObject
{
    public GameObject background;
    public float enemyMoveTime;
    
}

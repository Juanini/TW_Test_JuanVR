using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemies", order = 1)]
public class EnemyData : ScriptableObject
{
    [PreviewField]
    public Sprite enemySprite;

    public int enemyLifes;
    public int enemyPoints;

    public EnemyTypeEnum enemyType;

    public enum EnemyTypeEnum
    {
        enemyGreen = GameConstants.ENEMY_TYPE_GREEN,
        enemyBlue = GameConstants.ENEMY_TYPE_BLUE,
        enemyRed = GameConstants.ENEMY_TYPE_RED
    }
}

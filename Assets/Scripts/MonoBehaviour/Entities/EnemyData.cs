using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="new EnemyData", menuName ="Entities/Enemy")]
public class EnemyData : ScriptableObject
{
    public Sprite enemySprite;
    public AnimatorController animationController;
    public string enemyName;
    
    [Header("Stats")]
    public StatBlockInstance statBlockInstance;
}
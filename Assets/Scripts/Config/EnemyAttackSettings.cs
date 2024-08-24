using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Attack", menuName = "Config/Enemy/Enemy Attack")]
public class EnemyAttackSettings : ScriptableObject
{
    public float Range = 10f;
    public float AttackRate = 2f;
    public float AttackDamage = 1f;
}
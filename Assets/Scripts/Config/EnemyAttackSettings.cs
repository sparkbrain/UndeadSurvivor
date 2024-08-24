using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Attack", menuName = "Config/Enemy/Enemy Attack")]
public class EnemyAttackSettings : ScriptableObject
{
    public float range = 10f;
    public float attackRate = 2f;
    public float attackDamage = 1f;
}
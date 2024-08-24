using UnityEngine;

[CreateAssetMenu(fileName = "Player shooting", menuName = "Config/Player/Player Shooting")]
public class PlayerShootingSettings : ScriptableObject
{
    public float Range = 4f;
    public float FireRate = 2f;
    public int StartingAmmo = 30;
    public float Damage = 1;
    public float BulletTravelSpeed = 5;
}

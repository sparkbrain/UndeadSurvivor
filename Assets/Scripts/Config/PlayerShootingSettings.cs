using UnityEngine;

[CreateAssetMenu(fileName = "Player shooting", menuName = "Config/Player/Player Shooting")]
public class PlayerShootingSettings : ScriptableObject
{
    public float range = 4f;
    public float fireRate = 2f;
    public int startingAmmo = 30;
    public float damage = 1;
    public float bulletTravelSpeed = 15;
}

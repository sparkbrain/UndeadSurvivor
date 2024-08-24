using UnityEngine;

[CreateAssetMenu(fileName = "Pickup Settings", menuName = "Config/Loot/Pickup")]
public class PickupSettings : ScriptableObject
{
    public PickupType pickupType;
    public int value;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Config/Upgrade/Upgrade")]
public class UpgradeSettings : ScriptableObject
{
    public UpgradeType type;
    public float amount;
}

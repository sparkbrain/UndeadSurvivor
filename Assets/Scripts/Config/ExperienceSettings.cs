using UnityEngine;

[CreateAssetMenu(fileName = "Experience Settings", menuName = "Config/Experience Settings")]
public class ExperienceSettings : ScriptableObject
{
    public float experienceNeededStart;
    public float experienceNeededMultiplier;
}
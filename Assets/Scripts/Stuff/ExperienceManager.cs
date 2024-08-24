using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ExperienceManager : MonoBehaviour
{
    [Inject] private readonly AudioManager _audioManager;

    [SerializeField] UpgradeManager _upgradeManager;
    [SerializeField] Slider _experienceBar;
    [SerializeField] Text _levelText;
    [SerializeField] ExperienceSettings _experienceSettings;

    float _currentExperience;
    float _experienceForLevelUp;
    int _currentLevel;

    private void Awake()
    {
        _upgradeManager = GetComponent<UpgradeManager>();
        _experienceForLevelUp = _experienceSettings.experienceNeededStart;
        UpdateExperienceIndicator();
    }

    public void GiveExperience(int value)
    {
        _currentExperience += value;

        if(_currentExperience >= _experienceForLevelUp) 
        {
            LevelUp();
        }

        UpdateExperienceIndicator();
    }

    private void UpdateExperienceIndicator()
    {
        _experienceBar.value = _currentExperience / _experienceForLevelUp;
    }

    private void LevelUp()
    {
        _currentExperience -= _experienceForLevelUp;
        _experienceForLevelUp *= _experienceSettings.experienceNeededMultiplier;
        _currentLevel++;
        _levelText.text = $"Lv.{_currentLevel}";

        _upgradeManager.GiveUpgrade();
        _audioManager.PlayLevelUpSound();
    }
}

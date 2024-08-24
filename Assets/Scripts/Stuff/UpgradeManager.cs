using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public enum UpgradeType
{
    MOVEMENT_SPEED,
    FIRE_RANGE,
    FIRE_RATE,
    POISON_BULLETS
}


public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeTable _upgradeTable;
    [SerializeField] private Text _upgradeText;
    [SerializeField] private float _showUpgradeTextForSeconds;
    [SerializeField] private float _thenFadeTextInSeconds;

    Movement _movement;
    PlayerFire _playerFire;

    private void Awake()
    {
        _upgradeText.enabled = false;
        _movement = GetComponent<Movement>();
        _playerFire = GetComponent<PlayerFire>();
    }

    public void GiveUpgrade()
    {
        if (!_upgradeTable.GetDrop(out UpgradeSettings upgrade))
        {
            return;
        }

        switch (upgrade.type)
        {
            case UpgradeType.MOVEMENT_SPEED:
                _movement.IncreaseMovementSpeed(upgrade.amount);
                StartCoroutine(SetUpgradeText($"MOVEMENT SPEED INCREASED BY {upgrade.amount}"));
                break;
            case UpgradeType.FIRE_RANGE:
                _playerFire.IncreaseFireRange(upgrade.amount);
                StartCoroutine(SetUpgradeText($"FIRE RANGE INCREASED BY {upgrade.amount}"));
                break;
            case UpgradeType.FIRE_RATE:
                _playerFire.IncreaseFireRate(upgrade.amount);
                StartCoroutine(SetUpgradeText($"ATTACK SPEED INCREASED BY {upgrade.amount}"));
                break;
            case UpgradeType.POISON_BULLETS:
                _playerFire.IncreasePoisonDamage(upgrade.amount);
                StartCoroutine(SetUpgradeText($"POISON DAMAGE INCREASED BY {upgrade.amount}"));
                break;
        }
    }

    private IEnumerator SetUpgradeText(string text)
    {
        _upgradeText.enabled = true;
        _upgradeText.text = text;
        _upgradeText.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(_showUpgradeTextForSeconds);

        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime / (_thenFadeTextInSeconds + Mathf.Epsilon);
            _upgradeText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            yield return null;
        }

        _upgradeText.enabled = false;
    }
}
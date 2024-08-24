using System;
using UnityEngine;

[Serializable]
public struct AudioContainer
{
    [SerializeField] private AudioClip[] _clips;

    public readonly AudioClip GetClip()
    {
        int clipIndex = UnityEngine.Random.Range(0, _clips.Length);
        return _clips[clipIndex];
    }
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioContainer levelUpSound;
    [SerializeField] AudioContainer enemyDeathSound;
    [SerializeField] AudioContainer loseSound;
    [SerializeField] AudioContainer hitSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayLevelUpSound()
    {
        AudioClip clip = levelUpSound.GetClip();
        PlayAudioClip(clip);
    }

    public void PlayDeathSound()
    {
        AudioClip clip = enemyDeathSound.GetClip();
        PlayAudioClip(clip);
    }

    public void PlayLoseSound()
    {
        AudioClip clip = loseSound.GetClip();
        PlayAudioClip(clip);
    }

    public void PlayBulletHitSound()
    {
        AudioClip clip = hitSound.GetClip();
        PlayAudioClip(clip);
    }

    private void PlayAudioClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}

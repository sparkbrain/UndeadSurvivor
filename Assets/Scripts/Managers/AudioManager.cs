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
    [SerializeField] AudioContainer _levelUpSound;
    [SerializeField] AudioContainer _deathSound;
    [SerializeField] AudioContainer _loseSound;
    [SerializeField] AudioContainer _hitSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayLevelUpSound()
    {
        AudioClip clip = _levelUpSound.GetClip();
        PlayAudioClip(clip);
    }

    public void PlayDeathSound()
    {
        AudioClip clip = _deathSound.GetClip();
        PlayAudioClip(clip);
    }

    public void PlayLoseSound()
    {
        AudioClip clip = _loseSound.GetClip();
        PlayAudioClip(clip);
    }

    public void PlayBulletHitSound()
    {
        AudioClip clip = _hitSound.GetClip();
        PlayAudioClip(clip);
    }

    private void PlayAudioClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _targetVolume = 1f;

    [SerializeField]
    private float _fadeDurationSec = 1f;

    private Coroutine _fadeCoroutine;

    private readonly string _musicFolderPath = "Sounds";
    private readonly Dictionary<string, AudioClip> _musicDictionary = new();

    public void Start()
    {
        LoadMusic();
    }

    public void LoadMusic()
    {
        _musicDictionary.Clear();
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>(_musicFolderPath);

        foreach (AudioClip clip in audioClips)
        {
            if (clip != null && !_musicDictionary.ContainsKey(clip.name))
            {
                _musicDictionary.Add(clip.name, clip);
            }
        }

        Debug.Log($"Loaded {_musicDictionary.Count} music tracks from {_musicFolderPath}");
    }

    public AudioClip GetMusicTrack(string trackName)
    {
        if (_musicDictionary.TryGetValue(trackName, out AudioClip track))
        {
            return track;
        }
        Debug.LogWarning($"Music track with name '{trackName}' not found.");
        return null;
    }

    public void PlayMusic(string trackName)
    {
        AudioClip clip = GetMusicTrack(trackName);
        if (clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    public void StopMusic(string trackName)
    {
        _audioSource.Stop();
    }

    public void FadeVolume(float newTargetVolume, float duration, Action onComplete = null)
    {
        _targetVolume = Mathf.Clamp(newTargetVolume, 0f, 1f);
        _fadeDurationSec = Mathf.Max(duration, 0f);

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }
        _fadeCoroutine = StartCoroutine(FadeVolumeCoroutine(onComplete));
    }

    private IEnumerator FadeVolumeCoroutine(Action onComplete)
    {
        float startVolume = _audioSource.volume;
        float timeElapsed = 0f;

        while (timeElapsed < _fadeDurationSec)
        {
            timeElapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, _targetVolume, timeElapsed / _fadeDurationSec);
            yield return null;
        }

        _audioSource.volume = _targetVolume;
        onComplete?.Invoke();
    }
}

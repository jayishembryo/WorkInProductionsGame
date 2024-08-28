using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.pitch = 1f;
            s.Source.volume = s.Volume;
            s.Source.loop = s.Loop;
            s.Source.outputAudioMixerGroup = s.AudioMixer;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.Source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.Name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.Source.Stop();
    }

    public void Pause(string sound)
    {
        Sound s = Array.Find(sounds, item => item.Name == sound);
        s.Source.Pause();
    }

    public void UnPause(string sound)
    {
        Sound s = Array.Find(sounds, item => item.Name == sound);
        s.Source.UnPause();
    }

    public void FadeOut(string sound, float FadeTime)
    {
        Sound s = Array.Find(sounds, item => item.Name == sound);
        float startVolume = s.Source.volume;

        while (s.Source.volume > 0)
        {
            s.Source.volume -= startVolume * Time.deltaTime / FadeTime;
        }

        if (s.Source.volume <= 0)
        {
            s.Source.Stop();
            s.Source.volume = startVolume;
        }
    }
}

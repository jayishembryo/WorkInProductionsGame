using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioMixerGroup audioMixer;
    [Range(0f, 1f)] [SerializeField] private float volume;
    [SerializeField] private bool loop;

    private AudioSource source;

    public string Name { get => name; set => name = value; }
    public AudioClip Clip { get => clip; set => clip = value; }
    public AudioMixerGroup AudioMixer { get => audioMixer; set => audioMixer = value; }
    public float Volume { get => volume; set => volume = value; }
    public bool Loop { get => loop; set => loop = value; }
    public AudioSource Source { get => source; set => source = value; }
}
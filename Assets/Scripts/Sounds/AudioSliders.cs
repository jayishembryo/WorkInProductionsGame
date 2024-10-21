using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioSliders : MonoBehaviour
{
    private Bus Music;
    private Bus SFX;
    private Bus Ambience;
    private Bus Master;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float AmbienceVolume = 0.5f;
    float MasterVolume = 1f;


    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        Ambience = FMODUnity.RuntimeManager.GetBus("bus:/Ambience");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
    }

    void Update()
    {
        Debug.Log("AudioSliders: Update running.");
        Ambience.setVolume(AmbienceVolume);
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }
    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }
    public void AmbienceVolumeLevel(float newAmbienceVolume)
    {
        AmbienceVolume = newAmbienceVolume;
    }
    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }
}

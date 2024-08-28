using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sections;
    [SerializeField] private double bpm = 182f;
    [SerializeField] private int measureCounter = 1;
    [SerializeField] private AudioMixerGroup group;
    [Tooltip("allows you to toggle drums and the end")][SerializeField] private bool drums, dead, end;

    private AudioSource[] sources;
    private AudioSource[] loopedSources;
    private double measureGap, timer;
    private bool started;

    private double nextEventTime;
    private bool flip;

    public delegate void AudioStartAction(double syncTime);
    public static event AudioStartAction OnAudioStart;

    private void Awake()
    {
        sources = new AudioSource[sections.Length];
        loopedSources = new AudioSource[4];
        LoadParts();
    }

    private void Start()
    {
        measureGap = 4f / (bpm / 60f);

        nextEventTime = AudioSettings.dspTime + 1.0f;

        StartCoroutine(LateStart());
    }

    private void Update()
    {
        //song hasn't started
        if (!started)
            return;

        drums = GameObject.FindGameObjectsWithTag("Enemy").Length > 1;
        dead = GameObject.FindGameObjectsWithTag("Player").Length == 0;        

        //if the end is triggered
        if (end && !sources[4].isPlaying)
        {
            for (int i = 0; i < 4; i++)
                sources[i].Stop();

            for (int j = 5; j < 9; j++)
                sources[j].Stop();

            foreach (AudioSource audioSource in loopedSources)
                audioSource.Stop();

            sources[4].Play();
            measureCounter = 1;
            started = false;
            return;
        }

        //counts the measures of the song
        timer += Time.deltaTime;
        if (timer > measureGap)
        {
            timer -= measureGap;
            measureCounter++;
        }

        double time = AudioSettings.dspTime;

        if (time + 1.0f > nextEventTime)
        {
            if (flip)
                PlaySection(new int[] { 2, 3, 7, 8 }, sources);
            else
                PlaySection(new int[] { 0, 1, 2, 3 }, loopedSources);

            nextEventTime += 60.0f / bpm * 192f;
            flip = !flip;
        }

        //if dead is turned on...
        if (dead)
        {
            //mute all non-dead tracks
            for (int i = 0; i < 4; i++)
                sources[i].mute = true;

            loopedSources[0].mute = loopedSources[1].mute = true;

            //unmute the dead tracks
            sources[6].mute = false;
            if (flip)
                loopedSources[3].mute = false;
            else
                sources[8].mute = false;

            //if drums are enabled, unmute dead drums
            sources[5].mute = !drums;
            if (flip)
                loopedSources[2].mute = !drums;
            else
                sources[7].mute = !drums;
        }
        //if dead is turned off..
        else
        {
            //mute all dead tracks
            for (int i = 5; i < 9; i++)
                sources[i].mute = true;

            loopedSources[2].mute = loopedSources[3].mute = true;

            //unmute the normal tracks
            loopedSources[1].mute = sources[1].mute = sources[3].mute = false;

            //if drums are enabled, unmute drums
            loopedSources[0].mute = sources[0].mute = sources[2].mute = !drums; 
        }
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        PlaySection(new int[] { 0, 1, 5, 6 }, sources);
        nextEventTime += 60.0f / bpm * 34f;
        started = true;
    }

    private void LoadParts()
    {
        for (int curSect = 0; curSect < sections.Length; curSect++)
        {
            sources[curSect] = gameObject.AddComponent<AudioSource>();
            sources[curSect].clip = sections[curSect];
            sources[curSect].outputAudioMixerGroup = group;
            sources[curSect].clip.LoadAudioData();
        }

        for (int i = 0; i < loopedSources.Length; i++)
        {
            loopedSources[i] = gameObject.AddComponent<AudioSource>();

            switch (i)
            {
                case 0:
                    loopedSources[i].clip = sections[2];
                    break;
                case 1:
                    loopedSources[i].clip = sections[3];
                    break;
                case 2:
                    loopedSources[i].clip = sections[7];
                    break;
                case 3:
                    loopedSources[i].clip = sections[8];
                    break;
            }
            loopedSources[i].outputAudioMixerGroup = group;
            loopedSources[i].clip.LoadAudioData();
            loopedSources[i].mute = true;
        }
        
    }

    private void PlaySection(int[] sect, AudioSource[] audioSources)
    {
        foreach (int i in sect)
            audioSources[i].PlayScheduled(nextEventTime);

        OnAudioStart?.Invoke(nextEventTime);
    }

    private void PlaySection(int sect, AudioSource[] audioSources)
    {
        PlaySection(new int[] { sect }, audioSources);
    }
}

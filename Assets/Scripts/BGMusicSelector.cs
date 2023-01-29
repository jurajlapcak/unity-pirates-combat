using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BGMusicSelector : MonoBehaviour
{
    public AudioSource[] audioSources;

    private int trackSelector;
    private int trackHistory;

    void Start()
    {
        PlayRandomTrack();
    }

    void Update()
    {
        bool isTrackPlaying = false;

        foreach (var audioSource in audioSources)
        {
            isTrackPlaying = audioSource.isPlaying || isTrackPlaying;
        }

        if (!isTrackPlaying)
        {
            PlayRandomTrack();
        }
    }

    private void PlayRandomTrack()
    {
        trackSelector = Random.Range(0, audioSources.Length);
        while (trackSelector == trackHistory)
        {
            trackSelector = Random.Range(0, audioSources.Length);
        }

        audioSources[trackSelector].Play();
        trackHistory = trackSelector;
    }
}
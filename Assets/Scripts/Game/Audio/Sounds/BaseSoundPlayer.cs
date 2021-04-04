using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class BaseSoundPlayer : MonoBehaviour {
    public AudioClip[] audioClips;
    AudioSource source;
    int currentlyPlayingCount = 0;
    void Awake() {
        if (source == null)
            source = GetComponent<AudioSource>();
    }
    public void PlayOnce() {
        currentlyPlayingCount++;
        if (!source.isPlaying) {
            currentlyPlayingCount = 0;
        }
        if (audioClips.Length > 0) {
            if (currentlyPlayingCount < 3) {
                AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
                source.PlayOneShot(clip);
            }
        }
    }
}
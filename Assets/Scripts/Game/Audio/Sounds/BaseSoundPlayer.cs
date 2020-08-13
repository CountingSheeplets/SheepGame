using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoundPlayer : MonoBehaviour {
    public AudioClip[] audioClips;
    AudioSource source;
    void Awake() {
        if (source == null)
            source = GetComponent<AudioSource>();
    }
    public void PlayOnce() {
        if (audioClips.Length > 0) {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
            source.PlayOneShot(clip);
            //Debug.Log("Play: " + clip.name);
        }
    }
}
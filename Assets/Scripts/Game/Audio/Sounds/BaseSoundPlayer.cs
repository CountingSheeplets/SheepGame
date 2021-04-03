using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
            /*             float val0 = 0;
                        float val1 = 0;
                        float val2 = 0;
                        SoundTrackCoordinator.GetMixer().GetFloat("MyExposedParam", out val0);
                        SoundTrackCoordinator.GetMixer().GetFloat("MyExposedParam 1", out val1);
                        SoundTrackCoordinator.GetMixer().GetFloat("MyExposedParam 2", out val2);
                        Debug.Log("Play: " + clip.name + " s level: " + val0 + " a lv: " + val1 + " master:" + val2);
                     */
        }
    }
}
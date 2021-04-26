using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTrackCoordinator : Singleton<SoundTrackCoordinator> {

    public AudioClip menuSoundTrack;
    public AudioClip scoreSoundTrack;
    public List<AudioClip> matchSoundTracks;
    List<AudioClip> randomizedList;
    int matchTrackIndex;
    int flip;
    float loadDelay = 2f;
    public float targetPitchDown = 0.7f;
    public float targetVolumeDown = 0.05f;
    float normalVolume = 0.4f;
    public AudioSource[] players;
    int playCount = 1;

    void Awake() {
        normalVolume = Instance.players[0].volume;
        loadDelay = ConstantsBucket.PlayfieldFadeTime;
        randomizedList = matchSoundTracks.Shuffle();
    }
    public static void PlayMenu() {
        ScheduleAndPlay(Instance.menuSoundTrack, 0.5f);
    }
    public static void PlayScores() {
        Instance.ScheduledPlay(Instance.scoreSoundTrack, 0.2f);
    }
    public static void PlayNext() {
        ScheduleAndPlay(Instance.randomizedList[Instance.matchTrackIndex]);
        Instance.matchTrackIndex++;
        if (Instance.matchTrackIndex >= Instance.randomizedList.Count) {
            Instance.matchTrackIndex = 0;
        }
    }

    static void ScheduleAndPlay(AudioClip clip) {
        ScheduleAndPlay(clip, Instance.loadDelay);
    }
    static void ScheduleAndPlay(AudioClip clip, float delayTime) {
        Instance.playCount++;
        Instance.flip = 1 - Instance.flip;
        Instance.ScheduledPlay(clip, delayTime);
        Instance.StartCoroutine(Instance.LoopPlayer(clip, delayTime));
        //Debug.Log("ScheduleAndPlay:" + clip + "  time:" + AudioSettings.dspTime + delayTime + "  flip:" + (1 - Instance.flip));
        Instance.players[1 - Instance.flip].SetScheduledEndTime(AudioSettings.dspTime + delayTime);
        Instance.StartToneDown(Instance.players[1 - Instance.flip], delayTime);
    }
    public void ScheduledPlay(AudioClip clip, float delayTime) {
        players[Instance.flip].clip = clip;
        players[Instance.flip].PlayScheduled(AudioSettings.dspTime + delayTime);
        players[Instance.flip].pitch = 1f;
        players[Instance.flip].volume = normalVolume;
    }
    IEnumerator LoopPlayer(AudioClip clip, float delayTime) {
        int initialCount = playCount;
        float length = (clip.samples * 1f) / clip.frequency;
        yield return new WaitForSecondsRealtime(length + delayTime - 0.08f);
        if (initialCount == playCount) {
            ScheduledPlay(clip, 0);
            StartCoroutine(LoopPlayer(clip, 0));
        }
    }
    public void StartToneDown(AudioSource source, float timeToDie) {
        StartCoroutine(ToneDown(source, timeToDie));
    }
    IEnumerator ToneDown(AudioSource source, float timeToDie) {
        if (source.isPlaying) {
            float currenVolume = source.volume;
            float currenPitch = source.pitch;
            float timeCount = 0;
            while (timeToDie > timeCount) {
                timeCount += Time.deltaTime;
                float timePart = timeCount / timeToDie;
                source.pitch = Mathf.Lerp(currenPitch, targetPitchDown, timePart);
                source.volume = Mathf.Lerp(currenVolume, targetVolumeDown, timePart);
                yield return null;
            }
        }
        yield return null;
    }
}
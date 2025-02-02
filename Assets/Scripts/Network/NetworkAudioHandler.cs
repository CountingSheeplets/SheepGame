using System.Collections;
using System.Collections.Generic;
//using NDream.AirConsole;
//using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Audio;
/*public class NetworkAudioHandler : MonoBehaviour {
    public AudioMixer mixer;
    private AudioMixerSnapshot mutedSnapshot;
    private AudioMixerSnapshot fullSnapshot;
    private AudioMixerSnapshot fxOnlySnapshot;
    int musicState = 0;
    void Awake() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage += OnAudioBtn;
            AirConsole.instance.onAdShow += OnAdShow;
            AirConsole.instance.onAdComplete += OnAdComplete;
        }
    }
    void OnAdShow() {
        mutedSnapshot.TransitionTo(.5f);
    }
    void OnAdComplete(bool wasShown) {
        SetAudio();
    }
    void Start() {
        fullSnapshot = mixer.FindSnapshot("FullSnapshot");
        fxOnlySnapshot = mixer.FindSnapshot("FxOnlySnapshot");
        mutedSnapshot = mixer.FindSnapshot("MutedSnapshot");
    }

    public void SetAudio() {
        switch (musicState) {
            case 0:
                fullSnapshot.TransitionTo(.5f);
                break;
            case 1:
                fxOnlySnapshot.TransitionTo(.5f);
                break;
            case 2:
                mutedSnapshot.TransitionTo(.5f);
                break;
        }
    }

    void OnAudioBtn(int from, JToken message) {
        if (GameStateView.HasState(GameState.started))
            return;
        if (message["element"] != null)
            if (message["element"].ToString() == "audio-button") {
                Owner eventOwner = OwnersCoordinator.GetOwner(from);
                if (eventOwner == null)
                    return;
                else {
                    if (eventOwner.IsFirstOwner) {
                        musicState++;
                        if (musicState > 2)
                            musicState = 0;
                        NetworkCoordinator.SendAudioState(eventOwner.teamId, musicState);
                        SetAudio();
                    } else {
                        NetworkCoordinator.SendFirstOwner(eventOwner.teamId, false);
                    }
                }
            }
    }

    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnAudioBtn;
            AirConsole.instance.onAdShow -= OnAdShow;
            AirConsole.instance.onAdComplete -= OnAdComplete;
        }
    }
}*/
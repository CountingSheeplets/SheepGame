using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class LoadingScreenHandler : MonoBehaviour {
    bool trigger;
    public TextMeshProUGUI textMesh;
    public Animator animator;
    void Awake() {
        gameObject.SetActive(false);
        EventCoordinator.StartListening(EventName.Input.PlayersReady(), OnStartGame);
    }

    void OnStartGame(GameMessage msg) {
        gameObject.SetActive(true);
        animator.SetTrigger("fadeOut");
        StartCoroutine(StartGameNextFrame());
        textMesh.text = "Building sheepdoms...";
    }
    IEnumerator StartGameNextFrame() {
        yield return new WaitForFixedUpdate();
        EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        yield return null;
        gameObject.SetActive(false);
        yield return null;
    }
}
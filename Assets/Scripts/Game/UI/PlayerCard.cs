﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerCard : MonoBehaviour
{
    public bool gameStarted = false;
    public GameObject disconnectedImage;
    
    public TextMeshProUGUI crownCount;
    public TextMeshProUGUI moneyCount;
    public TextMeshProUGUI kingHealthText;
    public RectTransform kingHealthPanel;

    public TextMeshProUGUI playerName;
    public Image playerAvatarImage;

    public Owner owner;
    public Transform targetCardGhost;
    public float timeToAnimate = 0.5f;
    RectTransform myRectTr;
    RectTransform ghostRectTr;
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StartListening(EventName.System.Player.PlayerCardsSorted(), OnSorted);
        //init by copying ghost:
        myRectTr = GetComponent<RectTransform>();
        ghostRectTr = targetCardGhost.GetComponent<RectTransform>();
        ApplyTransforms();
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StopListening(EventName.System.Player.PlayerCardsSorted(), OnSorted);
    }

    void OnProfileUpdate(GameMessage msg){
        if(owner.EqualsByValue(msg.playerProfile.owner)){
            crownCount.text = Mathf.FloorToInt(msg.playerProfile.GetStarCount()).ToString();
            moneyCount.text = Mathf.FloorToInt(msg.playerProfile.GetMoney()).ToString();
            kingHealthText.text = Mathf.FloorToInt(msg.playerProfile.GetHealth()).ToString();

            Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(msg.playerProfile.GetHealth()/100f, 1);
            kingHealthPanel.anchorMin = min;
            kingHealthPanel.anchorMax = max;
        }
    }

    void OnPlayerJoined(GameMessage msg){
        if(gameStarted && owner.EqualsByValue(msg.owner)){
            disconnectedImage.SetActive(false);
        }
    }
    void OnPlayerLeft(GameMessage msg){
        if(gameStarted && owner.EqualsByValue(msg.owner)){
            disconnectedImage.SetActive(true);
        }
    }
    void OnStartGame(GameMessage msg){
        gameStarted = true;
        playerAvatarImage.sprite = owner.GetPlayerProfile().playerAvatarImage;
        playerName.text = owner.ownerName;
        ApplyTransforms();
    }
    void OnSorted(GameMessage msg){
        StartCoroutine(AnimateCardToPosition(targetCardGhost.GetComponent<RectTransform>()));
    }
    void ApplyTransforms(){
        myRectTr.anchorMin = ghostRectTr.anchorMin;
        myRectTr.anchorMax = ghostRectTr.anchorMax;
        myRectTr.anchoredPosition = ghostRectTr.anchoredPosition;
        myRectTr.sizeDelta = ghostRectTr.sizeDelta;
    }
    IEnumerator AnimateCardToPosition(RectTransform targetTransform){
        float startTime = Time.time;
        Vector2 startPos = myRectTr.anchoredPosition;
        while(Time.time < startTime + timeToAnimate){
            float progress = (Time.time - startTime)/timeToAnimate;
            myRectTr.anchoredPosition = Vector2.Lerp(startPos, targetTransform.anchoredPosition, progress);
		    yield return null;
        }
        yield return null;
    }
}

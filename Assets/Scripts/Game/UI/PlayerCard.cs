﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerCard : MonoBehaviour
{
    public bool isEliminated = false;
    public GameObject disconnectedImage;
    public GameObject eliminatedImage;
    
    public TextMeshProUGUI crownCount;
    public TextMeshProUGUI moneyCount;
    public TextMeshProUGUI kingHealthText;
    //public RectTransform kingHealthPanel;
    public Image kingHealthFill;

    public TextMeshProUGUI playerName;
    public Image playerAvatarImage;
    public Image playerColorBackground;

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
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnEliminated);
        EventCoordinator.StartListening(EventName.System.King.ReceivedDamage(), OnDamageReceived);
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
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnEliminated);
        EventCoordinator.StopListening(EventName.System.King.ReceivedDamage(), OnDamageReceived);
        if(targetCardGhost.gameObject != null)
            Destroy(targetCardGhost.gameObject);
    }

    void OnProfileUpdate(GameMessage msg){
        if(isEliminated)
            return;
        if(owner.EqualsByValue(msg.playerProfile.owner)){
            crownCount.text = Mathf.FloorToInt(msg.playerProfile.GetCrowns()).ToString();
            moneyCount.text = Mathf.FloorToInt(msg.playerProfile.GetMoney()).ToString();
/*             Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(msg.playerProfile.GetHealth()/100f, 1);
            kingHealthPanel.anchorMin = min;
            kingHealthPanel.anchorMax = max; */
            kingHealthFill.fillAmount = msg.playerProfile.GetHealth()/100f;
        }
    }
    void OnDamageReceived(GameMessage msg){
        if(isEliminated)
            return;
        if(msg.kingUnit == owner.GetKing()){
            kingHealthText.text = Mathf.FloorToInt(msg.floatMessage).ToString();
            kingHealthText.GetComponent<Animator>().SetTrigger("Show");
        }
    }
    void OnPlayerJoined(GameMessage msg){
        if((GameStateView.GetGameState() & GameState.started) != 0)
            if(owner.EqualsByValue(msg.owner)){
                disconnectedImage.SetActive(false);
            }
    }
    void OnPlayerLeft(GameMessage msg){
        if((GameStateView.GetGameState() & GameState.started) != 0)
            if(owner.EqualsByValue(msg.owner)){
                disconnectedImage.SetActive(true);
            }
    }
    void OnStartGame(GameMessage msg){
        playerAvatarImage.sprite = owner.GetPlayerProfile().playerAvatarImage;
        playerColorBackground.color = owner.GetPlayerProfile().playerColor;
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

    void OnEliminated(GameMessage msg){
        if(owner.EqualsByValue( msg.targetOwner)){
            //transform.SetAsLastSibling();
            eliminatedImage.SetActive(true);
            isEliminated = true;
        }
    }
}

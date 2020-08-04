using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeController : MonoBehaviour {
    float counter = 0f;
    int minute = 0;
    float startTime;
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStart);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStart);
    }
    void OnStart(GameMessage msg) {
        startTime = Time.time;
    }
    void Update() {
        if (!GameStateView.HasState(GameState.started))
            return;
        counter += Time.deltaTime;
        if (counter > ConstantsBucket.GoldIncomePeriod) {
            counter = 0;
            minute = Mathf.FloorToInt((Time.time - startTime) / 60f);
            foreach (Owner owner in OwnersCoordinator.GetOwnersAlive()) {
                float reward = ConstantsBucket.BaseGoldIncome + minute * ConstantsBucket.GoldIncomeIncrement;
                GoldRewardCoordinator.RewardGold(owner, reward);
            }
        }
    }
}
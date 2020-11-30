using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                float baseReward = ConstantsBucket.BaseGoldIncome + minute * ConstantsBucket.GoldIncomeIncrement;
                List<SheepUnit> greedySheep = SheepCoordinator.GetSheepInField(owner.GetPlayfield()).Where(x => x.sheepType == SheepType.Greedy && !owner.EqualsByValue(x.owner)).ToList();
                float reward = baseReward * (1 - greedySheep.Count);

                GoldRewardCoordinator.RewardGold(owner.GetPlayerProfile(), reward, owner.GetKing().transform);
                //your greedy sheep rewards:
                foreach (SheepUnit sheep in greedySheep) {
                    if (!owner.EqualsByValue(sheep.owner))
                        GoldRewardCoordinator.RewardGold(sheep.owner.GetPlayerProfile(), baseReward + 2, sheep.transform);
                    else
                        GoldRewardCoordinator.RewardGold(sheep.owner.GetPlayerProfile(), 2, sheep.transform);
                }
            }
        }
    }
}
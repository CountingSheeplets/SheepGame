using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventName.System;

public class AbilitiesPanelTile : MonoBehaviour
{
    public Owner owner;

    public Animator growGrassActivation;
    public Animator upgradeKingActivation;
    public Animator smashActivation;
    public Animator upgradeAActivation;
    public Animator upgradeBActivation;

    public Image upgradeA;
    public Image upgradeB;

    public TextMeshProUGUI growGrassCost;
    public TextMeshProUGUI upgradeKingCost;
    public TextMeshProUGUI smashCost;
    public TextMeshProUGUI upgradeACost;
    public TextMeshProUGUI upgradeBCost;
    
    public void SetOwner(Owner _owner) {
        owner = _owner;
    }

    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.BoughtGrass(), OnBoughtGrass);
        EventCoordinator.StartListening(EventName.Input.KingAbilities.NotBoughtGrass(), OnNotBoughtGrass);

        EventCoordinator.StartListening(EventName.System.King.StartSmash(), OnStartSmash);
        EventCoordinator.StartListening(EventName.System.King.NotStartSmash(), OnNotStartSmash);

        EventCoordinator.StartListening(EventName.Input.KingAbilities.KingUpgraded(), OnKingUpgraded);
        EventCoordinator.StartListening(EventName.Input.KingAbilities.KingNotUpgraded(), OnKingNotUpgraded);

        EventCoordinator.StartListening(EventName.System.Sheep.Upgraded(), OnSheepUpgraded);
        EventCoordinator.StartListening(EventName.System.Sheep.NotUpgraded(), OnSheepNotUpgraded);

        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
    }
    public void OnStartGame(GameMessage msg) {
        if (msg.owner == owner) {
            growGrassCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.BuyGrass()).ToString();
            upgradeKingCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.Upgrade()).ToString();
            smashCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.Smash()).ToString();
        }
    }
    public void OnBoughtGrass(GameMessage msg) {
        if(msg.owner == owner) {
            growGrassActivation.SetTrigger("Rise");
            growGrassCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.BuyGrass()).ToString();
        }
    }
    public void OnNotBoughtGrass(GameMessage msg) {
        if (msg.owner == owner) {
            growGrassActivation.SetTrigger("Short");
            growGrassCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.BuyGrass()).ToString();
        }
    }
    public void OnKingUpgraded(GameMessage msg) {
        if (msg.owner == owner) {
            upgradeKingActivation.SetTrigger("Rise");
            upgradeKingCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.Upgrade()).ToString();
        }
    }
    public void OnKingNotUpgraded(GameMessage msg) {
        if (msg.owner == owner) {
            upgradeKingActivation.SetTrigger("Short");
            upgradeKingCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.Upgrade()).ToString();
        }
    }
    public void OnStartSmash(GameMessage msg) {
        if (msg.owner == owner) {
            smashActivation.SetTrigger("Rise");
            smashCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.Smash()).ToString();
        }
    }
    public void OnNotStartSmash(GameMessage msg) {
        if (msg.owner == owner) {
            smashActivation.SetTrigger("Short");
            smashCost.text = PriceCoordinator.GetPrice(msg.owner, PriceName.King.Smash()).ToString();
        }
    }

    public void OnSheepUpgraded(GameMessage msg) {
        if (msg.owner == owner) {
            if (msg.sheepUnit.owner.EqualsByValue(owner)) {
                if (msg.sheepUnit.sheepType == SheepType.Small ||
                    msg.sheepUnit.sheepType == SheepType.Greedy ||
                    msg.sheepUnit.sheepType == SheepType.Bouncy
                    ) {
                    upgradeAActivation.SetTrigger("Rise");
                }
                if (msg.sheepUnit.sheepType == SheepType.Armored ||
                    msg.sheepUnit.sheepType == SheepType.Tank ||
                    msg.sheepUnit.sheepType == SheepType.Trench
                    ) {
                    upgradeBActivation.SetTrigger("Rise");
                }
            }
            SetUpgradeButtons(msg.sheepUnit);
        }
    }

    public void OnSheepNotUpgraded (GameMessage msg) {
        if (msg.owner == owner) {
            if (msg.sheepUnit.owner.EqualsByValue(owner)) {
                if (msg.sheepUnit.sheepType == SheepType.Small ||
                    msg.sheepUnit.sheepType == SheepType.Greedy ||
                    msg.sheepUnit.sheepType == SheepType.Bouncy
                    ) {
                    upgradeAActivation.SetTrigger("Short");
                }
                if (msg.sheepUnit.sheepType == SheepType.Armored ||
                    msg.sheepUnit.sheepType == SheepType.Tank ||
                    msg.sheepUnit.sheepType == SheepType.Trench
                    ) {
                    upgradeBActivation.SetTrigger("Short");
                }
            }
            SetUpgradeButtons(msg.sheepUnit);
        }
    }
    void OnSheepReady(GameMessage msg) {
        if (msg.playfield.owner == owner) {
            SetUpgradeButtons(msg.sheepUnit);
        }
    }

    void SetUpgradeButtons(SheepUnit sheep) {
        UpgradeProperty upgradeAProperty = UpgradeBucket.GetNextUpgradeA(sheep);
        UpgradeProperty upgradeBProperty = UpgradeBucket.GetNextUpgradeB(sheep);
        SheepType iconA = SheepType.None;
        float priceA = 0;
        SheepType iconB = SheepType.None;
        float priceB = 0;
        if (upgradeAProperty != null) {
            iconA = upgradeAProperty.sheepTypeOutput;
            priceA = PriceCoordinator.GetPrice(sheep.owner, upgradeAProperty.upgradeCodeName);
        }
        if (upgradeBProperty != null) {
            iconB = upgradeBProperty.sheepTypeOutput;
            priceB = PriceCoordinator.GetPrice(sheep.owner, upgradeBProperty.upgradeCodeName);
        }
        upgradeA.sprite = UpgradeIconBucket.GetIcon(iconA);
        upgradeB.sprite = UpgradeIconBucket.GetIcon(iconB);
        upgradeACost.text = priceA.ToString();
        upgradeBCost.text = priceB.ToString();
    }

    void OnDestroy()
    {
        Debug.Log("Panel destroyed:   " + gameObject);
    }


}

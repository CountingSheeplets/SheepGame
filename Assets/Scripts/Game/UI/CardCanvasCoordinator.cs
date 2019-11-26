using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardCanvasCoordinator : Singleton<CardCanvasCoordinator>
{
    public GameObject playerCardPrefab;
    public GameObject playerCardGhostPrefab;
    public Transform realCardContainer;

    public Dictionary<Owner, PlayerCard> cards = new Dictionary<Owner, PlayerCard>();

    public static PlayerCard CreateCard(Owner owner){
        if(Instance.cards.ContainsKey(owner))
            return null;
        GameObject newCardGO = Instantiate(Instance.playerCardPrefab);
        GameObject newCardGhost = Instantiate(Instance.playerCardGhostPrefab);
        newCardGO.transform.parent = Instance.realCardContainer;
        newCardGhost.transform.parent = Instance.transform;

        newCardGO.transform.localScale = new Vector3(1, 1, 1);
        newCardGhost.transform.localScale = new Vector3(1, 1, 1);

        PlayerCard newCard = newCardGO.GetComponent<PlayerCard>();
        newCard.targetCardGhost = newCardGhost.transform;
        Instance.cards.Add(owner, newCard);
        newCard.owner = owner;
        return newCard;
    }
    public static void RemovePlayerCard(Owner owner){
        Destroy(Instance.cards[owner].gameObject);
        Instance.cards.Remove(owner);
    }

    public static void Sort(){
        Transform[] countOrdered = Instance.cards.
            OrderByDescending(pair => pair.Key.GetPlayerProfile().GetHealth())
            .ThenByDescending(pair => pair.Key.GetPlayerProfile().GetGrass())
            .Select(pair => pair.Value.targetCardGhost)
            .ToArray();
        for(int i=0; i<countOrdered.Length; i++){
            countOrdered[i].SetSiblingIndex(i);
        }
    }
}

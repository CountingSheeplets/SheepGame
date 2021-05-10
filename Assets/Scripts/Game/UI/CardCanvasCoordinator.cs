using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardCanvasCoordinator : Singleton<CardCanvasCoordinator> {
    public GameObject playerCardPrefab;
    public GameObject playerCardGhostPrefab;
    public Transform realCardContainer;

    public Dictionary<Owner, PlayerCard> cards = new Dictionary<Owner, PlayerCard>();
    public List<Transform> cardList = new List<Transform>();

    public static PlayerCard CreateCard(Owner owner) {
        if (Instance.cards.ContainsKey(owner))
            return null;
        GameObject newCardGO = Instantiate(Instance.playerCardPrefab);
        GameObject newCardGhost = Instantiate(Instance.playerCardGhostPrefab);
        newCardGO.transform.SetParent(Instance.realCardContainer);
        newCardGhost.transform.SetParent(Instance.transform);

        newCardGO.transform.localScale = new Vector3(1, 1, 1);
        newCardGhost.transform.localScale = new Vector3(1, 1, 1);

        PlayerCard newCard = newCardGO.GetComponent<PlayerCard>();
        newCard.targetCardGhost = newCardGhost.transform;
        Instance.cards.Add(owner, newCard);
        Instance.cardList.Add(newCard.GetComponent<Transform>());
        newCard.owner = owner;
        return newCard;
    }
    public static void RemovePlayerCard(Owner owner) {
        if (Instance.cards.ContainsKey(owner)) {
            if (Instance.cards[owner] == null)return;
            Destroy(Instance.cards[owner].gameObject);
            Instance.cardList.Remove(Instance.cards[owner].GetComponent<Transform>());
            Instance.cards.Remove(owner);
        }
    }

    public void ClearInvalidCards() {
        foreach (Owner owner in cards.Keys) {
            if (!owner.IsInListByType(OwnersCoordinator.GetOwners())) {
                RemovePlayerCard(owner);
            }
        }
    }

    public static bool Sort() {
        List<Transform> countOrdered = Instance.cards.
        OrderByDescending(pair => pair.Key.GetPlayerProfile().isAlive).
        ThenByDescending(pair => pair.Key.GetPlayerProfile().GetMoneyEarned())
            .Select(pair => pair.Value.targetCardGhost)
            .ToList();
        bool sorted = !countOrdered.SequenceEqual(Instance.cardList);
        if (sorted)
            for (int i = 0; i < countOrdered.Count; i++) {
                countOrdered[i].SetSiblingIndex(i);
            }
        return sorted;
    }
}
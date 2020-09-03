using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScreenController : MonoBehaviour {
    public GameObject scoreRowPrefab;
    public Transform scoreScreen;
    public Transform scoresContainer;

    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }

    void OnScoresShow(GameMessage msg) {
        Debug.Log("OnScore Screen stuff:");
        scoreScreen.gameObject.SetActive(true);
        bool first = true;
        List<ScoreRow> scoreRows = new List<ScoreRow>();
        Debug.Log("owners total: :" + OwnersCoordinator.GetOwners().Count);
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            Debug.Log("scoring owner: :" + owner);
            PlayerScores scores = ScoreCoordinator.GetPlayerScores(owner);
            GameObject newScoreRow = Instantiate(scoreRowPrefab, scoresContainer);
            //newScoreRow.transform.SetSiblingIndex(1);

            newScoreRow.name = "ScoreRow:" + owner.ownerName;
            ScoreRow row = newScoreRow.GetComponent<ScoreRow>();
            scoreRows.Add(row);
            row.InitScoreRow(owner.ownerName, owner.GetPlayerProfile().playerColor, scores);
            row.eliminatedPlace = owner.GetPlayerProfile().eliminatedPlace;
        }
        scoreRows = scoreRows.OrderByDescending(row => row.eliminatedPlace).ToList();
        for (int i = 0; i < scoreRows.Count; i++) {
            if (first) {
                scoreRows[i].transform.SetSiblingIndex(i + 1);
                scoreRows[i].GetComponent<AspectRatioFitter>().aspectRatio -= 3;
                scoreRows[i].winnerX2.SetActive(true);
                first = false;
            } else {
                scoreRows[i].transform.SetSiblingIndex(i + 2);
            }
            //Debug.Log("row eliminated: "+scoreRows[i].eliminatedPlace+ "  "+scoreRows[i].playerName);
            //Debug.Log("get index: "+scoreRows[i].transform.GetSiblingIndex());
        }

        //winnerNameScrollbar.text = "winner - "+scoreRows[0].playerName.text;//+"     ";
        //winnerNameScrollbar.text+=winnerNameScrollbar.text;
        //winnerNameScrollbar.text+=winnerNameScrollbar.text;
    }
}
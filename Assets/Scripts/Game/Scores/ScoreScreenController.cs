using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class ScoreScreenController : MonoBehaviour
{
    public GameObject scoreRowPrefab;
    public Transform scoreScreen;
    public Transform scoresContainer;

    public TextMeshProUGUI winnerNameScrollbar;

    void Start()
    {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }

    void OnScoresShow(GameMessage msg)
    {
        scoreScreen.gameObject.SetActive(true);
        bool first = true;
        List<ScoreRow> scoreRows = new List<ScoreRow>();
        foreach(Owner owner in OwnersCoordinator.GetOwners()){
            PlayerScores scores = ScoreCoordinator.GetPlayerScores(owner);
            GameObject newScoreRow = Instantiate(scoreRowPrefab, scoresContainer);
                newScoreRow.transform.SetSiblingIndex(1);

            newScoreRow.name = "ScoreRow:"+owner.ownerName;
            ScoreRow row = newScoreRow.GetComponent<ScoreRow>();
            scoreRows.Add(row);
            row.InitScoreRow(owner.ownerName, owner.GetPlayerProfile().playerColor, scores);
            row.eliminatedPlace = owner.GetPlayerProfile().eliminatedPlace;
        }
        scoreRows.OrderByDescending(row => row.eliminatedPlace).ToArray();
        for (int i = 0; i < scoreRows.Count; i++)
        {
            if(first){
                scoreRows[i].transform.SetSiblingIndex(i+1);
                first = false;
            } else {
                scoreRows[i].transform.SetSiblingIndex(i+2);
            }
        }

        winnerNameScrollbar.text = "     winner - "+scoreRows[0].playerName.text+"     ";
        winnerNameScrollbar.text+=winnerNameScrollbar.text;
        winnerNameScrollbar.text+=winnerNameScrollbar.text;
    }
}

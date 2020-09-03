using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreRow : MonoBehaviour {
    public TextMeshProUGUI playerName;
    public Sprite[] scoreStripeIcons;
    public Transform[] iconLocations;
    public TextMeshProUGUI total;
    public int eliminatedPlace = 0;
    public GameObject scoreIconPrefab;
    //play again stuff:
    public GameObject playAgainCheckMark;
    //public Animator playAgainOverlayAnim;
    public Animator playAgainGlintAnim;
    public Animator playerNameAnim;
    public GameObject winnerX2;
    public void InitScoreRow(string _playerName, Color _playerColor, PlayerScores scores) {
        playerName.text = _playerName;
        GetComponent<Image>().color = _playerColor;
        //playAgainOverlayAnim.GetComponent<Image>().color = _playerColor;
        int econ = scores.GetScoreSum(ScoreType.Economy);
        //economy.text = econ.ToString();
        int mil = scores.GetScoreSum(ScoreType.Military);
        int tech = scores.GetScoreSum(ScoreType.Technology);
        total.text = (econ + mil + tech).ToString();
        List<string> scoreIcons = new List<string>();
        foreach (Score score in scores.scores) {
            if (score.total > 0)
                scoreIcons.Add(score.icon);
        }

        for (int i = 0; i < scoreIcons.Count; i++) {
            GameObject newIconOnStrip = Instantiate(scoreIconPrefab, Vector3.zero, Quaternion.identity, iconLocations[i]);
            newIconOnStrip.transform.localPosition = Vector3.zero;
            newIconOnStrip.GetComponent<Image>().sprite = GetIcon(scoreIcons[i]);
            newIconOnStrip.transform.localScale *= 0.9f;
        }
    }

    public void SetPlayAgain() {
        playAgainCheckMark.SetActive(true);
        playAgainGlintAnim.SetTrigger("run");
        playerNameAnim.SetTrigger("playerReady");
    }
    public Sprite GetIcon(string name) {
        foreach (Sprite sprite in scoreStripeIcons) {
            string iconName = sprite.ToString().Split(' ') [0];
            if (iconName == name)
                return sprite;
        }
        return null;
    }

}
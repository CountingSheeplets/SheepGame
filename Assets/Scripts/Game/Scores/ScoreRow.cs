using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreRow : MonoBehaviour {
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI economy;
    public TextMeshProUGUI military;
    public TextMeshProUGUI technology;
    public TextMeshProUGUI total;
    public int eliminatedPlace = 0;

    //play again stuff:
    public GameObject playAgainCheckMark;
    //public Animator playAgainOverlayAnim;
    public Animator playAgainGlintAnim;
    public Animator playerNameAnim;
    public Animator playAgainTextAnim;

    public void InitScoreRow(string _playerName, Color _playerColor, PlayerScores scores) {
        playerName.text = _playerName;
        GetComponent<Image>().color = _playerColor;
        //playAgainOverlayAnim.GetComponent<Image>().color = _playerColor;
        int econ = scores.GetScoreSum(ScoreType.Economy);
        economy.text = econ.ToString();
        int mil = scores.GetScoreSum(ScoreType.Military);
        military.text = mil.ToString();
        int tech = scores.GetScoreSum(ScoreType.Technology);
        technology.text = tech.ToString();

        total.text = (econ + mil + tech).ToString();
    }

    public void SetPlayAgain() {
        playAgainCheckMark.SetActive(true);
        ///playAgainOverlayAnim.SetTrigger("show");
        playAgainGlintAnim.SetTrigger("run");
        playerNameAnim.SetBool("isFadeoutLoopActive", true);
        playAgainTextAnim.SetBool("isFadeoutLoopActive", true);
    }
}
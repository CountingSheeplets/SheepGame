using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ScoreTitleHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }

    void OnScoresShow(GameMessage msg) {
        GetComponent<TextMeshProUGUI>().text = TranslationsHandler.GetAchievementsTranslation();
    }
}
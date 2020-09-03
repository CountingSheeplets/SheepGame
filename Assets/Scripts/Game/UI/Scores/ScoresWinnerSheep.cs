using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoresWinnerSheep : MonoBehaviour {
    public Transform offsetModelA;
    public Transform offsetModelB;
    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }
    void OnScoresShow(GameMessage msg) {
        PlayerProfile winnerProfile = OwnersCoordinator.GetOwners().Select(x => x.GetPlayerProfile()).OrderByDescending(row => row.eliminatedPlace).FirstOrDefault();
        GameObject sheepModelGO_A = SheepFactory.CreateSheepModel(winnerProfile.owner, offsetModelA);
        sheepModelGO_A.transform.localScale = Vector3.one * 3f;
        GameObject sheepModelGO_B = SheepFactory.CreateSheepModel(winnerProfile.owner, offsetModelB);
        sheepModelGO_B.transform.localScale = Vector3.one * 3f;

        //sheepModelGO_A.GetComponent<Animator>().SetFloat("dirX_blend", -1);
        //sheepModelGO_A.GetComponent<Animator>().SetFloat("dirY_blend", -1);
        sheepModelGO_A.GetComponent<SpineContainerBlendsEight>().WalkTo((Vector2) transform.position + Vector2.up);
        //sheepModelGO_B.GetComponent<Animator>().SetFloat("dirX_blend", -1);
        //sheepModelGO_B.GetComponent<Animator>().SetFloat("dirY_blend", 0);
        sheepModelGO_B.GetComponent<SpineContainerBlendsEight>().WalkTo((Vector2) transform.position + Vector2.up);
        //sheep and king not animating to walk properly, smth overrides it but cannot find...
        //Conclusion: this is most likely due to units being children of UI elements, fucks up position :(
    }
}
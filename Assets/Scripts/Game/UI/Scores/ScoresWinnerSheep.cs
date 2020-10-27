using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoresWinnerSheep : MonoBehaviour {
    public Transform offsetModelA;
    public Transform offsetModelB;
    int sheepAmount = 8;
    public List<Transform> scoreSheeps = new List<Transform>();

    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }
    void OnScoresShow(GameMessage msg) {
        CreateSheep();
        SetupScoreSheep();
    }
    void OnScoresShowOld(GameMessage msg) {
        PlayerProfile winnerProfile = OwnersCoordinator.GetOwners().Select(x => x.GetPlayerProfile()).OrderByDescending(row => row.eliminatedPlace).FirstOrDefault();
        GameObject sheepModelGO_A = SheepFactory.CreateSheepModel(winnerProfile.owner, offsetModelA);
        sheepModelGO_A.transform.localScale = Vector3.one * 15f;
        GameObject sheepModelGO_B = SheepFactory.CreateSheepModel(winnerProfile.owner, offsetModelB);
        sheepModelGO_B.transform.localScale = Vector3.one * 15f;

        //sheepModelGO_A.GetComponent<Animator>().SetFloat("dirX_blend", -1);
        //sheepModelGO_A.GetComponent<Animator>().SetFloat("dirY_blend", -1);
        sheepModelGO_A.GetComponent<SpineContainerBlendsEight>().WalkTo((Vector2)transform.position + Vector2.up);
        //sheepModelGO_B.GetComponent<Animator>().SetFloat("dirX_blend", -1);
        //sheepModelGO_B.GetComponent<Animator>().SetFloat("dirY_blend", 0);
        sheepModelGO_B.GetComponent<SpineContainerBlendsEight>().WalkTo((Vector2)transform.position + Vector2.up);
        //sheep and king not animating to walk properly, smth overrides it but cannot find...
        //Conclusion: this is most likely due to units being children of UI elements, fucks up position :(
    }

    void CreateSheep() {
        List<Owner> owners = OwnersCoordinator.GetOwners();
        for (int i = 0; i < sheepAmount; i++) {
            int ownerIndex = (int)(i % owners.Count);
            Transform scoreSheep = CreateSheep(owners[ownerIndex]);
            scoreSheeps.Add(scoreSheep);
            //scoreSheep.transform.position = PosInCircle(i);
        }
    }

    Transform CreateSheep(Owner owner) {
        GameObject sheepModelGO_A = SheepFactory.CreateSheepModel(owner, offsetModelA);
        sheepModelGO_A.transform.localScale = Vector3.one * 5f;
        sheepModelGO_A.AddComponent<ScoreSheep>();
        return sheepModelGO_A.transform;
    }
    void SetupScoreSheep() {
        for (int i = 0; i < scoreSheeps.Count; i++) {
            scoreSheeps[i].GetComponent<ScoreSheep>().Setup(i);
        }
    }

    /*     Vector3 PosInCircle(int index) {
            Vector3 center = Vector3.zero;
            float ang = (float)index / sheepAmount;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad) * 2f;
            pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = center.z;
            return pos;
        } */
}
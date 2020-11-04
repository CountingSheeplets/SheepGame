using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoresWinnerSheep : MonoBehaviour {
    public Transform sheepPanel;
    int sheepAmount = 8;
    public List<Transform> scoreSheeps = new List<Transform>();
    float width;
    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
        width = this.gameObject.GetComponent<RectTransform>().rect.width;
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }
    void OnScoresShow(GameMessage msg) {
        CreateSheep();
        SetupScoreSheep();
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
        GameObject sheepModelGO_A = SheepFactory.CreateSheepModel(owner, sheepPanel);
        sheepModelGO_A.transform.localScale = Vector3.one * 10f * width / 1920f;
        sheepModelGO_A.AddComponent<ScoreSheep>();
        return sheepModelGO_A.transform;
    }
    void SetupScoreSheep() {
        for (int i = 0; i < scoreSheeps.Count; i++) {
            scoreSheeps[i].GetComponent<ScoreSheep>().Setup(i);
        }
    }
}
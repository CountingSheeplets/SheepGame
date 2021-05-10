using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.Rendering;
public class ScoresWinnerKing : MonoBehaviour {

    public Transform offsetTransform;
    //SortingGroup sGroup;

    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
    }
    void OnScoresShow(GameMessage msg) {
        PlayerProfile winnerProfile = OwnersCoordinator.GetOwners().Select(x => x.GetPlayerProfile()).OrderByDescending(row => row.eliminatedPlace).FirstOrDefault();
        GameObject modelGo = KingFactory.TryCreateHeroModel(winnerProfile.owner, offsetTransform);
        KingModel model = modelGo.GetComponent<KingModel>();
        modelGo.transform.localPosition = Vector3.zero;
        modelGo.transform.localScale *= 1.6f;
        model.SetHat(winnerProfile.selectedHat);
        model.SetScepter(winnerProfile.selectedScepter);
        Animator anim = modelGo.GetComponent<Animator>();
        anim.SetFloat("dirX_blend", 1);
        anim.SetFloat("dirY_blend", 0);
        anim.SetTrigger("stopWalk");
    }
}
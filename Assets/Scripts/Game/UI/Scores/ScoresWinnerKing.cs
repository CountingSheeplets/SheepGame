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
        //if (sGroup == null)sGroup = model.GetComponentInChildren<SortingGroup>();
        modelGo.transform.localPosition = Vector3.zero;
        modelGo.transform.localScale *= 1.6f;
        //model.ChangeColor(winnerProfile.owner.teamId);
        model.SetHat(winnerProfile.selectedHat);
        model.SetScepter(winnerProfile.selectedScepter);
        //modelGo.GetComponent<SpineContainerBlendsFour>().WalkTo(Vector2.up + (Vector2) transform.position);
        //Debug.Log(transform.position);
        //Debug.Log(transform.parent.position);
        Animator anim = modelGo.GetComponent<Animator>();
        anim.SetFloat("dirX_blend", 1);
        anim.SetFloat("dirY_blend", 0);
        anim.SetTrigger("stopWalk");
        //if (sGroup)
        //    sGroup.sortingOrder = 200;
        //Debug.Log("getFLoat:" + anim.GetFloat("dirX_blend"));
    }
}
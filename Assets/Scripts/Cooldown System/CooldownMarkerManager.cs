/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CooldownMarkerManager : Singleton<CooldownMarkerManager>
{
    public GameObject cooldownMarkerPrefab;
    public List<GameObject> cooldownMarkers = new List<GameObject>();
    public Dictionary<BaseAbility, GameObject>  markedOtherBBs = new Dictionary<BaseAbility, GameObject> ();
    public List<GameObject> markedFutureBBs = new List<GameObject>();
    
    protected override void OnAwake(){
        EventManager.StartListening(EventName.System.Cooldown.Tick(), OnCdTick);
        EventManager.StartListening(EventName.System.Cooldown.Ended(), OnCdEnded);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.System.Cooldown.Tick(), OnCdTick);
        EventManager.StopListening(EventName.System.Cooldown.Ended(), OnCdEnded);
    }
    void OnCdTick(GameMessage msg){
        ClearNulls();
        TryMarkFutures(msg.cooldownGroup);
        TryMarkOthers(msg.cooldownGroup);
    }
    void TryMarkFutures(CooldownGroup cdGroup){
        List<BaseAbility> allUniqueFutures = GetMarkableFutures();
        //Debug.Log("total markables: futures: "+allUniqueFutures.Count);
        foreach(BaseAbility futureBB in allUniqueFutures){
            if(cdGroup.IsMyGroup(futureBB)){
                if (!futureBB.InListByType(markedFutureBBs.Select(x => x.GetComponent<BaseAbility>()).ToList())){
                    markedFutureBBs.Add(futureBB.gameObject);
                    GameObject cdMarker = cooldownMarkerPrefab.GetOrCreateStorableObject(cooldownMarkers, transform);
                    cdMarker.GetComponent<CooldownMarker>().Mark(futureBB.gameObject);
                }
            }
        }
    }
    void TryMarkOthers(CooldownGroup cdGroup){
        Dictionary<BaseAbility, GameObject> otherMarkables = GetMarkableOthers();
        //Debug.Log("total markables: other: "+otherMarkables.Count);
        foreach(BaseAbility otherBB in otherMarkables.Keys){
            if(cdGroup.IsMyGroup(otherBB)){
                if (!otherBB.InListByType(markedOtherBBs.Keys.Select(x => x.GetComponent<BaseAbility>()).ToList())){
                    markedOtherBBs.Add(otherBB, otherMarkables[otherBB]);
                    GameObject cdMarker = cooldownMarkerPrefab.GetOrCreateStorableObject(cooldownMarkers, transform);
                    cdMarker.GetComponent<CooldownMarker>().Mark(otherMarkables[otherBB]);
                }
            }
        }
    }
    //this finds only first unique of Future's, puts all uniques to list:
    List<BaseAbility> GetMarkableFutures(){
        List<BaseAbility> allFutures = new List<BaseAbility>(FindObjectsOfType<MonoBehaviour>().OfType<Future>().Cast<BaseAbility>().ToList());
        List<BaseAbility> addableFutures = new List<BaseAbility>();
        foreach(BaseAbility future in allFutures){
            BaseAbility bbInList = future.InListByType(addableFutures.Cast<BaseAbility>().ToList());
            if (!bbInList){
                addableFutures.Add(future);
            } else {
                addableFutures[addableFutures.IndexOf(bbInList)] = future;
            }
        }
        return addableFutures.Select(x=>(x as BaseAbility)).ToList();
    }
    //this finds all items other than futures:
    Dictionary<BaseAbility, GameObject> GetMarkableOthers(){
        List<BuildingMenuItem> menuItems = new List<BuildingMenuItem>(FindObjectsOfType<BuildingMenuItem>());
        Dictionary<BaseAbility, GameObject> items = new Dictionary<BaseAbility, GameObject>(menuItems.ToDictionary(x=>x.buildingPrefab.GetComponent<BaseAbility>(), y=>y.gameObject));
        if (GhostManager.ghost)
            items.Add(GhostManager.ghost, GhostManager.ghost.gameObject);
        return items;
    }
    void OnCdEnded(GameMessage msg){
        ClearNulls();
        //remove futures from future list:
        for(int i=markedFutureBBs.Count-1; i>=0; i--){
            if (msg.cooldownGroup.IsMyGroup(markedFutureBBs[i].GetComponent<BaseAbility>())){
                markedFutureBBs.RemoveAt(i);
            }
        }
        //remove otherBBs from others list:
        for(int i=markedOtherBBs.Count-1; i>=0; i--){
            BaseAbility bb = markedOtherBBs.ElementAt(i).Key;
            if (msg.cooldownGroup.IsMyGroup(bb)){
                markedOtherBBs.Remove(bb);
            }
        }
    }
    void ClearNulls(){
        for(int i=markedFutureBBs.Count-1; i>=0; i--){
            if (markedFutureBBs[i] == null){
                markedFutureBBs.RemoveAt(i);
            }
        }
        for(int i=markedOtherBBs.Count-1; i>=0; i--){
            BaseAbility bb = markedOtherBBs.ElementAt(i).Key;
            if (markedOtherBBs[bb] == null){
                markedOtherBBs.Remove(bb);
            }
        }
    }
} */
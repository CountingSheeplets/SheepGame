using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSpawner : MonoBehaviour
{
    public List<KingUnit> kings = new List<KingUnit>();
    public GameObject kingUnitPrefab;
    void Start()
    {
        EventManager.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.Input.StartGame(), OnStartGame);
    }

    void OnStartGame(GameMessage msg)
    {
        List<Owner> owners = OwnersManager.GetOwners();
        foreach(Owner owner in owners){
            GameObject newKingGO = Instantiate(kingUnitPrefab);
            KingUnit newKing = newKingGO.GetComponent<KingUnit>();
            newKing.owner = owner;
            newKingGO.transform.parent = ArenaManager.GetPlayfield(owner).transform;
            newKingGO.transform.position = Vector3.zero;
            GameObject newKingModelGO = Instantiate(KingModelManager.GetHeroModel(owner.ownerId));
            newKingModelGO.transform.parent = newKingGO.transform;
            newKingModelGO.transform.localPosition = Vector3.zero;
            kings.Add(newKing);
            EventManager.TriggerEvent(EventName.System.King.Spawned(), GameMessage.Write().WithOwner(owner).WithGameObject(newKingGO));
        }
    }
}

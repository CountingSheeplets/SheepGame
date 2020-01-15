using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class PlayerProfileNetworkWriter : MonoBehaviour
{
    float counter;
    void FixedUpdate(){
        counter += Time.deltaTime;
        if(counter > ConstantsBucket.ProfileUpdateInterval){
            counter = 0;
            foreach(Owner owner in OwnersCoordinator.GetOwners()){
                TrySendProfile(owner);
            }
        }
    }

    void TrySendProfile(Owner owner){
        if(AirConsole.instance == null)
            return;
        PlayerProfile profile = owner.GetPlayerProfile();
        if(profile != null)
            if(profile.IsNetworkDirty()){
                if((GameStateView.GetGameState() & GameState.started) != 0){
                    if(NetworkCoordinator.SendProfile(profile)){
                        EventCoordinator.TriggerEvent(EventName.System.Player.ProfileUpdate(), GameMessage.Write().WithPlayerProfile(profile).WithOwner(owner));
                        CardCanvasCoordinator.Sort();
                        EventCoordinator.TriggerEvent(EventName.System.Player.PlayerCardsSorted(), GameMessage.Write());
                    } else {
                        Debug.Log("Cant Send... profile not found or player is dead for owner: "+owner);
                    }
                } else {
                    NetworkCoordinator.SendKingItems(owner);
                    profile.CleanNetworkDirty();
                }
            }
    }
}

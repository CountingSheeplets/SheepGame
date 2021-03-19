using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using UnityEngine;

public class PlayerProfileNetworkWriter : MonoBehaviour {
    float counter;
    void FixedUpdate() {
        counter += Time.deltaTime;
        if (counter > ConstantsBucket.ProfileUpdateInterval) {
            counter = 0;
            foreach (Owner owner in OwnersCoordinator.GetOwners()) {
                TrySendProfile(owner);
            }
        }
    }

    void TrySendProfile(Owner owner) {
        if (AirConsole.instance == null)
            return;
        PlayerProfile profile = owner.GetPlayerProfile();
        if (profile != null)
            if (profile.IsNetworkDirty() && !GameStateView.HasState(GameState.ended)) {
                if (GameStateView.HasState(GameState.started)) {
                    if (NetworkCoordinator.SendProfile(profile)) {
                        EventCoordinator.TriggerEvent(EventName.System.Player.ProfileUpdate(), GameMessage.Write().WithPlayerProfile(profile).WithOwner(owner));
                        if (CardCanvasCoordinator.Sort())
                            EventCoordinator.TriggerEvent(EventName.System.Player.PlayerCardsSorted(), GameMessage.Write());
                    }
                } else {
                    NetworkCoordinator.SendKingItems(owner);
                }
            }
    }
}
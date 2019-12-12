using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OwnerTileResizer : MonoBehaviour
{
   public float height;
 
    void Start ()
    {
        //EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), Resize);
        Resize(GameMessage.Write());
    }
    void OnDestroy(){
        //EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), Resize);
    }
    void Resize(GameMessage msg){
        GridLayoutGroup layout = GetComponent<GridLayoutGroup>();
        height = this.gameObject.GetComponent<RectTransform>().rect.height;
        Debug.Log("Resizing...:height:"+height);
        layout.spacing = new Vector2(height/25, height/25);
        Vector2 newSize = new Vector2(height / 2f - layout.spacing.x, height / 2.5f - layout.spacing.y);
        layout.cellSize = newSize;
    }
}

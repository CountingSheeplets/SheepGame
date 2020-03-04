using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsBucket : Singleton<TipsBucket>
{
    public List<string> tips = new List<string>();
    int currentTip = 0;
    List<int> order = new List<int>();

    void Start()
    {
        List<int> tmpOrders = new List<int>();
        for(int i = 0; i < tips.Count; i++){
            tmpOrders.Add(i);
        }
        for(int i = 0; i < tips.Count; i++){
            int index = Random.Range(0, tmpOrders.Count);
            order.Add(tmpOrders[index]);
            tmpOrders.RemoveAt(index);
        }
    }

    public static string GetNextTip()
    {
        Instance.currentTip ++;
        if(Instance.currentTip >= Instance.order.Count)
            Instance.currentTip =0;
        return Instance.tips[Instance.order[Instance.currentTip]];
    }
}

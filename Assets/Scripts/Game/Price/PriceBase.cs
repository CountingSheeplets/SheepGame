using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class PriceBase
{
    public float basePrice = 0f;
    public float increment = 0f;
    public int level = 0;

    public float Total(){
        return basePrice + increment * level;
    }
    public void IncreaseLevel(int _level){
        level += _level;
    }
    public void SetLevel(int _level){
        level = _level;
    }

    public PriceBase(PriceBase newBase){
        basePrice = newBase.basePrice;
        increment = newBase.increment;
        level  = newBase.level;
    }
}

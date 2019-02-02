using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisAble : MonoBehaviour {

    GameObject Helper;
    GameObject Map1;
    GameObject Map2;
    bool MapState=false;
    public void ChangeHelperState()
    {
        if (Helper.activeSelf)
        {
            Helper.SetActive(false);
        }
        else
        {
            Helper.SetActive(true);
        }
    }

    public void ChangeMapState()
    {  
        Map1.SetActive(MapState);
        Map2.SetActive(MapState);
        MapState = !MapState;
    }
}

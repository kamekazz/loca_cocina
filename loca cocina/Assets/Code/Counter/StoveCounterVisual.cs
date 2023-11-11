using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] GameObject[] stoveOnGameObjectArray;
    [SerializeField] StoveCounter stoveCounterSP;

    void Start()
    {
        stoveCounterSP.OnStateChanged += ToggleOnOrOff;
    }

    private void ToggleOnOrOff(object sender, StoveCounter.OnStateChangedArgs e)
    {

        if (e.state != StoveCounter.State.Idle)
        {
            OnStove();
        }
        else
        {
            OffStove();
        }
    }

    void OnStove()
    {
        foreach (GameObject item in stoveOnGameObjectArray)
        {
            item.SetActive(true);
        }
    }


    void OffStove()
    {
        foreach (GameObject item in stoveOnGameObjectArray)
        {
            item.SetActive(false);
        }
    }
}

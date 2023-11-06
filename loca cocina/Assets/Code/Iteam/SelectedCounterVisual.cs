using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] ClearCounter clearCounterSP;
    [SerializeField] GameObject visualGameObject;
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounterSP)
        {
            visualGameObject.SetActive(true);
        }
        else
        {
            visualGameObject.SetActive(false);

        }
    }
}

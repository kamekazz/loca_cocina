using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounterSP;
    [SerializeField] GameObject[] visualGameObjectArray;
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounterSP)
        {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {

                visualGameObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {

                visualGameObject.SetActive(false);
            }

        }
    }
}

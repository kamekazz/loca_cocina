using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounterSP;
    Animator animatorCP;

    void Awake()
    {
        animatorCP = GetComponent<Animator>();
    }

    void Start()
    {
        cuttingCounterSP.OnProgressChanged += CuttingCounter_OnPlayerCutting;

    }

    private void CuttingCounter_OnPlayerCutting(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {

        if (e.progressNormalized != 0f)
        {

            animatorCP.SetTrigger("Cut");
        }
    }
}

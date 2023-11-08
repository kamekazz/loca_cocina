using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] ContainerCounter containerCounterSP;
    Animator animatorCP;

    void Awake()
    {
        animatorCP = GetComponent<Animator>();
    }

    void Start()
    {
        containerCounterSP.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animatorCP.SetTrigger("OpenClose");
    }
}

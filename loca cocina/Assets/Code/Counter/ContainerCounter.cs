using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] KitchenObjectSO kitchenObjectSO;



    public override void Interact(Player playerSP)
    {
        if (!playerSP.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, playerSP);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }


}

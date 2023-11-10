using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player playerSP)
    {
        if (playerSP.HasKitchenObject())
        {
            playerSP.GetKitchenObject().DestroySelf();
        }
    }
}

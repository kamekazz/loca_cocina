using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;






    public override void Interact(Player playerSP)
    {
        if (!HasKitchenObject())
        {
            if (playerSP.HasKitchenObject())
            {
                playerSP.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (playerSP.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(playerSP);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeArrayOS;
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

    public override void InteractAlternate(Player playerSP)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO _outPutKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(_outPutKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArrayOS)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO _cuttingRecipeSO in cuttingRecipeArrayOS)
        {
            if (_cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return _cuttingRecipeSO.output;
            }
        }
        return null;
    }
}

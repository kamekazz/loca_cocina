using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    [SerializeField] CuttingRecipeSO[] cuttingRecipeArrayOS;

    int cuttingProgress;
    public override void Interact(Player playerSP)
    {
        if (!HasKitchenObject())
        {
            if (playerSP.HasKitchenObject())
            {
                if (HasRecipeWithInput(playerSP.GetKitchenObject().GetKitchenObjectSO()))
                {
                    playerSP.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    UpdateProgressUI();
                }
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
            cuttingProgress++;

            CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput(
                                                                GetKitchenObject().GetKitchenObjectSO()
                                                            );
            UpdateProgressUI();
            if (cuttingProgress >= _cuttingRecipeSO.cuttingProgressMax)
            {

                KitchenObjectSO _outPutKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(_outPutKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (_cuttingRecipeSO != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (_cuttingRecipeSO != null)
        {
            return _cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO _cuttingRecipeSO in cuttingRecipeArrayOS)
        {
            if (_cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return _cuttingRecipeSO;
            }
        }
        return null;
    }

    private void UpdateProgressUI()
    {
        CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput(
                                                   GetKitchenObject().GetKitchenObjectSO()
                                               );
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            progressNormalized = (float)cuttingProgress / _cuttingRecipeSO.cuttingProgressMax
        });
    }

}

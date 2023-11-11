using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public event EventHandler<OnStateChangedArgs> OnStateChanged;
    public class OnStateChangedArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle, Frying, Fried, Burned
    }
    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    FryingRecipeSO fryingRecipeSO;
    State state;

    float fryingTimer = 0f;
    float burningTimer = 0f;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:

                    break;
                case State.Frying:

                    fryingTimer += Time.deltaTime;

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;

                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    if (burningTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.overDone, this);
                        state = State.Burned;
                    }
                    break;
                    // case State.Burned:
                    //     break;

            }

        }
    }

    public override void Interact(Player playerSP)
    {
        if (!HasKitchenObject())
        {
            if (playerSP.HasKitchenObject())
            {
                if (HasRecipeWithInput(playerSP.GetKitchenObject().GetKitchenObjectSO()))
                {
                    playerSP.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(
                                                               GetKitchenObject()
                                                               .GetKitchenObjectSO()
                                                           );
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
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
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO _fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (_fryingRecipeSO != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO _fryingRecipeSO in fryingRecipeSOArray)
        {
            if (_fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return _fryingRecipeSO;
            }
        }
        return null;
    }
}

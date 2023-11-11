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

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
        public bool isBurnetTimer;
    }

    public enum State
    {
        Idle, Frying, Fried, Burned
    }
    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    FryingRecipeSO fryingRecipeSO;
    State state;

    float fryingTimer = 0f;
    float cookieTime = 0f;

    public bool isBurnetTimer = false;


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
                    UpdateProgressUI();
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        fryingTimer = 0f;
                    }
                    break;
                case State.Fried:
                    fryingTimer += Time.deltaTime;
                    UpdateProgressUI();
                    isBurnetTimer = true;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
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
            cookieTime = _fryingRecipeSO.fryingTimerMax;
            return true;
        }
        else
        {
            cookieTime = 0f;
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

    private void UpdateProgressUI()
    {
        FryingRecipeSO _fryingRecipeSO = GetFryingRecipeSOWithInput(
                                                   GetKitchenObject().GetKitchenObjectSO()
                                               );

        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            progressNormalized = fryingTimer / cookieTime,
            isBurnetTimer = isBurnetTimer
        });



    }
}

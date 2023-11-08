using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObject kitchenObjectSP);

    public KitchenObject GetKitchenObject();

    public void ClearKitChenObject();

    public bool HasKitchenObject();
}



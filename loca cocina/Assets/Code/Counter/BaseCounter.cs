using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] Transform counterTopPointTF;

    KitchenObject kitchenObjectSP;
    public virtual void Interact(Player playerSP)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPointTF;
    }

    public void SetKitchenObject(KitchenObject kitchenObjectSP)
    {
        this.kitchenObjectSP = kitchenObjectSP;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObjectSP;
    }

    public void ClearKitChenObject()
    {
        kitchenObjectSP = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObjectSP != null;
    }
}

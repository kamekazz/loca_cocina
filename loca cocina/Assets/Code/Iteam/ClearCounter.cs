using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPointTF;

    KitchenObject kitchenObjectSP;





    public void Interact(Player playerSP)
    {
        if (kitchenObjectSP == null)
        {
            Transform kitchenObjectTF = Instantiate(kitchenObjectSO.gameObject.transform, counterTopPointTF);
            kitchenObjectTF.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObjectSP.SetKitchenObjectParent(playerSP);

        }
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

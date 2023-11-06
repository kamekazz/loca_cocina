using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPointTF;
    [SerializeField] ClearCounter secondClearCounter;
    [SerializeField] bool testing;
    KitchenObject kitchenObjectSP;

    void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {

            if (kitchenObjectSP != null)
            {
                kitchenObjectSP.SetClearCounter(secondClearCounter);
            }
        }
    }



    public void Interact()
    {
        if (kitchenObjectSP == null)
        {
            Transform kitchenObjectTF = Instantiate(kitchenObjectSO.gameObject.transform, counterTopPointTF);
            kitchenObjectTF.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObjectSP.GetClearCounter());
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

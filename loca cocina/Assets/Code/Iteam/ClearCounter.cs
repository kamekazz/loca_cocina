using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPointTF;
    public void Interact()
    {
        Debug.Log("Interact");
        Transform kitchenObjectTF = Instantiate(kitchenObjectSO.gameObject.transform, counterTopPointTF);
        kitchenObjectTF.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTF.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}

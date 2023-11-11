using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] Transform counterTopPoint;
    [SerializeField] Transform PlateVisualPrefab;
    [SerializeField] PlatesCounter platesCounterSP;

    List<GameObject> plateVisualList;
    void Awake()
    {
        plateVisualList = new List<GameObject>();
    }

    void Start()
    {
        platesCounterSP.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounterSP.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject _plateGO = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(_plateGO);
        Destroy(_plateGO);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform _plateVisualTransform = Instantiate(PlateVisualPrefab, counterTopPoint);

        float _plateOffSetY = .1f;
        _plateVisualTransform.localPosition = new Vector3(0, _plateOffSetY * plateVisualList.Count, 0);
        plateVisualList.Add(_plateVisualTransform.gameObject);
    }
}

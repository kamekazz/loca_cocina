using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounterSP;
    [SerializeField] Image barImageCP;


    void Start()
    {
        cuttingCounterSP.OnProgressChanged += CuttingCounter_OnProgressChanged;
        barImageCP.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImageCP.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized >= 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}

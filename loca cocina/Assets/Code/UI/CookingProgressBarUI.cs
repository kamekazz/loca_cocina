using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookingProgressBarUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounterSP;
    [SerializeField] Image barImageCP;


    void Start()
    {
        stoveCounterSP.OnProgressChanged += StoveCounter_OnProgressChanged;
        barImageCP.fillAmount = 0f;

        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, StoveCounter.OnProgressChangedEventArgs e)
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
        if (e.isBurnetTimer == true)
        {
            barImageCP.color = Color.red;
        }
        else
        {
            barImageCP.color = Color.yellow;
        }
        Debug.Log(e.isBurnetTimer);
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

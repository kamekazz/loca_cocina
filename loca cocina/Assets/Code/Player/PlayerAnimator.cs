using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    const string IS_WALKING = "isWalking";
    [SerializeField] Player playerSP;
    Animator animatorCP;

    void Awake()
    {
        animatorCP = GetComponent<Animator>();
        animatorCP.SetBool(IS_WALKING, false);
    }

    void Update()
    {
        if (playerSP.GetIsWalking())
        {
            animatorCP.SetBool(IS_WALKING, true);
        }
        else
        {
            animatorCP.SetBool(IS_WALKING, false);

        }
    }
}

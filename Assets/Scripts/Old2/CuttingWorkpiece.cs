using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CuttingWorkpiece : MonoBehaviour
{
    public event Action OnCuttingRod;

    private Animator animator;

    public GameObject silverBody;

    public GameObject silverCuttingZone;

    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources input_Sources = SteamVR_Input_Sources.Any;

    private void Start()
    {
        animator = GetComponent<Animator>();
        silverBody.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
       // if (other.gameObject == silverCuttingZone.gameObject
         //   && action.GetStateDown(input_Sources)
          //  && StateManager.currentState == State.CuttingRod)
        {
            Debug.Log("Ножницы работают");
            animator.SetBool("isCute", true);
            silverCuttingZone.SetActive(false);
            OnCuttingRod.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == silverCuttingZone.gameObject)
        {
            animator.SetBool("isCute", false);
        }
    }
}

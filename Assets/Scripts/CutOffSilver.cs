using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CutOffSilver : MonoBehaviour
{
    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources input_Sources = SteamVR_Input_Sources.Any;

    public GameObject zoneCutte;
    private Animator animator;

    private Interactable interactable;
    private Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        interactable = GetComponent<Interactable>();
    }

    private void OnTriggerStay(Collider other)
    {
        hand = interactable.attachedToHand;

        if (other.gameObject == zoneCutte.gameObject && hand != null
            && action.GetState(input_Sources)
            && StateManager.cuttingSubState == CuttingSubState.CuttingWorkpice)
        {
            animator.SetTrigger("isCute");
            Destroy(zoneCutte, 0.75f);
            StateManager.instance.CuttingSubStateComplete();
        }
    }
}

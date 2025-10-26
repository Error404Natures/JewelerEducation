using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GraspingMetalTweez : MonoBehaviour
{
    public GameObject[] silver;
    private GameObject grabSilver;
    public Transform pointFix;
    Throwable throwable;

    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources input = SteamVR_Input_Sources.Any;

    private Interactable interactable;
    private Hand hand;

    private bool isGrab = false;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        hand = interactable.attachedToHand;
        bool isHeld = hand != null;
       
        if (isHeld && action.GetState(input) && isGrab)
        {
            grabSilver.GetComponent<Rigidbody>().velocity = Vector3.zero;
            grabSilver.GetComponent<BoxCollider>().enabled = false;

            grabSilver.transform.position = pointFix.position;
            grabSilver.transform.rotation = pointFix.rotation;
        }
        else if(grabSilver != null)
        {
            grabSilver.GetComponent<BoxCollider>().enabled = true;
            isGrab = false ;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < silver.Length; i++) 
        {
            if (other.gameObject == silver[i].gameObject)
            {
                isGrab = true;
                grabSilver = silver[i];
            }
        }
    }
}

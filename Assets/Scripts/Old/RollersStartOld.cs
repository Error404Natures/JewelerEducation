using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RollersStartOld : MonoBehaviour
{
    public SteamVR_Action_Boolean action;
    private Interactable interactable;
    private Rigidbody rb;

    public GameObject blank;
    public Transform endPoint;

    public bool isPressed = false;

    public float moveSpeed = 0.2f;


    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (action.GetState(SteamVR_Input_Sources.RightHand) || action.GetState(SteamVR_Input_Sources.LeftHand))
        {
            if (blank.transform.position.x < endPoint.position.x)
            {
                blank.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                Debug.Log("ЭТА ХУЙНЯ РАБОТАТЬ БУДЕТ ИЛИ НЕТ?");
            }
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        if (action.GetState(hand.handType))
        {
            isPressed = true;
        }
    }
}
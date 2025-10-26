using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HummerBending : MonoBehaviour
{
    [Header("Настройки вибрации")]
    public SteamVR_Action_Vibration haptbcAction;
    public float hapticDuration = 0.15f;
    public float hapticFrequency = 100f;
    public float hapticAmplitude;

    private SteamVR_Input_Sources lastController;

    [Header("Настройки скрипта")]
    public GameObject silver;
    public GameObject ring;

    public int hitsRequired = 5;
    public float minImpactSpeed = 0.25f;
    public float maxImpactSpeed = 1.5f;

    public int currentHits = 0;

    public  AudioSource audioSource;

    private Rigidbody rb;
    private bool CurrentState => StateManager.bendingSubstate == BendingSubstate.hummerBending;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!CurrentState || collision.gameObject != silver) return;

        //if (CurrentState && collision.gameObject == silver)
        {
            float vercticalForce = Mathf.Abs(rb.velocity.y);

            //float impactSpeed = rb.velocity.magnitude;
            Debug.Log($"Скорость удара молотка: {vercticalForce:F2}");

            audioSource.Play();

            if ((minImpactSpeed < vercticalForce) && (vercticalForce < maxImpactSpeed))
            {
                currentHits++;
                TriggerHapticFeedback();
                Debug.Log($"Засчитанные удары: {currentHits}");
                CompleteBendingProcess();
            }
        }
    }

    private void CompleteBendingProcess()
    {
        if (currentHits >= hitsRequired) 
        {
            ring.transform.SetPositionAndRotation(silver.transform.position, silver.transform.rotation);
            ring.SetActive(true);
            silver.SetActive(false);
            StateManager.instance.BendingSubStateComplete();
        }
    }
    private void TriggerHapticFeedback()
    {
        Interactable interactable = GetComponent<Interactable>();
        if (interactable != null)
        {
            lastController = interactable.attachedToHand.handType;

            haptbcAction.Execute(0, hapticDuration, hapticFrequency, hapticAmplitude, lastController);
        }
    }
}

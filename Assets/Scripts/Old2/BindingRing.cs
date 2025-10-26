using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BindingRing : MonoBehaviour
{
    public event Action OnBindingRingChanged;

    public GameObject silverForCute; // �������� ������ (������ ������)
    public GameObject ring;         // �������� ������

    [Header("��������� ��������")]
    public float shakeVelocityThreshold = 5f;
    public int shakesRequired = 5;
    public float cooldownTime = 0.5f;

    // ��������� ����������
    private int currentShakes;
    private float lastShakeTime;
    private Vector3 lastPosition;
    private Hand attachedHand; // ����, �������� ������

    [Header("VR ��������")]
    public SteamVR_Input_Sources controller;
    public SteamVR_Action_Vibration hapticAction;
    public float hapticDuration = 0.1f;        // ������������ ��������
    public float hapticFrequency = 100f;        // ������� ��������
    public float hapticAmplitude = 0.5f;        // ���� ��������

    void Start()
    {
        lastPosition = silverForCute.transform.position;
        ring.SetActive(false);
    }

    void FixedUpdate()
    {
        //if (StateManager.currentState != State.Bending) return;

        // ���� ���� ��� �� ����������, �������� �����
        if (attachedHand == null)
        {
            var interactable = silverForCute.GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                attachedHand = interactable.attachedToHand;
                controller = interactable.attachedToHand.handType;

                hapticAction.Execute(0, hapticDuration, hapticFrequency, hapticAmplitude, controller);
            }
        }

        // ������������ �������� ���������
        Vector3 displacement = silverForCute.transform.position - lastPosition;
        float currentSpeed = displacement.magnitude / Time.fixedDeltaTime;
        lastPosition = silverForCute.transform.position;

        if (currentSpeed > shakeVelocityThreshold &&
            Time.time > lastShakeTime + cooldownTime)
        {
            RegisterShake();
        }
    }

    private void RegisterShake()
    {
        lastShakeTime = Time.time;
        currentShakes++;
        Debug.Log($"��������: {currentShakes}/{shakesRequired}");

        if (currentShakes >= shakesRequired)
        {
            TransformToRing();
        }
    }

    private void TransformToRing()
    {
        OnBindingRingChanged.Invoke();

        // 1. ���������� ������� ��������
        bool wasAttached = false;
        Transform handTransform = null;

        if (attachedHand != null)
        {
            wasAttached = true;
            handTransform = attachedHand.transform;

            // ���������� ������ ������
            attachedHand.DetachObject(silverForCute);
        }

        // 2. ������� � ��������
        ring.transform.position = silverForCute.transform.position;
        ring.transform.rotation = silverForCute.transform.rotation;

        // 3. ���������� ������
        ring.SetActive(true);
        Destroy(silverForCute);

        // 4. ����������� � ����
        if (wasAttached && handTransform != null)
        {
            // ������� ��������� ��������
            GameObject tempParent = new GameObject("TempParent");
            tempParent.transform.position = handTransform.position;
            tempParent.transform.rotation = handTransform.rotation;

            // ����������� ������ � ���������� ��������
            ring.transform.SetParent(tempParent.transform);

            // �������� ��������� Interactable ������
            var ringInteractable = ring.GetComponent<Interactable>();
            if (ringInteractable == null)
                ringInteractable = ring.AddComponent<Interactable>();

            // ����������� � ����
            attachedHand.AttachObject(ring, GrabTypes.Scripted);

            // ������� ���������� ��������
            Destroy(tempParent, 0.1f);
        }

       

        // 5. ��������� ������
        enabled = false;
    }
}

using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class IngotExtractor : MonoBehaviour
{
    public event Action OnIngonExtraction;

    [Header("�������� ���������")]
    //public GameObject silverIngotPrefab; // ������ ������ �������
    public Transform spawnPoint;          // ����� ��������� ������

    [Header("��������� ������")]
    public float shakeVelocityThreshold = 2.0f; // ����������� �������� ��� �����������
    public int shakesRequired = 5;             // ������ ���������� ��������
    public float cooldownTime = 0.3f;          // �������� ����� ����������

    [Header("VR ��������")]
    public SteamVR_Action_Vibration hapticAction;
    public float hapticDuration = 0.1f;        // ������������ ��������
    public float hapticFrequency = 100f;        // ������� ��������
    public float hapticAmplitude = 0.5f;        // ���� ��������

    // ��������� ����������
    private Rigidbody rb;
    private int currentShakes;
    private float lastShakeTime;
    private Vector3 lastPosition;
    private SteamVR_Input_Sources lastController;

    public GameObject silverIngus;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        silverIngus.SetActive(false);
    }

    private void FixedUpdate()
    {
        // ��������� ���������� �����
        //if (StateManager.currentState != State.RemovingFromMold) return;

        // ������������ �������� ��������
        Vector3 displacement = transform.position - lastPosition;
        float currentSpeed = displacement.magnitude / Time.fixedDeltaTime;
        lastPosition = transform.position;

        // ��������� ������� ��� ����������� ��������
      //  if (currentSpeed > shakeVelocityThreshold &&
          //  Time.time > lastShakeTime + cooldownTime && 
            //StateManager.currentState == State.RemovingFromMold)
        {
            RegisterShake();
        }
    }

    private void RegisterShake()
    {
        // ��������� ����� ��������� ��������
        lastShakeTime = Time.time;

        // ����������� �������
        currentShakes++;
        Debug.Log($"��������: {currentShakes}/{shakesRequired}");

        // ���������� ��������
        TriggerHapticFeedback();

        // ��������� ����������
        if (currentShakes >= shakesRequired)
        {
            ExtractIngot();
        }
    }

    private void TriggerHapticFeedback()
    {
        // �������� ����������, �������� ������
        var interactable = GetComponent<Interactable>();
        if (interactable != null && interactable.attachedToHand != null)
        {
            lastController = interactable.attachedToHand.handType;

            // ��������� ��������
            hapticAction.Execute(0, hapticDuration, hapticFrequency,
                                hapticAmplitude, lastController);
        }
    }

    private void ExtractIngot()
    {
        Debug.Log("���������� ������!");

        silverIngus.transform.position = spawnPoint.position;
        silverIngus.transform.rotation = spawnPoint.rotation;

        silverIngus.SetActive(true);

        // ������� ��������� ��������
        hapticAction.Execute(0, 0.3f, 150f, 1f, lastController);

        // ��������� � ���������� �����
        OnIngonExtraction.Invoke();

        // ��������� ������
        enabled = false;
    }

    // ��� ������� � ���������
    void OnGUI()
    {
       // if (StateManager.currentState == State.RemovingFromMold)
        {
            GUILayout.Label($"��������: {currentShakes}/{shakesRequired}");
            GUILayout.Label($"����� ��������: {shakeVelocityThreshold}");
        }
    }
}
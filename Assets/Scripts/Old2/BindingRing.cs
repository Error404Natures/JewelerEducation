using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BindingRing : MonoBehaviour
{
    public event Action OnBindingRingChanged;

    public GameObject silverForCute; // Исходный объект (прямой пруток)
    public GameObject ring;         // Согнутое кольцо

    [Header("Настройки сгибания")]
    public float shakeVelocityThreshold = 5f;
    public int shakesRequired = 5;
    public float cooldownTime = 0.5f;

    // Системные переменные
    private int currentShakes;
    private float lastShakeTime;
    private Vector3 lastPosition;
    private Hand attachedHand; // Рука, держащая объект

    [Header("VR Вибрация")]
    public SteamVR_Input_Sources controller;
    public SteamVR_Action_Vibration hapticAction;
    public float hapticDuration = 0.1f;        // Длительность вибрации
    public float hapticFrequency = 100f;        // Частота вибрации
    public float hapticAmplitude = 0.5f;        // Сила вибрации

    void Start()
    {
        lastPosition = silverForCute.transform.position;
        ring.SetActive(false);
    }

    void FixedUpdate()
    {
        //if (StateManager.currentState != State.Bending) return;

        // Если рука еще не определена, пытаемся найти
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

        // Рассчитываем скорость ЗАГОТОВКИ
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
        Debug.Log($"Встряска: {currentShakes}/{shakesRequired}");

        if (currentShakes >= shakesRequired)
        {
            TransformToRing();
        }
    }

    private void TransformToRing()
    {
        OnBindingRingChanged.Invoke();

        // 1. Запоминаем текущую привязку
        bool wasAttached = false;
        Transform handTransform = null;

        if (attachedHand != null)
        {
            wasAttached = true;
            handTransform = attachedHand.transform;

            // Открепляем старый объект
            attachedHand.DetachObject(silverForCute);
        }

        // 2. Позиция и вращение
        ring.transform.position = silverForCute.transform.position;
        ring.transform.rotation = silverForCute.transform.rotation;

        // 3. Активируем кольцо
        ring.SetActive(true);
        Destroy(silverForCute);

        // 4. Привязываем к руке
        if (wasAttached && handTransform != null)
        {
            // Создаем временный родитель
            GameObject tempParent = new GameObject("TempParent");
            tempParent.transform.position = handTransform.position;
            tempParent.transform.rotation = handTransform.rotation;

            // Прикрепляем кольцо к временному родителю
            ring.transform.SetParent(tempParent.transform);

            // Получаем компонент Interactable кольца
            var ringInteractable = ring.GetComponent<Interactable>();
            if (ringInteractable == null)
                ringInteractable = ring.AddComponent<Interactable>();

            // Прикрепляем к руке
            attachedHand.AttachObject(ring, GrabTypes.Scripted);

            // Удаляем временного родителя
            Destroy(tempParent, 0.1f);
        }

       

        // 5. Отключаем скрипт
        enabled = false;
    }
}
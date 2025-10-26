
using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class IngotExtractor : MonoBehaviour
{
    public event Action OnIngonExtraction;

    [Header("Основные настройки")]
    //public GameObject silverIngotPrefab; // Префаб слитка серебра
    public Transform spawnPoint;          // Точка появления слитка

    [Header("Настройки тряски")]
    public float shakeVelocityThreshold = 2.0f; // Минимальная скорость для регистрации
    public int shakesRequired = 5;             // Нужное количество встрясок
    public float cooldownTime = 0.3f;          // Задержка между встрясками

    [Header("VR Вибрация")]
    public SteamVR_Action_Vibration hapticAction;
    public float hapticDuration = 0.1f;        // Длительность вибрации
    public float hapticFrequency = 100f;        // Частота вибрации
    public float hapticAmplitude = 0.5f;        // Сила вибрации

    // Системные переменные
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
        // Проверяем активность этапа
        //if (StateManager.currentState != State.RemovingFromMold) return;

        // Рассчитываем скорость движения
        Vector3 displacement = transform.position - lastPosition;
        float currentSpeed = displacement.magnitude / Time.fixedDeltaTime;
        lastPosition = transform.position;

        // Проверяем условия для регистрации встряски
      //  if (currentSpeed > shakeVelocityThreshold &&
          //  Time.time > lastShakeTime + cooldownTime && 
            //StateManager.currentState == State.RemovingFromMold)
        {
            RegisterShake();
        }
    }

    private void RegisterShake()
    {
        // Обновляем время последней встряски
        lastShakeTime = Time.time;

        // Увеличиваем счетчик
        currentShakes++;
        Debug.Log($"Встряска: {currentShakes}/{shakesRequired}");

        // Активируем вибрацию
        TriggerHapticFeedback();

        // Проверяем завершение
        if (currentShakes >= shakesRequired)
        {
            ExtractIngot();
        }
    }

    private void TriggerHapticFeedback()
    {
        // Получаем контроллер, держащий объект
        var interactable = GetComponent<Interactable>();
        if (interactable != null && interactable.attachedToHand != null)
        {
            lastController = interactable.attachedToHand.handType;

            // Запускаем вибрацию
            hapticAction.Execute(0, hapticDuration, hapticFrequency,
                                hapticAmplitude, lastController);
        }
    }

    private void ExtractIngot()
    {
        Debug.Log("Извлечение слитка!");

        silverIngus.transform.position = spawnPoint.position;
        silverIngus.transform.rotation = spawnPoint.rotation;

        silverIngus.SetActive(true);

        // Сильная финальная вибрация
        hapticAction.Execute(0, 0.3f, 150f, 1f, lastController);

        // Переходим к следующему этапу
        OnIngonExtraction.Invoke();

        // Отключаем скрипт
        enabled = false;
    }

    // Для отладки в редакторе
    void OnGUI()
    {
       // if (StateManager.currentState == State.RemovingFromMold)
        {
            GUILayout.Label($"Встряски: {currentShakes}/{shakesRequired}");
            GUILayout.Label($"Порог скорости: {shakeVelocityThreshold}");
        }
    }
}
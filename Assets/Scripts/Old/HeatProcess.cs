using UnityEngine;
using UnityEngine.Events;

public abstract class HeatProcess : MonoBehaviour
{
    [Header("Основные настройки")]
    public State requiredState;
    public GameObject targetObject;
    public UnityEvent onProcessStart;
    public UnityEvent onProcessComplete;
    public UnityEvent onProcessStop;

    protected bool isProcessing;
    protected float progress;

    public virtual void Initialize()
    {
        // Базовая инициализация (при необходимости переопределить)
    }

    public virtual bool CanProcess(GameObject obj)
    {
        return obj == targetObject;
    }

    public virtual void StartProcess()
    {
        isProcessing = true;
        onProcessStart?.Invoke();
    }

    public virtual void UpdateProcess(float deltaTime)
    {
        if (!isProcessing) return;
    }

    public virtual void StopProcess()
    {
        isProcessing = false;
        onProcessStop?.Invoke();
    }

    protected virtual void CompleteProcess()
    {
        isProcessing = false;
        onProcessComplete?.Invoke();
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public abstract class BaseProcess : MonoBehaviour
{
    [SerializeField] protected GameObject silver;
    [SerializeField] protected float timer;
    [SerializeField] protected bool useTimer;
    
    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected AudioSource audioSource;
    
    [SerializeField] protected bool isSound = false;
    [SerializeField] protected bool isEffects; //Будет отвечать за нужен нам звук или нет
    [SerializeField] protected bool isComplete = false;

    protected abstract bool IsCorrectState();
    protected abstract void CompleteSubState();

    protected void ProcessTrigger(Collider other)
    {

        if (!IsCorrectState() || isComplete || other.gameObject != silver)
        {
            return;
        }

        HandleAudio();

        if (TimerProcess())
        {
            isComplete = true;
            CompleteSubState();
        }
    }

    protected bool TimerProcess()
    {

        if (useTimer)
        {
            timer -= Time.deltaTime;

            if (timer > 0)
            {
                isSound = true;
                Debug.Log($"До конца осталось {timer.ToString("F1")}");
                return false;
            }
            isSound = false;
            HandleAudio();
            return true;
        }

        isSound = false;
        return true;
    }

    protected void HandleAudio()
    {
        if (!isEffects) return;

        if (isSound && !audioSource.isPlaying)
        {
            if (particle != null) particle.Play();
            if (audioSource != null) audioSource.Play();
        }
        else if (!isSound && audioSource.isPlaying)
        {
            if (particle != null) particle.Stop();
            if (audioSource != null) audioSource.Stop();
        }
    }
}
